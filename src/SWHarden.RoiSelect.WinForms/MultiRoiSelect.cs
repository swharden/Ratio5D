namespace SWHarden.RoiSelect.WinForms;

public partial class MultiRoiSelect : UserControl
{
    public RoiBitmap? RoiBitmap = null;
    public readonly RoiPixelCollection RoiCollection = new();

    public MultiRoiSelect()
    {
        InitializeComponent();

        listBox1.SelectedIndexChanged += (s, e) =>
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
                RoiCollection.Rois[i].IsSlected = listBox1.SelectedIndices.Contains(i);
            UpdateImage();
        };

        SizeChanged += (s, e) =>
        {
            // TODO: support non-square images
            int minEdge = Math.Min(panel1.Width, panel1.Height);
            pictureBox1.Size = new(minEdge, minEdge);
            pictureBox1.Location = new(0, 0);
            UpdateImage();
        };

        btnAdd.Click += (s, e) =>
        {
            if (RoiBitmap is null)
                return;

            int x1 = Random.Shared.Next(RoiBitmap.OriginalWidth);
            int x2 = Random.Shared.Next(RoiBitmap.OriginalWidth);
            int y1 = Random.Shared.Next(RoiBitmap.OriginalHeight);
            int y2 = Random.Shared.Next(RoiBitmap.OriginalHeight);
            RoiCollection.Add(x1, x2, y1, y2, $"ROI #{RoiCollection.Count + 1}");
            listBox1.Items.Add(RoiCollection.Rois.Last().Name);
            UpdateImage();
        };

        pictureBox1.MouseMove += (s, e) =>
        {
            if (RoiBitmap is null)
                return;

            Point pt = new(e.X, e.Y);
            Cursor = RoiBitmap.IsMouseOver(pt) ? Cursors.Hand : Cursors.Arrow;
        };
    }

    public void SetImage(Bitmap bmp)
    {
        RoiBitmap? oldBmp = RoiBitmap;
        RoiBitmap = new(bmp, RoiCollection);
        oldBmp?.Dispose();
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (RoiBitmap is null)
            return;

        Image? oldImage = pictureBox1.Image;
        pictureBox1.Image = RoiBitmap.GetBitmap(pictureBox1.Size);
        oldImage?.Dispose();
    }
}
