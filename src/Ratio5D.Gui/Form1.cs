using Ratio5D.Core;
using ScottPlot;
using SWHarden.RoiSelect.WinForms;
using System.Diagnostics;

namespace Ratio5D.Gui;

public partial class Form1 : Form
{
    private TSeriesFolder? TS;

    public Form1()
    {
        InitializeComponent();
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        hsbFrame.ValueChanged += (s, e) => UpdatePreviewImage();
        hsbSweep.ValueChanged += (s, e) => UpdatePreviewImage();

        Shown += (s, e) =>
        {
            Application.DoEvents();
            LoadFolder(Ratio5D.Core.SampleData.TSeriesFolderPath);
        };

        multiRoiSelect1.RoiCollection.SelectedRoiChanged += (s, e) => AnalyzeRoi(e);
    }

    DateTime LastProgressUpdate;
    void LoadingUpdate(int value, int max, string message)
    {
        bool intermediateValue = value > 0 && value < max;
        if (intermediateValue && (DateTime.Now - LastProgressUpdate).TotalSeconds < .1)
            return;

        Text = $"{value} of {max} {message}";
        progressBar1.Maximum = max;
        progressBar1.Value = value;
        LastProgressUpdate = DateTime.Now;
    }

    private void LoadFolder(string path)
    {
        Stopwatch sw = Stopwatch.StartNew();
        progressBar1.Visible = true;
        TS = new TSeriesFolder(path, LoadingUpdate);

        hsbFrame.Minimum = 0;
        hsbFrame.Value = 0;
        hsbFrame.Maximum = TS.FramesPerSweep - 1;
        hsbFrame.LargeChange = 1;

        hsbSweep.Minimum = 0;
        hsbSweep.Value = 0;
        hsbSweep.Maximum = TS.Sweeps - 1;
        hsbSweep.LargeChange = 1;

        LoadingUpdate(30, 100, "Updating preview image...");
        UpdatePreviewImage();
        LoadingUpdate(60, 100, "Updating projection image...");
        UpdateProjectedImage();
        LoadingUpdate(100, 100, $"Loading completed in {sw.Elapsed.TotalSeconds:N2} seconds");
        progressBar1.Visible = false;
    }

    private void UpdateProjectedImage()
    {
        if (TS is null)
            return;

        byte[] image = TS.GetProjectedRedImage();
        using MemoryStream ms = new(image);
        Bitmap bmp = new(ms);
        multiRoiSelect1.SetImage(bmp);
    }

    private void UpdatePreviewImage()
    {
        if (TS is null)
            return;

        int frame = hsbFrame.Value;
        int sweep = hsbSweep.Value;
        gbFrame.Text = $"Frame {frame + 1}/{TS.FramesPerSweep}";
        gbSweep.Text = $"Sweep {sweep + 1}/{TS.Sweeps}";

        byte[] image = TS.GetMergedImage(sweep, frame);
        using MemoryStream ms = new(image);
        Bitmap bmp = new(ms);
        var old = pictureBox1.Image;
        pictureBox1.Image = bmp;
        old?.Dispose();
    }

    private void AnalyzeRoi(DataRoi roi)
    {
        if (TS is null)
            return;

        // TODO: update from GUI
        formsPlot1.Plot.Clear();

        for (int i = 0; i < TS.Sweeps; i++)
        {
            double[] reds = new double[TS.FramesPerSweep];
            double[] greens = new double[TS.FramesPerSweep];
            double[] ratios = new double[TS.FramesPerSweep];

            for (int j = 0; j < TS.FramesPerSweep; j++)
            {
                SciTIF.Image red = TS.GetRedImage(i, j);
                SciTIF.Image green = TS.GetGreenImage(i, j);
                reds[i] = red.Values.Average();
                greens[i] = green.Values.Average();
                ratios[i] = greens[i] / reds[i];
            }

            var sig1 = formsPlot1.Plot.Add.Signal(reds);
            sig1.Color = Colors.Red;
            sig1.LineWidth = 2;

            var sig2 = formsPlot1.Plot.Add.Signal(greens);
            sig2.Color = Colors.Green;
            sig2.LineWidth = 2;

            var sig3 = formsPlot2.Plot.Add.Signal(ratios);
            sig3.Color = Colors.C0;
            sig3.LineWidth = 2;

            formsPlot1.Refresh();
            formsPlot2.Refresh();
            Application.DoEvents();
        }
    }
}
