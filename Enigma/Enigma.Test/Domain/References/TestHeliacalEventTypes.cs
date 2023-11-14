// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestHeliacalEventTypes
{
    
    [Test]
    public void TestRetrievingDetails()
    {
        const HeliacalEventTypes eventType = HeliacalEventTypes.HeliacalSetting;
        HeliacalEventTypeDetails details = eventType.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.IndexForSe, Is.EqualTo(EnigmaConstants.SE_HELIACAL_SETTING));
            Assert.That(details.EventType, Is.EqualTo(HeliacalEventTypes.HeliacalSetting));
        });
    }
    
    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (HeliacalEventTypes eventType in Enum.GetValues(typeof(HeliacalEventTypes)))
        {
            HeliacalEventTypeDetails details = eventType.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }
    
    [Test]
    public void TestRetrievingWithIndex()
    {
        const int index = 2;
        HeliacalEventTypes eventType = HeliacalEventTypesExtensions.EventTypeForIndex(index);
        Assert.That(eventType, Is.EqualTo(HeliacalEventTypes.EveningFirst));
    }    
    
    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int index = 300;
        Assert.That(() => _ = HeliacalEventTypesExtensions.EventTypeForIndex(index), Throws.TypeOf<ArgumentException>());
    }
    
}


