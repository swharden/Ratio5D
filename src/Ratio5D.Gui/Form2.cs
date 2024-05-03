using Ratio5D.Core;
using SWHarden.RoiSelect.WinForms;
using System.Diagnostics;
using System.Windows.Forms;

namespace Ratio5D.Gui;

public partial class Form2 : Form
{
    TSeriesFolder? TS = null;
    System.Windows.Forms.Timer UpdateTimer = new() { Interval = 50 };
    private bool UpdateNeeded = false;

    public Form2()
    {
        InitializeComponent();
        progressBar1.Visible = false;

        nudB1.ValueChanged += (s, e) => UpdatePlots();
        nudB2.ValueChanged += (s, e) => UpdatePlots();
        nudM1.ValueChanged += (s, e) => UpdatePlots();
        nudM2.ValueChanged += (s, e) => UpdatePlots();

        singleRoiSelect1.RoiCollection.SelectedRoiChanged += (s, e) => UpdatePlots();
        cbSubtract.CheckedChanged += (s, e) => UpdatePlots();

        SetFolder(Core.SampleData.TSeriesFolderPath);
        UpdateTimer.Tick += (s, e) => UpdatePlotsBlocking(false, false);
        UpdateTimer.Start();

        btnSave.Click += (s, e) => UpdatePlotsBlocking(true, true);

        AllowDrop = true;

        DragEnter += (o, e) =>
        {
            if (e.Data is null) return;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        };

        DragDrop += (o, e) =>
        {
            if (e.Data is null) return;
            string[]? paths = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (paths is null) return;
            SetFolder(paths.First());
        };

        lblFolder.Click += (s, e) => Clipboard.SetText(lblFolder.Text);
    }

    void ImageLoadAction(int a, int b, string c)
    {
        progressBar1.Maximum = b;
        progressBar1.Value = a;
        Text = c;
    }


    void SetFolder(string folderPath)
    {
        progressBar1.Visible = true;
        progressBar1.Value = 0;
        TS = new TSeriesFolder(folderPath, ImageLoadAction);
        singleRoiSelect1.SetImage(TS.GetProjectedRedBitmap());
        singleRoiSelect1.RoiCollection.ROIs.Clear();
        singleRoiSelect1.AddCenterRoi();
        lblFolder.Text = folderPath;
        progressBar1.Visible = false;
        UpdatePlotsBlocking(true, false);
    }

    void UpdatePlots() => UpdateNeeded = true;

    void UpdatePlotsBlocking(bool force, bool save)
    {
        if (TS is null)
            return;

        if (!UpdateNeeded && !force)
            return;

        if (singleRoiSelect1.RoiCollection.ROIs.Count == 0)
            return;

        DataRoi roi = singleRoiSelect1.GetDataRoi(0);
        Text = roi.ToString();

        AfuData5D data = TS.GetAfuData(roi);

        IndexRange baselineRange = new((int)nudB1.Value, (int)nudB2.Value);
        IndexRange measurementRange = new((int)nudM1.Value, (int)nudM2.Value);
        DffCurve[] sweeps = cbSubtract.Checked
            ? data.GetSweepsRelativeToFirst(baselineRange)
            : data.GetSweeps(baselineRange);

        formsPlot1.Reset();
        Plotting.PlotAfuCurves(formsPlot1.Plot, sweeps, baselineRange, measurementRange);
        formsPlot1.Refresh();

        formsPlot2.Reset();
        Plotting.PlotDffCurves(formsPlot2.Plot, sweeps, baselineRange, measurementRange);
        formsPlot2.Refresh();

        formsPlot3.Reset();
        Plotting.PlotMeans(formsPlot3.Plot, sweeps, measurementRange);
        foreach (var sp in formsPlot3.Plot.GetPlottables<ScottPlot.Plottables.Scatter>())
        {
            sp.LineWidth = 2;
            sp.MarkerSize = 10;
        }
        formsPlot3.Refresh();

        if (save)
        {
            string saveFolder = Path.Combine(TS.Path, "Analysis");
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            SaveDffCsv(saveFolder, sweeps, roi);
            SavePointsCsv(saveFolder, sweeps, roi, measurementRange);
        }

        UpdateNeeded = false;
    }

    private void SaveDffCsv(string saveFolder, DffCurve[] sweeps, DataRoi roi)
    {
        if (TS is null)
            return;

        SWHarden.CsvBuilder.CsvBuilder dffCsv = new();
        dffCsv.Add("Time", "Sec", "", TS.FrameTimes);
        for (int i = 0; i < TS.Sweeps; i++)
        {
            dffCsv.Add($"Sweep {i + 1}", "dF/F %", "", sweeps[i].DFFs);
        }
        string tSeriesName = Path.GetFileName(TS.Path);
        string saveAs = Path.Join(saveFolder, $"dff-{tSeriesName}.csv");
        dffCsv.SaveAs(saveAs, true, false, false);
        string json =
            """
                {
                  "Version": "3.3.5",
                  "Generated": "{{NOW}}",
                  "Folder": "{{FOLDER}}",
                  "Roi": "{{ROI}}"
                }
                """
            .Replace("{{NOW}}", DateTime.Now.ToString())
            .Replace("{{FOLDER}}", TS.Path.Replace("\\", "/"))
            .Replace("{{ROI}}", roi.ToString());

        File.WriteAllText(saveAs + ".json", json);
        Clipboard.SetText($"LoadCSV \"{saveAs}\"");
    }

    private void SavePointsCsv(string saveFolder, DffCurve[] sweeps, DataRoi roi, IndexRange measureRange)
    {
        if (TS is null)
            return;

        double[] xs = Enumerable.Range(0, sweeps.Length).Select(x => (double)x).ToArray();
        double[] meansBySweep = sweeps.Select(x => x.GetMean(measureRange)).ToArray();

        SWHarden.CsvBuilder.CsvBuilder dffCsv = new();
        dffCsv.Add("Sweep", "#", "", xs);
        dffCsv.Add("Mean", "dFF", "", meansBySweep);

        string tSeriesName = Path.GetFileName(TS.Path);
        string saveAs = Path.Join(saveFolder, $"mean-{tSeriesName}.csv");
        dffCsv.SaveAs(saveAs, true, false, false);
        string json =
            """
                {
                  "Version": "3.3.5",
                  "Generated": "{{NOW}}",
                  "Folder": "{{FOLDER}}",
                  "Roi": "{{ROI}}"
                }
                """
            .Replace("{{NOW}}", DateTime.Now.ToString())
            .Replace("{{FOLDER}}", TS.Path.Replace("\\", "/"))
            .Replace("{{ROI}}", roi.ToString());

        File.WriteAllText(saveAs + ".json", json);
        Clipboard.SetText($"LoadCSV \"{saveAs}\"");
    }
}
