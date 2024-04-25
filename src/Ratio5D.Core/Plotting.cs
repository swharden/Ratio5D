using ScottPlot;
using System.Runtime.Intrinsics.X86;

namespace Ratio5D.Core;

public static class Plotting
{
    public static void PlotAfuCurves(Plot plot, DffCurve[] sweeps, IndexRange baseline, IndexRange measure)
    {
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

        (double b1, double b2) = baseline.GetTimes(sweeps.First().Period);
        var hspan1 = plot.Add.HorizontalSpan(b1, b2);
        hspan1.FillColor = Colors.Black.WithAlpha(.1);

        (double m1, double m2) = measure.GetTimes(sweeps.First().Period);
        var hspan2 = plot.Add.HorizontalSpan(m1, m2);
        hspan2.FillColor = Colors.Black.WithAlpha(.1);
    }

    public static void PlotDffCurves(Plot plot, DffCurve[] sweeps, IndexRange baseline, IndexRange measure)
    {
        foreach (DffCurve sweep in sweeps)
        {
            var sig3 = plot.Add.Signal(sweep.DFFs);
            sig3.Color = Colors.Blue;
            sig3.Data.Period = sweep.Period;
        }

        plot.YLabel("ΔF/F (%)");
        plot.XLabel("Time (seconds)");

        (double b1, double b2) = baseline.GetTimes(sweeps.First().Period);
        var hspan1 = plot.Add.HorizontalSpan(b1, b2);
        hspan1.FillColor = Colors.Black.WithAlpha(.1);

        (double m1, double m2) = measure.GetTimes(sweeps.First().Period);
        var hspan2 = plot.Add.HorizontalSpan(m1, m2);
        hspan2.FillColor = Colors.Black.WithAlpha(.1);
    }

    public static void PlotMeans(Plot plot, DffCurve[] sweeps, IndexRange measureRange)
    {
        double[] xs = ScottPlot.Generate.Consecutive(sweeps.Length);
        double[] meansBySweep = sweeps
            .Select(x => x.GetMean(measureRange))
            .ToArray();

        plot.Add.Scatter(xs, meansBySweep);
        plot.YLabel("Mean ΔF/F (%)");
        plot.XLabel("Time (seconds)");
    }
}