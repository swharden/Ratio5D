namespace SWHarden.RoiSelect.WinForms;

public enum Handle
{
    TopLeft, TopCenter, TopRight,
    MiddleLeft, /* MiddleCenter, */ MiddleRight,
    BottomLeft, BottomCenter, BottomRight,
}

public static class HandleExtensions
{
    public static PointF GetPoint(this Handle handle, RectangleF rect)
    {
        return handle switch
        {
            Handle.TopLeft => new(rect.Left, rect.Top),
            Handle.TopCenter => new((rect.Left + rect.Right) / 2, rect.Top),
            Handle.TopRight => new(rect.Right, rect.Top),
            Handle.MiddleLeft => new(rect.Left, (rect.Top + rect.Bottom) / 2),
            Handle.MiddleRight => new(rect.Right, (rect.Top + rect.Bottom) / 2),
            Handle.BottomLeft => new(rect.Left, rect.Bottom),
            Handle.BottomCenter => new((rect.Left + rect.Right) / 2, rect.Bottom),
            Handle.BottomRight => new(rect.Right, rect.Bottom),
            _ => throw new NotImplementedException(),
        };
    }

    public static RectangleF GetRect(this Handle handle, RectangleF rect, float radius)
    {
        PointF pt = handle.GetPoint(rect);
        return new(pt.X - radius, pt.Y - radius, radius * 2, radius * 2);
    }
}