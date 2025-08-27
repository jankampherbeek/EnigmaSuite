// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.PresentationFactories;

namespace Enigma.Test.Core.Slices.BlaSchema;

[TestFixture]
public class TestBlaElementsCrossesForDataGridFactory
{
    private BlaElementsCrossesForDataGridFactory _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new BlaElementsCrossesForDataGridFactory();
    }

    [Test]
    public void TestCreateBlaItemsForElementsCrosses_ReturnsCorrectNumberOfItems()
    {
        // Arrange
        var chartDetails = CreateSampleChartDetails();

        // Act
        var result = _factory.CreateBlaItemsForElementsCrosses(chartDetails);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(7)); // Cardinal, Fixed, Mutable, Fire, Earth, Air, Water
    }

    [Test]
    public void TestCreateBlaItemsForElementsCrosses_ContainsExpectedCategories()
    {
        // Arrange
        var chartDetails = CreateSampleChartDetails();

        // Act
        var result = _factory.CreateBlaItemsForElementsCrosses(chartDetails);

        // Assert
        var names = result.Select(r => r.Name).ToList();
        Assert.Multiple(() =>
        {
            Assert.That(names, Does.Contain("Cardinal"));
            Assert.That(names, Does.Contain("Fixed"));
            Assert.That(names, Does.Contain("Mutable"));
            Assert.That(names, Does.Contain("Fire"));
            Assert.That(names, Does.Contain("Earth"));
            Assert.That(names, Does.Contain("Air"));
            Assert.That(names, Does.Contain("Water"));
        });
    }

    [Test]
    public void TestCreateBlaItemsForElementsCrosses_ValidatesDataStructure()
    {
        // Arrange
        var chartDetails = CreateSampleChartDetails();

        // Act
        var result = _factory.CreateBlaItemsForElementsCrosses(chartDetails);

        // Assert
        foreach (var item in result)
        {
            Assert.Multiple(() =>
            {
                Assert.That(item.Name, Is.Not.Null);
                Assert.That(item.Sign, Is.GreaterThanOrEqualTo(0));
                Assert.That(item.House, Is.GreaterThanOrEqualTo(0));
                Assert.That(item.Sum, Is.EqualTo(item.Sign + item.House));
                Assert.That(item.Hcusp, Is.EqualTo(1)); // Should always be 1 based on factory logic
                Assert.That(item.Total, Is.EqualTo(item.Sum + item.Hcusp));
            });
        }
    }

    [Test]
    public void TestCreateBlaItemsForElementsCrosses_EmptyChartDetails()
    {
        // Arrange
        var chartDetails = new ChartDetails(new List<BlaPositions>(), new List<int>(), new List<int>(), new Dictionary<ChartPoints, int>(), new Dictionary<ChartPoints, int>(), new Dictionary<int, List<RulerPair>>());

        // Act
        var result = _factory.CreateBlaItemsForElementsCrosses(chartDetails);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(7));
        
        // All counts should be 0 for empty chart
        foreach (var item in result)
        {
            Assert.Multiple(() =>
            {
                Assert.That(item.Sign, Is.EqualTo(0));
                Assert.That(item.House, Is.EqualTo(0));
                Assert.That(item.Sum, Is.EqualTo(0));
                Assert.That(item.Total, Is.EqualTo(1)); // Hcusp is always 1
            });
        }
    }

    private static ChartDetails CreateSampleChartDetails()
    {
        var positions = new List<BlaPositions>
        {
            new(ChartPoints.Sun, 0.0, 1, 1, 1),      // Aries, Mars decan, House 1 (Cardinal, Fire)
            new(ChartPoints.Moon, 30.0, 2, 4, 2),    // Taurus, Mercury decan, House 2 (Fixed, Earth)
            new(ChartPoints.Mercury, 60.0, 3, 7, 3), // Gemini, Jupiter decan, House 3 (Mutable, Air)
            new(ChartPoints.Venus, 90.0, 4, 3, 4),   // Cancer, Venus decan, House 4 (Cardinal, Water)
            new(ChartPoints.Mars, 120.0, 5, 6, 5),   // Leo, Saturn decan, House 5 (Fixed, Fire)
            new(ChartPoints.Jupiter, 150.0, 6, 2, 6), // Virgo, Sun decan, House 6 (Mutable, Earth)
            new(ChartPoints.Saturn, 180.0, 7, 5, 7),  // Libra, Moon decan, House 7 (Cardinal, Air)
            new(ChartPoints.Uranus, 210.0, 8, 1, 8),  // Scorpio, Mars decan, House 8 (Fixed, Water)
            new(ChartPoints.Neptune, 240.0, 9, 4, 9), // Sagittarius, Mercury decan, House 9 (Mutable, Fire)
            new(ChartPoints.Pluto, 270.0, 10, 7, 10), // Capricorn, Jupiter decan, House 10 (Cardinal, Earth)
            new(ChartPoints.Ascendant, 300.0, 11, 3, 11), // Aquarius, Venus decan, House 11 (Fixed, Air)
            new(ChartPoints.Mc, 330.0, 12, 6, 12)     // Pisces, Saturn decan, House 12 (Mutable, Water)
        };

        return new ChartDetails(positions, new List<int>(), new List<int>(), new Dictionary<ChartPoints, int>(), new Dictionary<ChartPoints, int>(), new Dictionary<int, List<RulerPair>>());
    }
}
