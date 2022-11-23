// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Helpers.Conversions;
using Enigma.Core.Helpers.Interfaces;
using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.Persistency;

namespace Enigma.Test.Core.Work.Research;

[TestFixture]
public class TestInputDataConverter
{
    private IInputDataConverter _converter;
    private readonly double _delta = 0.00000001;


    [SetUp]
    public void SetUp()
    {
        _converter = new InputDataConverter();

    }

    [Test]
    public void TestMarshallUnmarshallInputDataItem()
    {
        StandardInputItem inputItem1 = CreateStandardInputItem();
        string jsonText = _converter.MarshallInputItem(inputItem1);
        StandardInputItem inputItem2 = _converter.UnMarshallInputItem(jsonText);
        Assert.That(inputItem2, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(inputItem2.Id, Is.EqualTo(inputItem1.Id));
            Assert.That(inputItem2.Name, Is.EqualTo(inputItem1.Name));
            Assert.That(inputItem2.GeoLatitude, Is.EqualTo(inputItem1.GeoLatitude).Within(_delta));
            Assert.That(inputItem2.GeoLongitude, Is.EqualTo(inputItem1.GeoLongitude).Within(_delta));
            Assert.That(inputItem2.Date.Day, Is.EqualTo(inputItem1.Date.Day));
            Assert.That(inputItem2.Time.Minute, Is.EqualTo(inputItem1.Time.Minute));
        });
    }

    [Test]
    public void TestMarshallUnmarshallStandardInput()
    {
        List<StandardInputItem> chartData = CreateMultipleStandardInputItems();
        StandardInput standardInput1 = new("test_data", "placeholder_for_date_time", chartData);
        string jsonText = _converter.MarshallStandardInput(standardInput1);
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
        string id = "123x";
        string name = "item1";
        double geoLongitude = 52.0;
        double geoLatitude = 7.0;
        return new StandardInputItem(id, name, geoLongitude, geoLatitude, date, time);
    }

    private static List<StandardInputItem> CreateMultipleStandardInputItems()
    {
        PersistableDate date = new(2000, 1, 7, "G");
        PersistableTime time = new(18, 59, 30, 2.0, 1.0);
        string id = "456b";
        string name = "item2";
        double geoLongitude = 32.0;
        double geoLatitude = 17.0;

        List<StandardInputItem> items = new()
        {
            new StandardInputItem(id, name, geoLongitude, geoLatitude, date, time),
            CreateStandardInputItem()
        };
        return items;


    }

}