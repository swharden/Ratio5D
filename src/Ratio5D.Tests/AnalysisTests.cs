using ScottPlot;

namespace Ratio5D.Tests;

internal class AnalysisTests
{
    [Test]
    public void Test_Analysis_Workflow()
    {
        /*
            * copy all rave outputs (one per stim time) onto excel sheet
            * excel sheet subtracts first sweep from all sweeps (to account for bleaching)
            * copy/paste from excel into new origin sheet (once for mean, once for peak)
            * origin sheet now contains dF/F values for all cells (columns) at all stimulations (rows)
            * ccave and plot Y +/- SE
        */

        TimeRange baselineRange = new(0.25, 0.75);
        TimeRange measureRange = new(1, 3);

        TSeriesFolder ts = Core.SampleData.TSeriesFolder;
        AfuData5D afuData = ts.GetAfuData();
        DffCurve[] sweeps = afuData.GetSweeps(baselineRange);

        Plotting.PlotAfuCurves(sweeps)
            .SavePng("PlotAfuCurves.png", 600, 400)
            .LaunchFile();

        Plotting.PlotDffCurves(sweeps, baselineRange, measureRange)
            .SavePng("PlotDffCurves.png", 600, 400)
            .LaunchFile();

        Plotting.PlotMeans(sweeps, measureRange)
            .SavePng("PlotMeans.png", 600, 400)
            .LaunchFile();
    }


}
