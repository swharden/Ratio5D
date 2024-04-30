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
    }

    void SetFolder(string folderPath)
    {
        TS = new TSeriesFolder(folderPath);
        singleRoiSelect1.SetImage(TS.GetProjectedRedBitmap());
        singleRoiSelect1.AddCenterRoi();
        lblFolder.Text = folderPath;
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

            SWHarden.CsvBuilder.CsvBuilder dffCsv = new();
            dffCsv.AddHeaderLine($"ROI {roi}");
            dffCsv.Add("Time", "Sec", "", TS.FrameTimes);
            for (int i = 0; i < TS.Sweeps; i++)
            {
                dffCsv.Add($"Sweep {i + 1}", "dF/F %", "", sweeps[i].DFFs);
            }

            string saveAs = Path.Join(saveFolder, "dff.csv");
            dffCsv.SaveAs(saveAs);
            Debug.WriteLine(saveAs);

            Clipboard.SetText($"LoadCSV \"{saveAs}\"");

            System.Diagnostics.Process.Start("explorer.exe", saveFolder);
        }

        UpdateNeeded = false;
    }
}
