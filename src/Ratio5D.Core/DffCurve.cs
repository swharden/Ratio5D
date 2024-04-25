namespace Ratio5D.Core;

public class DffCurve
{
    public double Period { get; }
    public double[] RedRaw { get; }
    public double[] GreenRaw { get; }
    public double[] DFFs { get; }
    public TimeRange Baseline { get; }

    public DffCurve(double[] redAfus, double[] greenAfus, double period, TimeRange baseline)
    {
        RedRaw = redAfus;
        GreenRaw = greenAfus;
        Period = period;
        Baseline = baseline;

        DFFs = new double[RedRaw.Length];
        for (int i = 0; i < RedRaw.Length; i++)
            DFFs[i] = GreenRaw[i] / RedRaw[i];

        (int i1, int i2) = baseline.GetIndexes(period);
        double baselineMean = DFFs[i1..i2].Average();
        for (int i = 0; i < DFFs.Length; i++)
            DFFs[i] = (DFFs[i] - baselineMean) / baselineMean * 100;
    }

    public double GetMean(TimeRange range)
    {
        (int i1, int i2) = range.GetIndexes(Period);
        return DFFs[i1..i2].Average();
    }

    public double GetMax(double baseline1 = 0, double baseline2 = 1)
    {
        int i1 = (int)(baseline1 / Period);
        int i2 = (int)(baseline2 / Period);
        if (i1 == i2) i2 += 2;
        return DFFs[i1..i2].Max();
    }
}
