// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Support;
using Moq;

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public class TestChartDataConverter
{
    private const double DELTA = 0.00000001;
    private IChartDataConverter? _chartDataConverter;
    private const string NAME = "Chart Name";
    private const string DESCRIPTION = "Chart Description";
    private const string SOURCE = "Chart Source";
    private const string LOCATION_NAME = "Some _location";
    private const ChartCategories CHART_CATEGORY = ChartCategories.Election;
    private const RoddenRatings RATING = RoddenRatings.DD;
    private const int ID = 123;
    private const double JD_ET = 123456.789;
    private const string DATE_TEXT = "2023-02-2023";
    private const string TIME_TEXT = "21:24:30";
    private const string LOCATION_FULL_NAME = "Some _location 12.5 N / 13.66666666667 W";
    private const double GEO_LONG = -13.66666666667;
    private const double GEO_LAT = 12.5;

    [SetUp]
    public void SetUp()
    {
        var locationConversionMock = new Mock<ILocationConversion>();
        locationConversionMock.Setup(p => p.CreateLocationDescription(LOCATION_NAME, GEO_LAT, GEO_LONG)).Returns(LOCATION_FULL_NAME);
        _chartDataConverter = new ChartDataConverter(locationConversionMock.Object);

    }


    [Test]
    public void TestChartDataToPersistableChartData()
    {
        PersistableChartData expected = CreatePersistableChartData();
        PersistableChartData result = _chartDataConverter!.ToPersistableChartData(CreateChartData());
        Assert.Multiple(() =>
        {
            Assert.That(expected.Name, Is.EqualTo(result.Name));
            Assert.That(expected.GeoLat, Is.EqualTo(result.GeoLat).Within(DELTA));
            Assert.That(expected.DateText, Is.EqualTo(DATE_TEXT));
            Assert.That(expected.JulianDayEt, Is.EqualTo(JD_ET).Within(DELTA));
        });
    }

    [Test]
    public void TestPersistableChartDataToChartData()
    {
        ChartData expected = CreateChartData();
        ChartData result = _chartDataConverter!.FromPersistableChartData(CreatePersistableChartData());
        Assert.Multiple(() =>
        {
            Assert.That(expected.MetaData.Name, Is.EqualTo(result.MetaData.Name));
            Assert.That(expected.Location.GeoLong, Is.EqualTo(result.Location.GeoLong).Within(DELTA));
            Assert.That(expected.FullDateTime.JulianDayForEt, Is.EqualTo(result.FullDateTime.JulianDayForEt).Within(DELTA));
        });
    }

    private static ChartData CreateChartData()
    {
        MetaData metaData = new(NAME, DESCRIPTION, SOURCE, LOCATION_NAME, CHART_CATEGORY, RATING);
        Location location = new(LOCATION_FULL_NAME, GEO_LONG, GEO_LAT);
        FullDateTime fullDateTime = new(DATE_TEXT, TIME_TEXT, JD_ET);
        return new ChartData(ID, metaData, location, fullDateTime);
    }

    private static PersistableChartData CreatePersistableChartData()
    {
        return new PersistableChartData(
            NAME,
            DESCRIPTION,
            SOURCE,
            CHART_CATEGORY,
            RATING,
            JD_ET,
            DATE_TEXT,
            TIME_TEXT,
            LOCATION_FULL_NAME,
            GEO_LONG,
            GEO_LAT
        );
    }
}

