// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestHeliacalObjects
{
    
    [Test]
    public void TestRetrievingDetails()
    {
        const HeliacalObjects heliacalObject = HeliacalObjects.Mars;
        HeliacalObjectDetails details = heliacalObject.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.ChartPoint, Is.EqualTo(ChartPoints.Mars));
            Assert.That(details.HeliacalObject, Is.EqualTo(HeliacalObjects.Mars));
        });
    }
    
    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (HeliacalObjects heliacalObject in Enum.GetValues(typeof(HeliacalObjects)))
        {
            HeliacalObjectDetails details = heliacalObject.GetDetails();
            Assert.That(details, Is.Not.Null);
        }
    }
    
    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 2;
        HeliacalObjects heliacalObject = HeliacalObjectsExtensions.HeliacalObjectForIndex(index);
        Assert.That(heliacalObject, Is.EqualTo(HeliacalObjects.Venus));
    }    
    
    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 300;
        Assert.That(() => 
            _ = HeliacalObjectsExtensions.HeliacalObjectForIndex(index), Throws.TypeOf<ArgumentException>());
    }

}