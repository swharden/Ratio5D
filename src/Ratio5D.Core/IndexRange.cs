namespace Ratio5D.Core;

public readonly record struct IndexRange(int Start, int End)
{
    public (double start, double end) GetTimes(double period) => (Start * period, End * period);
}
