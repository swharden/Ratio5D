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

    private SciTIF.Image[] Images { get; }

    public TSeriesFolder(string path)
    {
        Path = path;
        ReferenceFiles = Directory.GetFiles(ReferencePath);
        TifFiles = Directory.GetFiles(path, "*.tif");
        Array.Sort(TifFiles);

        string xmlFile = Directory.GetFiles(path, "*.xml").Single();
        string xmlText = File.ReadAllText(xmlFile);
        XDocument doc = XDocument.Parse(xmlText);
        Sweeps = GetSequenceCount(doc);
        FramesPerSweep = GetFrameCount(doc);
        FramePeriod = GetFramePeriod(doc);
        Images = TifFiles.Select(x => new SciTIF.TifFile(x).GetImage()).ToArray();
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

    private SciTIF.Image GetRedImage(int sweep, int frame)
    {
        int sweepOffset = FramesPerSweep * ChannelsPerFrame * sweep;
        int channelOffset = 0;
        int frameOffset = frame;
        return Images[sweepOffset + channelOffset + frameOffset];
    }

    private SciTIF.Image GetGreenImage(int sweep, int frame)
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
        List<SciTIF.Image> images = new();
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
