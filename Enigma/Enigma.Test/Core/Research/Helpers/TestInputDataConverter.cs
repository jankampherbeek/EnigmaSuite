// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Research.Helpers;
using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Persistables;

namespace Enigma.Test.Core.Research.Helpers;

[TestFixture]
public class TestInputDataConverter
{
    private IInputDataConverter? _converter;
    private const double DELTA = 0.00000001;


    [SetUp]
    public void SetUp()
    {
        _converter = new InputDataConverter();
    }

    [Test]
    public void TestMarshallUnmarshallInputDataItem()
    {
        StandardInputItem inputItem1 = CreateStandardInputItem();
        string jsonText = _converter!.MarshallInputItem(inputItem1);
        StandardInputItem inputItem2 = _converter.UnMarshallInputItem(jsonText);
        Assert.That(inputItem2, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(inputItem2.Id, Is.EqualTo(inputItem1.Id));
            Assert.That(inputItem2.Name, Is.EqualTo(inputItem1.Name));
            Assert.That(inputItem2.GeoLatitude, Is.EqualTo(inputItem1.GeoLatitude).Within(DELTA));
            Assert.That(inputItem2.GeoLongitude, Is.EqualTo(inputItem1.GeoLongitude).Within(DELTA));
        });
    }

    [Test]
    public void TestMarshallUnmarshallStandardInput()
    {
        List<StandardInputItem> chartData = CreateMultipleStandardInputItems();
        StandardInput standardInput1 = new("test_data", "placeholder_for_date_time", chartData);
        string jsonText = _converter!.MarshallStandardInput(standardInput1);
        StandardInput standardInput2 = _converter.UnMarshallStandardInput(jsonText);
        Assert.That(standardInput2, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(standardInput2.DataName, Is.EqualTo(standardInput1.DataName));
            Assert.That(standardInput2.Creation, Is.EqualTo(standardInput1.Creation));
            Assert.That(standardInput2.ChartData[0].Name, Is.EqualTo(standardInput1.ChartData[0].Name));
            Assert.That(standardInput2.ChartData[0].Id, Is.EqualTo(standardInput1.ChartData[0].Id));
        });

    }

    private static StandardInputItem CreateStandardInputItem()
    {
        PersistableDate date = new(2022, 11, 6, "G");
        PersistableTime time = new(12, 2, 30, 1.0, 0.0);
        const string id = "123x";
        const string name = "item1";
        const double geoLongitude = 52.0;
        const double geoLatitude = 7.0;
        return new StandardInputItem(id, name, geoLongitude, geoLatitude, date, time);
    }

    private static List<StandardInputItem> CreateMultipleStandardInputItems()
    {
        PersistableDate date = new(2000, 1, 7, "G");
        PersistableTime time = new(18, 59, 30, 2.0, 1.0);
        const string id = "456b";
        const string name = "item2";
        const double geoLongitude = 32.0;
        const double geoLatitude = 17.0;

        List<StandardInputItem> items = new()
        {
            new StandardInputItem(id, name, geoLongitude, geoLatitude, date, time),
            CreateStandardInputItem()
        };
        return items;


    }

}