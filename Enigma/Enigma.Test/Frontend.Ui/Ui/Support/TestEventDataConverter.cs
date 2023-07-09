// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Persistency;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.SUpport;
using Moq;

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public class TestEventDataConverter
{
    private readonly double _delta = 0.00000001;
    private IEventDataConverter _eventDataConverter;
    private readonly string _description = "Event Description";
    private readonly string _locationName = "Some location";
    private const int ID = 456;
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
        _eventDataConverter = new EventDataConverter(locationConversionMock.Object);

    }


    [Test]
    public void TestEventDataToPersistableEventData()
    {
        PersistableEventData expected = CreatePersistableEventData();
        PersistableEventData result = _eventDataConverter.ToPersistableEventData(CreateEventData());
        Assert.Multiple(() =>
        {
            Assert.That(expected.Description, Is.EqualTo(result.Description));
            Assert.That(expected.GeoLat, Is.EqualTo(result.GeoLat).Within(_delta));
            Assert.That(expected.DateText, Is.EqualTo(_dateText));
            Assert.That(expected.JulianDayEt, Is.EqualTo(_jdEt).Within(_delta));
        });
    }

    [Test]
    public void TestPersistableEventDataToEventData()
    {
        EventData expected = CreateEventData();
        EventData result = _eventDataConverter.FromPersistableEventData(CreatePersistableEventData());
        Assert.Multiple(() =>
        {
            Assert.That(expected.Description, Is.EqualTo(result.Description));
            Assert.That(expected.Location.GeoLong, Is.EqualTo(result.Location.GeoLong).Within(_delta));
            Assert.That(expected.FullDateTime.JulianDayForEt, Is.EqualTo(result.FullDateTime.JulianDayForEt).Within(_delta));
        });
    }

    private EventData CreateEventData()
    {
        string locationFullName = _locationFullName;
        Location location = new(locationFullName, _geoLong, _geoLat);
        FullDateTime fullDateTime = new(_dateText, _timeText, _jdEt);
        return new EventData(ID, _description, _locationName, location, fullDateTime);
    }

    private PersistableEventData CreatePersistableEventData()
    {
        return new PersistableEventData(
            _description,
            _jdEt,
            _dateText,
            _timeText,
            _locationFullName,
            _geoLong,
            _geoLat
        );
    }
}
