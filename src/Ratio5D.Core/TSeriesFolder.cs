using System.Xml.Linq;

namespace Ratio5D.Core;

public class TSeriesFolder
{
    public string Path { get; }
    public string ReferencePath => System.IO.Path.Join(Path, "References");
    public string[] ReferenceFiles { get; }
    public string[] TifFiles { get; }
    public int Sweeps { get; }
    public int FrameCount { get; }
    public double FramePeriod { get; }

    public TSeriesFolder(string path)
    {
        Path = path;
        ReferenceFiles = Directory.GetFiles(ReferencePath);
        TifFiles = Directory.GetFiles(path, "*.tif");

        string xmlFile = Directory.GetFiles(path, "*.xml").Single();
        string xmlText = File.ReadAllText(xmlFile);
        XDocument doc = XDocument.Parse(xmlText);
        Sweeps = GetSequenceCount(doc);
        FrameCount = GetFrameCount(doc);
        FramePeriod = GetFramePeriod(doc);
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
}
