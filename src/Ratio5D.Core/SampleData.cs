namespace Ratio5D.Core;

public static class SampleData
{
    public static string TSeriesFolderPath => @"C:\Users\swharden\Documents\Important\sample-data\TSeries-04052024-1736-2690";
    public static TSeriesFolder TSeriesFolder => new TSeriesFolder(TSeriesFolderPath);
}
