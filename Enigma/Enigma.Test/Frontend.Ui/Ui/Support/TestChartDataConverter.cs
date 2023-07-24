// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.SUpport;
using Moq;

namespace Enigma.Test.Frontend.Ui.Ui.Support;

[TestFixture]
public class TestChartDataConverter
{
    private const double Delta = 0.00000001;
    private IChartDataConverter _chartDataConverter;
    private const string Name = "Chart Name";
    private const string Description = "Chart Description";
    private const string Source = "Chart Source";
    private const string LocationName = "Some _location";
    private const ChartCategories ChartCategory = ChartCategories.Election;
    private const RoddenRatings Rating = RoddenRatings.DD;
    private const int Id = 123;
    private const double JdEt = 123456.789;
    private const string DateText = "2023-02-2023";
    private const string TimeText = "21:24:30";
    private const string LocationFullName = "Some _location 12.5 N / 13.66666666667 W";
    private const double GeoLong = -13.66666666667;
    private const double GeoLat = 12.5;

    [SetUp]
    public void SetUp()
    {
        var locationConversionMock = new Mock<ILocationConversion>();
        locationConversionMock.Setup(p => p.CreateLocationDescription(LocationName, GeoLat, GeoLong)).Returns(LocationFullName);
        _chartDataConverter = new ChartDataConverter(locationConversionMock.Object);

    }


    [Test]
    public void TestChartDataToPersistableChartData()
    {
        PersistableChartData expected = CreatePersistableChartData();
        PersistableChartData result = _chartDataConverter.ToPersistableChartData(CreateChartData());
        Assert.Multiple(() =>
        {
            Assert.That(expected.Name, Is.EqualTo(result.Name));
            Assert.That(expected.GeoLat, Is.EqualTo(result.GeoLat).Within(Delta));
            Assert.That(expected.DateText, Is.EqualTo(DateText));
            Assert.That(expected.JulianDayEt, Is.EqualTo(JdEt).Within(Delta));
        });
    }

    [Test]
    public void TestPersistableChartDataToChartData()
    {
        ChartData expected = CreateChartData();
        ChartData result = _chartDataConverter.FromPersistableChartData(CreatePersistableChartData());
        Assert.Multiple(() =>
        {
            Assert.That(expected.MetaData.Name, Is.EqualTo(result.MetaData.Name));
            Assert.That(expected.Location.GeoLong, Is.EqualTo(result.Location.GeoLong).Within(Delta));
            Assert.That(expected.FullDateTime.JulianDayForEt, Is.EqualTo(result.FullDateTime.JulianDayForEt).Within(Delta));
        });
    }

    private static ChartData CreateChartData()
    {
        MetaData metaData = new(Name, Description, Source, LocationName, ChartCategory, Rating);
        Location location = new(LocationFullName, GeoLong, GeoLat);
        FullDateTime fullDateTime = new(DateText, TimeText, JdEt);
        return new ChartData(Id, metaData, location, fullDateTime);
    }

    private static PersistableChartData CreatePersistableChartData()
    {
        return new PersistableChartData(
            Name,
            Description,
            Source,
            ChartCategory,
            Rating,
            JdEt,
            DateText,
            TimeText,
            LocationFullName,
            GeoLong,
            GeoLat
        );
    }
}

