﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Frontend.Ui.Support;
using FakeItEasy;

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public class TestEventDataConverter
{
    private const double DELTA = 0.00000001;
    private IEventDataConverter? _eventDataConverter;
    private const string DESCRIPTION = "Event Description";
    private const string LOCATION_NAME = "Some location";
    private const int ID = 456;
    private const double JD_ET = 123456.789;
    private const string DATE_TEXT = "2023-02-2023";
    private const string TIME_TEXT = "21:24:30";
    private const string LOCATION_FULL_NAME = "Some location 12.5 N / 13.66666666667 W";
    private const double GEO_LONG = -13.66666666667;
    private const double GEO_LAT = 12.5;

    [SetUp]
    public void SetUp()
    {
        var locationConversionFake = A.Fake<ILocationConversion>();
        A.CallTo(() => locationConversionFake.CreateLocationDescription(LOCATION_NAME, GEO_LAT, GEO_LONG)).Returns(LOCATION_FULL_NAME);
        _eventDataConverter = new EventDataConverter(locationConversionFake);

    }


    [Test]
    public void TestEventDataToPersistableEventData()
    {
        PersistableEventData expected = CreatePersistableEventData();
        PersistableEventData result = _eventDataConverter!.ToPersistableEventData(CreateEventData());
        Assert.Multiple(() =>
        {
            Assert.That(expected.Description, Is.EqualTo(result.Description));
            Assert.That(expected.GeoLat, Is.EqualTo(result.GeoLat).Within(DELTA));
            Assert.That(expected.DateText, Is.EqualTo(DATE_TEXT));
            Assert.That(expected.JdForEt, Is.EqualTo(JD_ET).Within(DELTA));
        });
    }

    [Test]
    public void TestPersistableEventDataToEventData()
    {
        ProgEvent expected = CreateEventData();
        ProgEvent result = _eventDataConverter!.FromPersistableEventData(CreatePersistableEventData());
        Assert.Multiple(() =>
        {
            Assert.That(expected.Description, Is.EqualTo(result.Description));
            Assert.That(expected.Location.GeoLong, Is.EqualTo(result.Location.GeoLong).Within(DELTA));
            Assert.That(expected.DateTime.JulianDayForEt, Is.EqualTo(result.DateTime.JulianDayForEt).Within(DELTA));
        });
    }

    private static ProgEvent CreateEventData()
    {
        Location? location = new(LOCATION_FULL_NAME, GEO_LONG, GEO_LAT);
        FullDateTime fullDateTime = new(DATE_TEXT, TIME_TEXT, JD_ET);
        return new ProgEvent(ID, DESCRIPTION, LOCATION_NAME, location, fullDateTime);
    }

    private static PersistableEventData CreatePersistableEventData()
    {
        PersistableEventData persEventData = new();
        persEventData.Description = DESCRIPTION;
        persEventData.DateText = DATE_TEXT;
        persEventData.TimeText = TIME_TEXT;
        persEventData.JdForEt = JD_ET;
        persEventData.LocationName = LOCATION_FULL_NAME;
        persEventData.GeoLat = GEO_LAT;
        persEventData.GeoLong = GEO_LONG;
        return persEventData;
    }
}
