﻿namespace Ratio5D.Core;

public class AfuData5D(int sweeps, int framesPerSweep, double framePeriod)
{
    public double[,] Reds = new double[sweeps, framesPerSweep];
    public double[,] Greens = new double[sweeps, framesPerSweep];
    public int Sweeps => Reds.GetLength(0);
    public int FramesPerSweep => Reds.GetLength(1);
    public double FramePeriod = framePeriod;

    public DffCurve[] GetSweeps(IndexRange baseline) =>
        Enumerable.Range(0, Sweeps)
        .Select(x => GetSweep(x, baseline))
        .ToArray();

    public DffCurve[] GetSweepsRelativeToFirst(IndexRange baseline)
    {
        DffCurve firstSweep = GetSweep(0, baseline);
        DffCurve[] curves = new DffCurve[Sweeps];
        for (int i = 0; i < Sweeps; i++)
        {
            curves[i] = GetSweep(i, baseline);
            for (int j = 0; j < firstSweep.DFFs.Length; j++)
            {
                curves[i].DFFs[j] -= firstSweep.DFFs[j];
            }
        }
        return curves;
    }

    public DffCurve GetSweep(int sweep, IndexRange baseline) =>
        new(GetRow(Reds, sweep), GetRow(Greens, sweep), FramePeriod, baseline);

    static double[] GetRow(double[,] data, int row)
    {
        double[] values = new double[data.GetLength(1)];
        for (int i = 0; i < values.Length; i++)
            values[i] = data[row, i];
        return values;
    }
}
