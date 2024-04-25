using System.Xml.Linq;

namespace Ratio5D.Core;

public class TSeriesFolder
{
    public string Path { get; }
    public string ReferencePath => System.IO.Path.Join(Path, "References");
    public string[] ReferenceFiles { get; }
    public string[] TifFiles { get; }
    public int Sweeps { get; }
    public int FramesPerSweep { get; }
    public double FramePeriod { get; }
    public const int ChannelsPerFrame = 2;
    private Action<int, int, string>? ImageLoadedAction { get; }

    private SciTIF.Image[] Images { get; }

    public TSeriesFolder(string path, Action<int, int, string>? imageLoadedAction = null)
    {
        ImageLoadedAction = imageLoadedAction;
        Path = path;

        ImageLoadedAction?.Invoke(0, 100, "Scanning folder");
        ReferenceFiles = Directory.GetFiles(ReferencePath);
        TifFiles = Directory.GetFiles(path, "*.tif");
        Array.Sort(TifFiles);

        ImageLoadedAction?.Invoke(0, 100, "Reading XML metadata");
        string xmlFile = Directory.GetFiles(path, "*.xml").Single();
        string xmlText = File.ReadAllText(xmlFile);
        XDocument doc = XDocument.Parse(xmlText);
        Sweeps = GetSequenceCount(doc);
        FramesPerSweep = GetFrameCount(doc);
        FramePeriod = GetFramePeriod(doc);
        Images = LoadImageTiffs();
        ImageLoadedAction?.Invoke(TifFiles.Length, TifFiles.Length, $"Loaded {TifFiles.Length} images");
    }

    public bool HasSavedImageData()
    {
        string saveAs = System.IO.Path.Join(ReferencePath, "ImageData.bin");
        return System.IO.File.Exists(saveAs);
    }

    public void SaveImageData()
    {
        int valueCount = Images.First().Values.Length * Images.Length;
        Console.WriteLine($"Creating save file with {valueCount:N0} values...");
        double[] allValues = new double[valueCount];

        int index = 0;
        for (int i = 0; i < Images.Length; i++)
        {
            for (int j = 0; j < Images[i].Values.Length; j++)
            {
                allValues[index++] = Images[i].Values[j];
            }
        }

        string saveAs = System.IO.Path.Join(ReferencePath, "ImageData.bin");

        byte[] bytes = new byte[allValues.Length * sizeof(double)];
        Buffer.BlockCopy(allValues, 0, bytes, 0, bytes.Length);

        Console.WriteLine($"Writing {saveAs} ({bytes.Length / 1000:N0} kB)...");
        File.WriteAllBytes(saveAs, bytes);
    }

    public SciTIF.Image[] LoadImageTiffs()
    {
        ImageLoadedAction?.Invoke(0, 0, "Loading images from TIF files...");

        int imageCount = TifFiles.Length;
        SciTIF.Image[] images = new SciTIF.Image[imageCount];
        for (int i = 0; i < imageCount; i++)
        {
            string imagePath = TifFiles[i];
            string imageFilename = System.IO.Path.GetFileName(imagePath);
            images[i] = new SciTIF.TifFile(imagePath).GetImage();
            string message = $"Loading {imageFilename}";
            ImageLoadedAction?.Invoke(i + 1, imageCount, message);
        }

        return images;
    }

    private static int GetSequenceCount(XDocument doc)
    {
        return doc.Element("PVScan")!
            .Elements("Sequence")
            .Count();
    }

    private static int GetFrameCount(XDocument doc)
    {
        return doc.Element("PVScan")!
            .Elements("Sequence")
            .First()
            .Elements("Frame")
            .Count();
    }

    private static double GetFramePeriod(XDocument doc)
    {
        string v = doc.Element("PVScan")!
            .Elements("Sequence")
            .First()
            .Elements("Frame")
            .Skip(1)
            .First()
            .Attribute("relativeTime")!
            .Value;

        return double.Parse(v);
    }

    public SciTIF.Image GetRedImage(int sweep, int frame)
    {
        int sweepOffset = FramesPerSweep * ChannelsPerFrame * sweep;
        int channelOffset = 0;
        int frameOffset = frame;
        return Images[sweepOffset + channelOffset + frameOffset];
    }

    public SciTIF.Image GetGreenImage(int sweep, int frame)
    {
        int sweepOffset = FramesPerSweep * ChannelsPerFrame * sweep;
        int channelOffset = FramesPerSweep;
        int frameOffset = frame;
        return Images[sweepOffset + channelOffset + frameOffset];
    }

    public byte[] GetMergedImage(int sweep, int frame)
    {
        double scaleBy = 1 / 32.0; // difference between 8-bit and 13-bit
        SciTIF.Image red = GetRedImage(sweep, frame).ScaledBy(0, scaleBy);
        SciTIF.Image green = GetGreenImage(sweep, frame).ScaledBy(0, scaleBy);
        SciTIF.ImageRGB rgb = new(red, green, red);
        return rgb.GetBitmapBytes();
    }

    public byte[] GetProjectedRedImage()
    {
        List<SciTIF.Image> images = [];
        for (int sweep = 0; sweep < Sweeps; sweep++)
        {
            for (int frame = 0; frame < FramesPerSweep; frame++)
            {
                SciTIF.Image img = GetRedImage(sweep, frame);
                images.Add(img);
            }
        }

        SciTIF.ImageStack stack = new(images);

        double scaleBy = 1 / 32.0; // difference between 8-bit and 13-bit
        return stack.ProjectMean().ScaledBy(0, scaleBy).GetBitmapBytes();
    }
}
