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

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public class TestChartDataConverter
{
    private readonly double _delta = 0.00000001;
    private IChartDataConverter _chartDataConverter;
    private readonly string _name = "Chart Name";
    private readonly string _description = "Chart Description";
    private readonly string _source = "Chart Source";
    private readonly string _locationName = "Some location";
    private readonly ChartCategories _chartCategory = ChartCategories.Election;
    private readonly RoddenRatings _rating = RoddenRatings.DD;
    private readonly int _id = 123;
    private readonly double _jdEt = 123456.789;
    private readonly string _dateText = "2023-02-2023";
    private readonly string _timeText = "21:24:30";
    private readonly string _locationFullName = "Some location 12.5 N / 13.66666666667 W";
    private readonly double _geoLong = -13.66666666667;
    private readonly double _geoLat = 12.5;

    [SetUp]
    public void SetUp()
    {
        var locationConversionMock = new Mock<ILocationConversion>();
        locationConversionMock.Setup(p => p.CreateLocationDescription(_locationName, _geoLat, _geoLong)).Returns(_locationFullName);
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
            Assert.That(expected.GeoLat, Is.EqualTo(result.GeoLat).Within(_delta));
            Assert.That(expected.DateText, Is.EqualTo(_dateText));
            Assert.That(expected.JulianDayEt, Is.EqualTo(_jdEt).Within(_delta));
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
            Assert.That(expected.Location.GeoLong, Is.EqualTo(result.Location.GeoLong).Within(_delta));
            Assert.That(expected.FullDateTime.JulianDayForEt, Is.EqualTo(result.FullDateTime.JulianDayForEt).Within(_delta));
            Assert.That(expected.Id, Is.EqualTo(result.Id));
        });
    }

    private ChartData CreateChartData()
    {
        MetaData metaData = new(_name, _description, _source, _locationName, _chartCategory, _rating);
        string locationFullName = _locationFullName;
        Location location = new(locationFullName, _geoLong, _geoLat);
        FullDateTime fullDateTime = new(_dateText, _timeText, _jdEt);
        return new ChartData(_id, 0, metaData, location, fullDateTime);
    }

    private PersistableChartData CreatePersistableChartData()
    {
        return new PersistableChartData(
            _id,
            _name,
            _description,
            _source,
            _chartCategory,
            _rating,
            _jdEt,
            _dateText,
            _timeText,
            _locationFullName,
            _geoLong,
            _geoLat
        );
    }
}

