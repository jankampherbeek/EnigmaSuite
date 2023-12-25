// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Configuration;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using MaterialDesignThemes.Wpf.Converters;

namespace Enigma.Test.Core.Configuration;

[TestFixture]
public class TestDeltaTexts
{
    private readonly IDeltaTexts _deltaTexts = new DeltaTexts();

    [Test]
    public void TestChartPoint()
    {
        ChartPoints chartPoint = ChartPoints.Sun;
        bool isUsed = true;
        char glyph = 'a';
        int percentageOrb = 100;
        bool showInChart = true;
        ChartPointConfigSpecs specs = new(isUsed, glyph, percentageOrb, showInChart);
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForPoint(chartPoint, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("CP_0"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("y||100||a||y"));
        });
    }

    [Test]
    public void TestChartPointHighIndex()
    {
        ChartPoints chartPoint = ChartPoints.Vertex;
        bool isUsed = true;
        char glyph = ' ';
        int percentageOrb = 50;
        bool showInChart = false;
        ChartPointConfigSpecs specs = new(isUsed, glyph, percentageOrb, showInChart);
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForPoint(chartPoint, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("CP_1004"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("y||50|| ||n"));
        });
    }

    [Test]
    public void TestAspect()
    {
        AspectTypes aspect = AspectTypes.Opposition;
        bool isUsed = true;
        char glyph = 'C';
        int percentageOrb = 100;
        bool showInChart = true;
        AspectConfigSpecs specs = new(isUsed, glyph, percentageOrb, showInChart);
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForAspect(aspect, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("AT_1"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("y||100||C||y"));
        });
    }

    [Test]
    public void TestAspectNotUsed()
    {
        AspectTypes aspect = AspectTypes.Vigintile;
        bool isUsed = false;
        char glyph = 'Ï';
        int percentageOrb = 0;
        bool showInChart = false;
        AspectConfigSpecs specs = new(isUsed, glyph, percentageOrb, showInChart);
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForAspect(aspect, specs);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("AT_21"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("n||0||Ï||n"));
        });
    }

    [Test]
    public void TestProgTransitOrb()
    {
        ProgresMethods method = ProgresMethods.Transits;
        double orb = 1.2;
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
        ProgresMethods method = ProgresMethods.Secundary;
        ChartPoints chartPoint = ChartPoints.Moon;
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
        SymbolicKeys timeKey = SymbolicKeys.MeanSun;   // index 3
        Tuple<string, string> deltaTexts = _deltaTexts.CreateDeltaForProgSymKey(timeKey);
        Assert.Multiple(() =>
        {
            Assert.That(deltaTexts.Item1, Is.EqualTo("SM_KEY"));
            Assert.That(deltaTexts.Item2, Is.EqualTo("3"));
        });
    }
}