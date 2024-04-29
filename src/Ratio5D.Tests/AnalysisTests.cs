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
}
