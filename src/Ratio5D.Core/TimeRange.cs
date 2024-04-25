namespace Ratio5D.Core;

public readonly record struct TimeRange(double Start, double End)
{
    public IndexRange ToIndexRange(double period)
    {
        int i1 = (int)(Start / period) - 1;
        int i2 = (int)(End / period);
        return (i1 == i2) ? new(i1, i2 + 1) : new(i1, i2);
    }
}
