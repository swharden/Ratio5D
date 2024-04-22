namespace SWHarden.RoiSelect.WinForms;

public class DraggableRoiCollection()
{
    public readonly List<DraggableRoi> ROIs = [];
    public RoiBitmap? RoiBitmap { get; set; } = null;

    private DraggableRoi? RoiUnderMouse = null;
    public bool IsRoiUnderMouse => RoiUnderMouse is not null;
    public bool IsDraggingHandle => RoiUnderMouse is not null && RoiUnderMouse.IsDraggingHandle;
    public bool SnapToPixels { get; set; } = true;
    public DraggableRoi GetRandomRoi()
    {
        if (RoiBitmap is null)
            throw new InvalidOperationException();

        float x1 = Random.Shared.Next(0, RoiBitmap.OriginalWidth - 1) * RoiBitmap.ScaleX;
        float x2 = Random.Shared.Next(0, RoiBitmap.OriginalWidth - 1) * RoiBitmap.ScaleX;
        float y1 = Random.Shared.Next(0, RoiBitmap.OriginalHeight - 1) * RoiBitmap.ScaleY;
        float y2 = Random.Shared.Next(0, RoiBitmap.OriginalHeight - 1) * RoiBitmap.ScaleY;
        DraggableRoi roi = new(x1, x2, y1, y2);
        return roi;
    }

    public void SetImage(Bitmap bmp)
    {
        RoiBitmap? oldBmp = RoiBitmap;
        RoiBitmap = new(bmp);
        oldBmp?.Dispose();
    }

    public Bitmap? GetBitmap(Size size)
    {
        if (RoiBitmap is null)
            return null!;

        return RoiBitmap.GetBitmap(size, this);
    }

    public Cursor GetCursor(float x, float y)
    {
        foreach (DraggableRoi roi in ROIs)
        {
            Cursor? roiCursor = roi.GetCursor(x, y);

            if (roiCursor is not null)
                return roiCursor;
        }

        return Cursors.Arrow;
    }

    public bool MouseMove(float x, float y)
    {
        foreach (DraggableRoi roi in ROIs)
        {
            if (!roi.IsSelected) // only interact with selected ROIs
                continue;

            string before = roi.ToString();
            roi.MouseMove(x, y);

            if (roi.IsDraggingHandle && RoiBitmap is not null)
            {
                roi.Snap(RoiBitmap.ScaleX, RoiBitmap.ScaleY);
            }

            if (roi.IsHandleUnderMouse)
            {
                RoiUnderMouse = roi;
                string after = roi.ToString();
                return before != after;
            }
        }

        RoiUnderMouse = null;
        return false;
    }

    public void MouseDown(float x, float y)
    {
        RoiUnderMouse?.MouseDown(x, y);
    }

    public void MouseUp(float x, float y)
    {
        RoiUnderMouse?.MouseUp(x, y);
    }

    public void Load()
    {

    }

    public void Save()
    {

    }
}
