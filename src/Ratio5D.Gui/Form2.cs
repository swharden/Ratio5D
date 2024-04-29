using Ratio5D.Core;
using SWHarden.RoiSelect.WinForms;
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

        roiSelect.RoiCollection.SelectedRoiChanged += (s, e) => UpdatePlots();

        SetFolder(Core.SampleData.TSeriesFolderPath);
        UpdateTimer.Tick += (s, e) => UpdatePlotsBlocking(false);
        UpdateTimer.Start();
    }

    void SetFolder(string folderPath)
    {
        TS = new TSeriesFolder(folderPath);
        roiSelect.SetImage(TS.GetProjectedRedBitmap());
        lblFolder.Text = folderPath;
        UpdatePlotsBlocking(true);
    }

    void UpdatePlots() => UpdateNeeded = true;

    void UpdatePlotsBlocking(bool force)
    {
        if (TS is null)
            return;

        if (!UpdateNeeded && !force)
            return;

        DataRoi roi = roiSelect.GetDataRoi(0);
        Text = roi.ToString();

        AfuData5D data = TS.GetAfuData(roi);

        IndexRange baselineRange = new((int)nudB1.Value, (int)nudB2.Value);
        IndexRange measurementRange = new((int)nudM1.Value, (int)nudM2.Value);
        DffCurve[] sweeps = data.GetSweeps(baselineRange);

        formsPlot1.Reset();
        Plotting.PlotAfuCurves(formsPlot1.Plot, sweeps, baselineRange, measurementRange);
        //formsPlot1.Plot.Axes.SetLimitsY(0, 10_000);
        formsPlot1.Refresh();

        formsPlot2.Reset();
        Plotting.PlotDffCurves(formsPlot2.Plot, sweeps, baselineRange, measurementRange);
        //formsPlot2.Plot.Axes.SetLimitsY(-50, 250);
        formsPlot2.Refresh();

        formsPlot3.Reset();
        Plotting.PlotMeans(formsPlot3.Plot, sweeps, measurementRange);
        foreach (var sp in formsPlot3.Plot.GetPlottables<ScottPlot.Plottables.Scatter>())
        {
            sp.LineWidth = 2;
            sp.MarkerSize = 10;
        }
        //formsPlot3.Plot.Axes.SetLimitsY(-20, 120);
        formsPlot3.Refresh();

        UpdateNeeded = false;
    }
}
