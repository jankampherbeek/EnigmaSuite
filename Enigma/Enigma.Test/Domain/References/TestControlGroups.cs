// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.References;

namespace Enigma.Test.Domain.References;

[TestFixture]
public class TestControlGroupTypes
{


    [Test]
    public void TestRetrievingDetails()
    {
        const ControlGroupTypes controlGroupType = ControlGroupTypes.StandardShift;
        ControlGroupTypeDetails details = controlGroupType.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.ControlGroupType, Is.EqualTo(controlGroupType));
            Assert.That(details.Text, Is.EqualTo("Standard shifting of location, date, and time"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ControlGroupTypes controlGroupType in Enum.GetValues(typeof(ControlGroupTypes)))
        {
            ControlGroupTypeDetails details = controlGroupType.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Text, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        const int controlGroupTypeIndex = 0;
        ControlGroupTypes controlGroupType = ControlGroupTypesExtensions.ControlGroupTypeForIndex(controlGroupTypeIndex);
        Assert.That(controlGroupType, Is.EqualTo(ControlGroupTypes.StandardShift));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        const int controlGroupTypeIndex = 500;
        Assert.That(() => _ = ControlGroupTypesExtensions.ControlGroupTypeForIndex(controlGroupTypeIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllControlGroupTypeDetails()
    {
        List<ControlGroupTypeDetails> allDetails = ControlGroupTypesExtensions.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(1));
            Assert.That(allDetails[0].ControlGroupType, Is.EqualTo(ControlGroupTypes.StandardShift));
        });
    }
}