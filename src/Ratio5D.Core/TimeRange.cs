namespace Ratio5D.Core;

public readonly record struct TimeRange(double Start, double End)
{
    public (int, int) GetIndexes(double period)
    {
        int i1 = (int)(Start / period);
        int i2 = (int)(End / period);
        return (i1 == i2) ? (i1, i2 + 1) : (i1, i2);
    }
}
