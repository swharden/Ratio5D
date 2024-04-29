namespace Ratio5D.Core;

public static class SampleData
{
    public static string TSeriesFolderPath
    {
        get
        {
            string[] paths = [
                @"C:\Users\swharden\Documents\Important\sample-data\TSeries-04052024-1736-2690",
                @"C:\Users\scott\Documents\SampleData\TSeries-04062024-0120-2717",
            ];

            foreach (string path in paths)
            {
                if (Directory.Exists(path))
                    return path;
            }

            throw new DirectoryNotFoundException();
        }
    }
    public static TSeriesFolder TSeriesFolder => new TSeriesFolder(TSeriesFolderPath);
}
