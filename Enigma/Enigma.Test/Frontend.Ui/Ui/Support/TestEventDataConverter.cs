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

namespace Enigma.Test.Frontend.Ui.Ui.Support;

[TestFixture]
public class TestEventDataConverter
{
    private const double Delta = 0.00000001;
    private IEventDataConverter? _eventDataConverter;
    private const string Description = "Event Description";
    private const string LocationName = "Some location";
    private const int Id = 456;
    private const double JdEt = 123456.789;
    private const string DateText = "2023-02-2023";
    private const string TimeText = "21:24:30";
    private const string LocationFullName = "Some location 12.5 N / 13.66666666667 W";
    private const double GeoLong = -13.66666666667;
    private const double GeoLat = 12.5;

    [SetUp]
    public void SetUp()
    {
        var locationConversionMock = new Mock<ILocationConversion>();
        locationConversionMock.Setup(p => p.CreateLocationDescription(LocationName, GeoLat, GeoLong)).Returns(LocationFullName);
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
            Assert.That(expected.GeoLat, Is.EqualTo(result.GeoLat).Within(Delta));
            Assert.That(expected.DateText, Is.EqualTo(DateText));
            Assert.That(expected.JulianDayEt, Is.EqualTo(JdEt).Within(Delta));
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
            Assert.That(expected.Location.GeoLong, Is.EqualTo(result.Location.GeoLong).Within(Delta));
            Assert.That(expected.FullDateTime.JulianDayForEt, Is.EqualTo(result.FullDateTime.JulianDayForEt).Within(Delta));
        });
    }

    private static EventData CreateEventData()
    {
        Location location = new(LocationFullName, GeoLong, GeoLat);
        FullDateTime fullDateTime = new(DateText, TimeText, JdEt);
        return new EventData(Id, Description, LocationName, location, fullDateTime);
    }

    private static PersistableEventData CreatePersistableEventData()
    {
        return new PersistableEventData(
            Description,
            JdEt,
            DateText,
            TimeText,
            LocationFullName,
            GeoLong,
            GeoLat
        );
    }
}
