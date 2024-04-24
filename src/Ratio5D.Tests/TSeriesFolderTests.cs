using System.Diagnostics;

namespace Ratio5D.Tests;

public class Tests
{
    [Test]
    public void Test_TSeriesFolder_Data()
    {
        TSeriesFolder ts = SampleData.TSeriesFolder;
        ts.ReferenceFiles.Should().NotBeEmpty();
        ts.TifFiles.Should().NotBeEmpty();
        ts.Sweeps.Should().Be(6);
        ts.FramesPerSweep.Should().Be(100);
        ts.FramePeriod.Should().Be(0.067206472);
    }

    [Test]
    public void Test_TSeriesFolder_SaveImage()
    {
        TSeriesFolder ts = new(SampleData.TSeriesFolderPath);
        Stopwatch sw = Stopwatch.StartNew();
        ts.SaveImageData();
        Console.WriteLine($"Saved in {sw.Elapsed.TotalSeconds:N2} seconds");
    }

    [Test]
    public void Test_TSeriesFolder_LoadSlow()
    {
        Stopwatch sw = Stopwatch.StartNew();
        TSeriesFolder ts = new(SampleData.TSeriesFolderPath);
        Console.WriteLine($"Loaded in {sw.Elapsed.TotalSeconds:N2} seconds");
    }

    [Test]
    public void Test_TSeriesFolder_LoadFast()
    {
        Stopwatch sw = Stopwatch.StartNew();
        TSeriesFolder ts = new(SampleData.TSeriesFolderPath, fast: true);
        Console.WriteLine($"Loaded in {sw.Elapsed.TotalSeconds:N2} seconds");
    }
}