namespace SWHarden.RoiSelect.WinForms;

public partial class SingleRoiSelect : UserControl
{
    public readonly DraggableRoiCollection RoiCollection = new();

    readonly System.Windows.Forms.Timer UpdateTimer = new() { Interval = 20 };
    public int RenderCount { get; private set; }

    public bool UpdateNeeded = false;

    public SingleRoiSelect()
    {
        InitializeComponent();

        SizeChanged += (s, e) =>
        {
            // TODO: support non-square images
            int minEdge = Math.Min(panel1.Width, panel1.Height);
            pictureBox1.Size = new(minEdge, minEdge);
            pictureBox1.Location = new(0, 0);
            UpdateImage();
        };

        pictureBox1.MouseDown += (s, e) => RoiCollection.MouseDown(e.X, e.Y);
        pictureBox1.MouseUp += (s, e) => RoiCollection.MouseUp(e.X, e.Y);
        pictureBox1.MouseMove += (s, e) =>
        {
            bool movedSomething = RoiCollection.MouseMove(e.X, e.Y);
            Cursor = RoiCollection.GetCursor(e.X, e.Y);
            if (movedSomething)
                UpdateImage();
        };

        UpdateTimer.Tick += (s, e) => UpdateImageIfNeeded();
        UpdateTimer.Start();
    }

    private void AddSingleRoi(SizeF originalSize)
    {
        if (RoiCollection is null || RoiCollection.RoiBitmap is null)
            return;

        RoiCollection.ROIs.Clear();
        DraggableRoi roi = RoiCollection.GetCenterRoi(pictureBox1.Size, originalSize, 20);
        roi.IsSelected = true;
        RoiCollection.ROIs.Add(roi);
    }

    public DataRoi GetSingleDataRoi()
    {
        if (RoiCollection is null || RoiCollection.RoiBitmap is null)
            throw new InvalidOperationException();

        return RoiCollection.ROIs.First().GetDataRoi(pictureBox1.Size, RoiCollection.RoiBitmap.OriginalSize);
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

    private void UpdateImage()
    {
        UpdateNeeded = true;
    }

    private void UpdateImageIfNeeded()
    {
        if (!UpdateNeeded)
            return;
        UpdateNeeded = false;

        Image? oldImage = pictureBox1.Image;
        pictureBox1.Image = RoiCollection.GetBitmap(pictureBox1.Size);
        oldImage?.Dispose();
    }
}
