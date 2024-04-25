using ScottPlot;

namespace Ratio5D.Core;

public static class Plotting
{
    public static Plot PlotAfuCurves(DffCurve[] sweeps)
    {
        Plot plot = new();

        foreach (DffCurve sweep in sweeps)
        {
            var sig1 = plot.Add.Signal(sweep.RedRaw);
            sig1.Color = Colors.Red;
            sig1.Data.Period = sweep.Period;

            var sig2 = plot.Add.Signal(sweep.GreenRaw);
            sig2.Color = Colors.Green;
            sig2.Data.Period = sweep.Period;
        }

        plot.YLabel("Raw PMT Values (AFU)");
        plot.XLabel("Time (seconds)");

        return plot;
    }

    public static Plot PlotDffCurves(DffCurve[] sweeps, TimeRange baseline, TimeRange measure)
    {
        Plot plot = new();

        foreach (DffCurve sweep in sweeps)
        {
            var sig3 = plot.Add.Signal(sweep.DFFs);
            sig3.Color = Colors.Blue;
            sig3.Data.Period = sweep.Period;
        }

        plot.YLabel("ΔF/F (%)");
        plot.XLabel("Time (seconds)");

        var hspan1 = plot.Add.HorizontalSpan(baseline.Start, baseline.End);
        hspan1.FillColor = Colors.Black.WithAlpha(.1);

        var hspan2 = plot.Add.HorizontalSpan(measure.Start, measure.End);
        hspan2.FillColor = Colors.Black.WithAlpha(.1);

        return plot;
    }

    public static Plot PlotMeans(DffCurve[] sweeps, TimeRange measureRange)
    {
        double[] xs = ScottPlot.Generate.Consecutive(sweeps.Length);
        double[] meansBySweep = sweeps
            .Select(x => x.GetMean(measureRange))
            .ToArray();

        Plot plot = new();
        plot.Add.Scatter(xs, meansBySweep);
        plot.YLabel("Mean ΔF/F (%)");
        plot.XLabel("Time (seconds)");

        return plot;
    }
}