namespace SWHarden.RoiSelect.WinForms;

// TODO: draw using new bitmap (no image stretching)
// TODO: mouse hit detection

public partial class MultiRoiSelect : UserControl
{
    private Bitmap? OriginalImage = null;
    public readonly RoiPixelCollection RoiCollection = new();

    public MultiRoiSelect()
    {
        InitializeComponent();
        listBox1.SelectedIndexChanged += (s, e) => UpdateImage();
        btnAdd.Click += (s, e) =>
        {
            if (OriginalImage is null)
                return;

            int x1 = Random.Shared.Next(OriginalImage.Width);
            int x2 = Random.Shared.Next(OriginalImage.Width);
            int y1 = Random.Shared.Next(OriginalImage.Height);
            int y2 = Random.Shared.Next(OriginalImage.Height);
            RoiCollection.Add(x1, x2, y1, y2, $"ROI #{RoiCollection.Count + 1}");
            listBox1.Items.Add(RoiCollection.Rois.Last().Name);
            UpdateImage();
        };
    }

    public void SetImage(Bitmap bmp)
    {
        var old = OriginalImage;
        OriginalImage = bmp;
        old?.Dispose();
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (OriginalImage is null)
            return;

        Bitmap bmp = new(OriginalImage);
        using Graphics gfx = Graphics.FromImage(bmp);

        for (int i = 0; i < RoiCollection.Count; i++)
        {
            PixelRoi roi = RoiCollection.Rois[i];
            bool isSelected = listBox1.SelectedIndex == i;
            DrawRoi(gfx, roi, isSelected);
        }

        var old = pictureBox1.Image;
        pictureBox1.Image = bmp;
        old?.Dispose();
    }

    private void DrawRoi(Graphics gfx, PixelRoi roi, bool isSelected)
    {
        gfx.DrawRectangle(Pens.Yellow, roi.Left, roi.Top, roi.Width, roi.Height);

        if (!isSelected)
            return;

        Point[] corners = [
            new(roi.Left, roi.Top),
            new(roi.Right, roi.Top),
            new(roi.Left, roi.Bottom),
            new(roi.Right, roi.Bottom),
        ];

        foreach (Point corner in corners)
        {
            int radB = 3;
            gfx.FillRectangle(Brushes.Black, corner.X - radB, corner.Y - radB, radB * 2, radB * 2);

            int radW = 2;
            gfx.FillRectangle(Brushes.White, corner.X - radW, corner.Y - radW, radW * 2, radW * 2);
        }

    }
}
