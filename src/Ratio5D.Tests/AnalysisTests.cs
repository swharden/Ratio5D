using ScottPlot;

namespace Ratio5D.Tests;

internal class AnalysisTests
{
    [Test]
    public void Test_Analysis_Workflow()
    {
        TimeRange baselineRange = new(0.25, 0.75);
        TimeRange measureRange = new(1, 3);

        TSeriesFolder ts = Core.SampleData.TSeriesFolder;
        AfuData5D afuData = ts.GetAfuData();
        DffCurve[] sweeps = afuData.GetSweeps(baselineRange);

        Plot plot1 = new();
        Plotting.PlotAfuCurves(plot1, sweeps);
        plot1.SavePng("PlotAfuCurves.png", 600, 400).LaunchFile();

        Plot plot2 = new();
        Plotting.PlotDffCurves(plot2, sweeps, baselineRange, measureRange);
        plot2.SavePng("PlotDffCurves.png", 600, 400).LaunchFile();

        Plot plot3 = new();
        Plotting.PlotMeans(plot3, sweeps, measureRange);
        plot3.SavePng("PlotMeans.png", 600, 400).LaunchFile();
    }
}
