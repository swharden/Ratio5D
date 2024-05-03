using ScottPlot;

using SWHarden.RoiSelect.WinForms;

namespace Ratio5D.Tests;

internal class AnalysisTests
{
    [Test]
    public void Test_Analysis_Workflow()
    {
        Ratio5D.Core.IndexRange baselineRange = new(10, 15);
        Ratio5D.Core.IndexRange measureRange = new(20, 40);

        DataRoi roi = new(new System.Drawing.Rectangle(2, 3, 4, 5), new double[,] { });

        TSeriesFolder ts = Core.SampleData.TSeriesFolder;
        AfuData5D afuData = ts.GetAfuData(roi);
        DffCurve[] sweeps = afuData.GetSweeps(baselineRange);

        Plot plot1 = new();
        Plotting.PlotAfuCurves(plot1, sweeps, baselineRange, measureRange);
        plot1.SavePng("PlotAfuCurves.png", 600, 400).LaunchFile();

        Plot plot2 = new();
        Plotting.PlotDffCurves(plot2, sweeps, baselineRange, measureRange);
        plot2.SavePng("PlotDffCurves.png", 600, 400).LaunchFile();

        Plot plot3 = new();
        Plotting.PlotMeans(plot3, sweeps, measureRange);
        plot3.SavePng("PlotMeans.png", 600, 400).LaunchFile();
    }

    [Test]
    public void Test_Analysis_Report()
    {
        string dataFolder = @"X:\Data\zProjects\Aging Spine\data";
        string[] roiCsvPaths = Directory.GetFiles(dataFolder, "rois.csv", SearchOption.AllDirectories);

        List<string> analysisFoldersShort = [];
        for (int i = 0; i < roiCsvPaths.Length; i += 2)
            analysisFoldersShort.Add(Path.GetDirectoryName(roiCsvPaths[i])!);

        List<string> analysisFoldersLong = [];
        for (int i = 1; i < roiCsvPaths.Length; i += 2)
            analysisFoldersLong.Add(Path.GetDirectoryName(roiCsvPaths[i])!);

        Console.WriteLine("SHORT");
        foreach (string folder in analysisFoldersShort)
        {
            string dffCsvPath = Directory.GetFiles(folder)
                .Where(x => x.Contains("dff-TSeries-") && x.EndsWith(".csv"))
                .Single();

            Console.WriteLine(dffCsvPath);
        }

        Console.WriteLine("LONG");
        foreach (string folder in analysisFoldersLong)
        {
            string dffCsvPath = Directory.GetFiles(folder)
                .Where(x => x.Contains("dff-TSeries-") && x.EndsWith(".csv"))
                .Single();

            Console.WriteLine(dffCsvPath);
        }
    }

    [Test]
    public void Test_Analysis_Report2()
    {
        string dataFolder = @"X:\Data\zProjects\Aging Spine\data";
        string[] roiCsvPaths = Directory.GetFiles(dataFolder, "rois.csv", SearchOption.AllDirectories);

        List<string> analysisFoldersShort = [];
        for (int i = 0; i < roiCsvPaths.Length; i += 2)
            analysisFoldersShort.Add(Path.GetDirectoryName(roiCsvPaths[i])!);

        List<string> analysisFoldersLong = [];
        for (int i = 1; i < roiCsvPaths.Length; i += 2)
            analysisFoldersLong.Add(Path.GetDirectoryName(roiCsvPaths[i])!);

        Console.WriteLine("SHORT");
        foreach (string folder in analysisFoldersShort)
        {
            string dffCsvPath = Directory.GetFiles(folder)
                .Where(x => x.Contains("mean-TSeries-") && x.EndsWith(".csv"))
                .Single();

            Console.WriteLine(dffCsvPath);
        }

        Console.WriteLine("LONG");
        foreach (string folder in analysisFoldersLong)
        {
            string dffCsvPath = Directory.GetFiles(folder)
                .Where(x => x.Contains("mean-TSeries-") && x.EndsWith(".csv"))
                .Single();

            Console.WriteLine(dffCsvPath);
        }
    }
}
