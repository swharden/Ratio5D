using SWHarden.RoiSelect.WinForms;
using System.Drawing;
using System.Xml.Linq;

namespace Ratio5D.Core;

public class TSeriesFolder
{
    public string Path { get; }
    public string ReferencePath => System.IO.Path.Join(Path, "References");
    public string ProjectedBitmapFilePath => System.IO.Path.Join(ReferencePath, "projected.bmp");
    public string[] ReferenceFiles { get; }
    public string[] TifFiles { get; }
    public int Sweeps { get; }
    public int FramesPerSweep { get; }
    public double FramePeriod { get; }
    public double[] FrameTimes { get; }
    public const int ChannelsPerFrame = 2;
    private Action<int, int, string>? ImageLoadedAction { get; }

    private SciTIF.Image[] Images { get; }

    public TSeriesFolder(string path, Action<int, int, string>? imageLoadedAction = null, bool loadImmediately = false)
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
        FrameTimes = Enumerable.Range(0, FramesPerSweep).Select(x => x * FramePeriod).ToArray();
        Images = new SciTIF.Image[TifFiles.Length];
        if (loadImmediately)
            PopulateImagesFromDisk();
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

    public void PopulateImagesFromDisk()
    {
        ImageLoadedAction?.Invoke(0, 0, "Loading images from TIF files...");

        for (int i = 0; i < Images.Length; i++)
        {
            string imagePath = TifFiles[i];
            string imageFilename = System.IO.Path.GetFileName(imagePath);
            Images[i] = new SciTIF.TifFile(imagePath).GetImage();
            string message = $"Loading {imageFilename}";
            ImageLoadedAction?.Invoke(i + 1, Images.Length, message);
        }
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
        if (Images[0] is null)
            PopulateImagesFromDisk();
        int sweepOffset = FramesPerSweep * ChannelsPerFrame * sweep;
        int channelOffset = 0;
        int frameOffset = frame;
        return Images[sweepOffset + channelOffset + frameOffset];
    }

    public SciTIF.Image GetGreenImage(int sweep, int frame)
    {
        if (Images[0] is null)
            PopulateImagesFromDisk();
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

    public byte[] CreateProjectedRedBitmapBytes()
    {
        if (Images[0] is null)
            PopulateImagesFromDisk();

        List<SciTIF.Image> redImages = [];
        for (int sweep = 0; sweep < Sweeps; sweep++)
        {
            for (int frame = 0; frame < FramesPerSweep; frame++)
            {
                SciTIF.Image img = GetRedImage(sweep, frame);
                redImages.Add(img);
            }
        }

        SciTIF.ImageStack stack = new(redImages);

        double scaleBy = 1 / 32.0; // difference between 8-bit and 13-bit
        byte[] bytes = stack.ProjectMean().ScaledBy(0, scaleBy).GetBitmapBytes();
        File.WriteAllBytes(ProjectedBitmapFilePath, bytes);
        return bytes;
    }

    public Bitmap GetProjectedRedBitmap()
    {
        byte[] bytes = File.Exists(ProjectedBitmapFilePath)
            ? File.ReadAllBytes(ProjectedBitmapFilePath)
            : CreateProjectedRedBitmapBytes();

        using MemoryStream ms = new(bytes);
        Bitmap bmp = new(ms);
        return bmp;
    }

    public AfuData5D GetAfuData(DataRoi roi)
    {
        AfuData5D afuData = new(Sweeps, FramesPerSweep, FramePeriod);

        for (int sweep = 0; Sweeps > sweep; sweep++)
        {
            for (int frame = 0; frame < FramesPerSweep; frame++)
            {
                SciTIF.Image redImage = GetRedImage(sweep, frame);
                SciTIF.Image greenImage = GetGreenImage(sweep, frame);

                double[] redRoiValues = GetValues(redImage, roi);
                double[] greenRoiValues = GetValues(greenImage, roi);

                afuData.Reds[sweep, frame] = redRoiValues.Average();
                afuData.Greens[sweep, frame] = greenRoiValues.Average();
            }
        }

        return afuData;
    }

    private static double[] GetValues(SciTIF.Image image, DataRoi roi)
    {
        double[] values2 = new double[roi.Rect.Height * roi.Rect.Width];

        int i = 0;
        for (int dy = 0; dy < roi.Rect.Height; dy++)
            for (int dx = 0; dx < roi.Rect.Width; dx++)
                values2[i++] = image.GetPixel(roi.Rect.Left + dx, roi.Rect.Top + dy);

        return values2;
    }
}
