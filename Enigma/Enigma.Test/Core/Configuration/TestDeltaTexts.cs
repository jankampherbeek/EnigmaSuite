// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Configuration;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Configuration;

[TestFixture]
public class TestDeltaTexts
{
    private readonly IDeltaTexts _deltaTexts = new DeltaTexts();

    [Test]
    public void TestChartPoint()
    {
        const ChartPoints chartPoint = ChartPoints.Sun;
        const bool isUsed = true;
        const char glyph = 'a';
        const int percentageOrb = 100;
        const bool showInChart = true;
        ChartPointConfigSpecs specs = new(isUsed, glyph, percentageOrb, showInChart);
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForPoint(chartPoint, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("CP_0"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("y||a||100||y"));
        });
    }

    [Test]
    public void TestChartPointHighIndex()
    {
        const ChartPoints chartPoint = ChartPoints.Vertex;
        const bool isUsed = true;
        const char glyph = ' ';
        const int percentageOrb = 50;
        const bool showInChart = false;
        ChartPointConfigSpecs specs = new(isUsed, glyph, percentageOrb, showInChart);
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForPoint(chartPoint, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("CP_1004"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("y|| ||50||n"));
        });
    }

    [Test]
    public void TestAspect()
    {
        const AspectTypes aspect = AspectTypes.Opposition;
        const bool isUsed = true;
        const char glyph = 'C';
        const int percentageOrb = 100;
        const bool showInChart = true;
        AspectConfigSpecs specs = new(isUsed, glyph, percentageOrb, showInChart);
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForAspect(aspect, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("AT_1"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("y||C||100||y"));
        });
    }

    [Test]
    public void TestAspectNotUsed()
    {
        AspectTypes aspect = AspectTypes.Vigintile;
        const bool isUsed = false;
        const char glyph = 'Ï';
        const int percentageOrb = 0;
        const bool showInChart = false;
        AspectConfigSpecs specs = new(isUsed, glyph, percentageOrb, showInChart);
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForAspect(aspect, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("AT_21"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("n||Ï||0||n"));
        });
    }

    [Test]
    public void TestProgTransitOrb()
    {
        const ProgresMethods method = ProgresMethods.Transits;
        const double orb = 1.2;
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForProgOrb(method, orb);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("TR_ORB"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("1.2"));
        });
    }

    [Test]
    public void TestProgSecDirPoint()
    {
        const ProgresMethods method = ProgresMethods.Secondary;
        const ChartPoints chartPoint = ChartPoints.Moon;
        ProgPointConfigSpecs specs = new(true, 'b');
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForProgChartPoint(method, chartPoint, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("SC_CP_1"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("y||b"));
        });
    }

    [Test]
    public void TestSymbolicKey()
    {
        const SymbolicKeys timeKey = SymbolicKeys.MeanSun;   // index 3
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForProgSymKey(timeKey);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("SM_KEY"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("2"));
        });
    }
}