namespace SWHarden.RoiSelect.WinForms;

public readonly struct DataRoi(int x1, int x2, int y1, int y2)
{
    public int XMin { get; } = Math.Min(x1, x2);
    public int XMax { get; } = Math.Max(x1, x2);
    public int YMin { get; } = Math.Min(y1, y2);
    public int YMax { get; } = Math.Max(y1, y2);

    public int Width => XMax - XMin + 1;
    public int Height => YMax - XMin + 1;
}
