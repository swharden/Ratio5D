using Ratio5D.Core;
using SWHarden.RoiSelect.WinForms;
using System.Diagnostics;

namespace Ratio5D.Gui;

public partial class Form2 : Form
{
    AfuData5D? AfuData = null;

    public Form2()
    {
        InitializeComponent();

        nudB1.ValueChanged += (s, e) => UpdatePlots();
        nudB2.ValueChanged += (s, e) => UpdatePlots();
        nudM1.ValueChanged += (s, e) => UpdatePlots();
        nudM2.ValueChanged += (s, e) => UpdatePlots();

        singleRoiSelect1.RoiCollection.SelectedRoiChanged += (s, e) =>
        {
            UpdatePlots();
        };

        SetFolder(Core.SampleData.TSeriesFolderPath);
    }

    void SetFolder(string folderPath)
    {
        lblFolder.Text = folderPath;
        TSeriesFolder ts = new(folderPath);
        AfuData = ts.GetAfuData();
        singleRoiSelect1.SetImage(ts.GetProjectedRedBitmap());
        UpdatePlots();
    }

    void UpdatePlots()
    {
        if (AfuData is null)
            return;

        DataRoi roi = singleRoiSelect1.GetSingleDataRoi();
        Text = roi.ToString();

        IndexRange baselineRange = new((int)nudB1.Value, (int)nudB2.Value);
        IndexRange measurementRange = new((int)nudM1.Value, (int)nudM2.Value);
        DffCurve[] sweeps = AfuData.GetSweeps(baselineRange);

        formsPlot1.Reset();
        Plotting.PlotAfuCurves(formsPlot1.Plot, sweeps, baselineRange, measurementRange);
        formsPlot1.Refresh();

        formsPlot2.Reset();
        Plotting.PlotDffCurves(formsPlot2.Plot, sweeps, baselineRange, measurementRange);
        formsPlot2.Refresh();

        formsPlot3.Reset();
        Plotting.PlotMeans(formsPlot3.Plot, sweeps, measurementRange);
        formsPlot3.Refresh();
    }
}
