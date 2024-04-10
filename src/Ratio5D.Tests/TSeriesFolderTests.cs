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
        ts.FrameCount.Should().Be(100);
        ts.FramePeriod.Should().Be(0.067206472);
    }
}