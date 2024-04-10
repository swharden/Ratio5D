using Ratio5D.Core;

namespace Ratio5D.Gui;

public partial class Form1 : Form
{
    private TSeriesFolder? TS;

    public Form1()
    {
        InitializeComponent();
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

        hsbFrame.ValueChanged += (s, e) => UpdatePreviewImage();
        hsbSweep.ValueChanged += (s, e) => UpdatePreviewImage();
        LoadFolder(SampleData.TSeriesFolderPath);
    }

    private void LoadFolder(string path)
    {
        TS = new TSeriesFolder(path);

        hsbFrame.Minimum = 0;
        hsbFrame.Value = 0;
        hsbFrame.Maximum = TS.FramesPerSweep - 1;
        hsbFrame.LargeChange = 1;

        hsbSweep.Minimum = 0;
        hsbSweep.Value = 0;
        hsbSweep.Maximum = TS.Sweeps - 1;
        hsbSweep.LargeChange = 1;

        UpdatePreviewImage();
        UpdateProjectedImage();
    }

    private void UpdateProjectedImage()
    {
        if (TS is null)
            return;

        byte[] image = TS.GetProjectedRedImage();
        using MemoryStream ms = new(image);
        Bitmap bmp = new(ms);
        var old = pictureBox2.Image;
        pictureBox2.Image = bmp;
        old?.Dispose();
    }

    private void UpdatePreviewImage()
    {
        if (TS is null)
            return;

        int frame = hsbFrame.Value;
        int sweep = hsbSweep.Value;
        lblFrame.Text = $"Frame {frame + 1}/{TS.FramesPerSweep}";
        lblSweep.Text = $"Sweep {sweep + 1}/{TS.Sweeps}";

        byte[] image = TS.GetMergedImage(sweep, frame);
        using MemoryStream ms = new(image);
        Bitmap bmp = new(ms);
        var old = pictureBox1.Image;
        pictureBox1.Image = bmp;
        old?.Dispose();
    }
}
