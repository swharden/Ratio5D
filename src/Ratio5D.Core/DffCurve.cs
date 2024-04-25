namespace Ratio5D.Core;

public class DffCurve
{
    public double Period { get; }
    public double[] RedRaw { get; }
    public double[] GreenRaw { get; }
    public double[] DFFs { get; }
    public IndexRange Baseline { get; }

    public DffCurve(double[] redAfus, double[] greenAfus, double period, IndexRange baseline)
    {
        RedRaw = redAfus;
        GreenRaw = greenAfus;
        Period = period;
        Baseline = baseline;

        DFFs = new double[RedRaw.Length];
        for (int i = 0; i < RedRaw.Length; i++)
            DFFs[i] = GreenRaw[i] / RedRaw[i];

        double baselineMean = DFFs[baseline.Start..baseline.End].Average();
        for (int i = 0; i < DFFs.Length; i++)
            DFFs[i] = (DFFs[i] - baselineMean) / baselineMean * 100;
    }

    public double GetMean(IndexRange range)
    {
        return DFFs[range.Start..range.End].Average();
    }

    public double GetMax(IndexRange range)
    {
        return DFFs[range.Start..range.End].Max();
    }
}
