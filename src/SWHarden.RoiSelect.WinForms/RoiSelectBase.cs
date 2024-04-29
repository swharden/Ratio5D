using System.Windows.Forms;

namespace SWHarden.RoiSelect.WinForms;

public abstract class RoiSelectBase : UserControl
{
    public abstract Panel Panel { get; }
    public abstract PictureBox PictureBox { get; }

    public readonly DraggableRoiCollection RoiCollection = new();

    private readonly System.Windows.Forms.Timer UpdateTimer = new() { Interval = 20 };

    private bool UpdateNeeded = false;
    public int RenderCount { get; private set; }

    public void StartUpdateTimer()
    {
        UpdateTimer.Tick += (s, e) => UpdateImageIfNeeded();
        UpdateTimer.Start();
    }

    public void UpdateSize()
    {
        // TODO: support non-square images
        int minEdge = Math.Min(Panel.Width, Panel.Height);
        PictureBox.Size = new(minEdge, minEdge);
        PictureBox.Location = new(0, 0);
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
        AddSingleRoi(bmp.Size);
        UpdateImage();
    }

    public void SetImage(double[,] values)
    {
        RoiCollection.SetImage(values);
        AddSingleRoi(new SizeF(values.GetLength(1), values.GetLength(0)));
        UpdateImage();
    }

    public void AddSingleRoi(SizeF originalSize)
    {
        if (RoiCollection is null || RoiCollection.RoiBitmap is null)
            return;

        RoiCollection.ROIs.Clear();
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
