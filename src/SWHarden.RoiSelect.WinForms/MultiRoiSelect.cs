using System.Diagnostics;

namespace SWHarden.RoiSelect.WinForms;

public partial class MultiRoiSelect : UserControl
{
    public readonly DraggableRoiCollection RoiCollection = new();

    readonly System.Windows.Forms.Timer UpdateTimer = new() { Interval = 20 };
    public int RenderCount { get; private set; }

    public bool UpdateNeeded = false;
    private void UpdateImage() => UpdateNeeded = true;

    public MultiRoiSelect()
    {
        InitializeComponent();

        listBox1.SelectedIndexChanged += (s, e) =>
        {
            for (int i = 0; i < RoiCollection.ROIs.Count; i++)
                RoiCollection.ROIs[i].IsSelected = listBox1.SelectedIndices.Contains(i);

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
            DraggableRoi roi = RoiCollection.GetRandomRoi();
            RoiCollection.ROIs.Add(roi);
            listBox1.Items.Add($"ROI #{RoiCollection.ROIs.Count}");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        };

        btnDelete.Click += (s, e) =>
        {
            List<int> indices = [];
            foreach (int index in listBox1.SelectedIndices)
                indices.Add(index);
            indices.Reverse();

            foreach (int index in indices)
            {
                listBox1.Items.RemoveAt(index);
                RoiCollection.ROIs.RemoveAt(index);
            }
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
