using System;

namespace SWHarden.RoiSelect.WinForms;

public class RoiSelectBase : UserControl
{
    public virtual Panel Panel { get; } = new Panel();
    public virtual PictureBox PictureBox { get; } = new PictureBox();

    public readonly DraggableRoiCollection RoiCollection = new();

    private readonly System.Windows.Forms.Timer UpdateTimer = new() { Interval = 20 };

    private bool UpdateNeeded = false;
    public int RenderCount { get; private set; }

    public RoiSelectBase()
    {
        if (!DesignMode)
        {
            UpdateTimer.Tick += (s, e) => UpdateImageIfNeeded();
            UpdateTimer.Start();
        }
    }

    public void UpdateSize()
    {
        // TODO: support non-square images
        int minEdge = Math.Min(Panel.Width, Panel.Height);
        PictureBox.Size = new(minEdge, minEdge);
        PictureBox.Location = new(0, 0);
        RoiCollection.SnapAfterResizing(PictureBox.Size);
        UpdateImage();
    }

    public void UpdateImage()
    {
        UpdateNeeded = true;
    }

    public void UpdateImageIfNeeded()
    {
        if (!UpdateNeeded)
            return;
        UpdateNeeded = false;

        Image? oldImage = PictureBox.Image;
        PictureBox.Image = RoiCollection.GetBitmap(PictureBox.Size);
        oldImage?.Dispose();
    }

    public void SetImage(Bitmap bmp)
    {
        RoiCollection.SetImage(bmp);
        UpdateImage();
    }

    public void SetImage(double[,] values)
    {
        RoiCollection.SetImage(values);
        UpdateImage();
    }

    public void AddCenterRoi()
    {
        if (RoiCollection is null || RoiCollection.RoiBitmap is null)
            return;

        SizeF originalSize = RoiCollection.RoiBitmap.OriginalSize;
        DraggableRoi roi = RoiCollection.GetCenterRoi(PictureBox.Size, originalSize, 20);
        roi.IsSelected = true;
        RoiCollection.ROIs.Add(roi);
    }

    public DataRoi GetDataRoi(int roiIndex)
    {
        if (RoiCollection is null || RoiCollection.RoiBitmap is null)
            throw new InvalidOperationException();

        return RoiCollection.ROIs[roiIndex].GetDataRoi(PictureBox.Size, RoiCollection.RoiBitmap.OriginalSize);
    }
}
