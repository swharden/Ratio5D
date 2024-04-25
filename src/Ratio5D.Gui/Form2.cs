using Ratio5D.Core;
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

        SetFolder(Core.SampleData.TSeriesFolderPath);
    }

    void SetFolder(string folderPath)
    {
        Stopwatch sw = Stopwatch.StartNew();

        Debug.WriteLine($"{sw.Elapsed.TotalSeconds:N3} Loading TIFs...");
        lblFolder.Text = folderPath;
        TSeriesFolder ts = new(folderPath);

        Debug.WriteLine($"{sw.Elapsed.TotalSeconds:N3} Genearting AFU dat...");
        AfuData = ts.GetAfuData();

        Debug.WriteLine($"{sw.Elapsed.TotalSeconds:N3} Genearting projection image...");
        Bitmap bmp = ts.GetProjectedRedBitmap();
        multiRoiSelect1.SetImage(bmp);

        Debug.WriteLine($"{sw.Elapsed.TotalSeconds:N3} Updating plots...");
        UpdatePlots();

        Debug.WriteLine($"{sw.Elapsed.TotalSeconds:N3} Loading finished.");
    }

    void UpdatePlots()
    {
        if (AfuData is null)
            return;

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
