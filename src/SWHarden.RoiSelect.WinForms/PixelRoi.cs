namespace SWHarden.RoiSelect.WinForms;

/// <summary>
/// ROI defined in pixel units
/// </summary>
public class PixelRoi(int x1, int x2, int y1, int y2, string name = "")
{
    public string Name { get; set; } = name;
    public int X1 { get; set; } = x1;
    public int X2 { get; set; } = x2;
    public int Y1 { get; set; } = y1;
    public int Y2 { get; set; } = y2;
    public int Top => Math.Min(Y1, Y2);
    public int Bottom => Math.Max(Y1, Y2);
    public int Left => Math.Min(X1, X2);
    public int Right => Math.Max(X1, X2);
    public int Width => Right - Left;
    public int Height => Bottom - Top;

    public override string ToString() => $"ROI {Name}: X={Left}, Y={Top}, W={Width}, H={Top}";
}
