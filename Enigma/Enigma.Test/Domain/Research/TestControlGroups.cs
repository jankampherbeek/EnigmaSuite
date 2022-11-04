// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Engima.Domain.Research;

namespace Enigma.Test.Domain.Research;

[TestFixture]
public class TestControlGroupTypes
{
    private IControlGroupTypeSpecifications specifications;

    [SetUp]
    public void SetUp()
    {
        specifications = new ControlGroupTypeSpecifications();
    }


    [Test]
    public void TestRetrievingDetails()
    {
        ControlGroupTypes controlGroupType = ControlGroupTypes.StandardShift;
        ControlGroupTypeDetails details = specifications.DetailsForControlGroupType(controlGroupType);
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.ControlGroupType, Is.EqualTo(controlGroupType));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.controlgrouptypes.standardshift"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ControlGroupTypes controlGroupType in Enum.GetValues(typeof(ControlGroupTypes)))
        {
            ControlGroupTypeDetails details = specifications.DetailsForControlGroupType(controlGroupType);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int controlGroupTypeIndex = 2;
        ControlGroupTypes controlGroupType = specifications.ControlGroupTypeForIndex(controlGroupTypeIndex);
        Assert.That(controlGroupType, Is.EqualTo(ControlGroupTypes.GroupMemberShift));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int controlGroupTypeIndex = 500;
        Assert.That(() => _ = specifications.ControlGroupTypeForIndex(controlGroupTypeIndex), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void TestAllControlGroupTypeDetails()
    {
        List<ControlGroupTypeDetails> allDetails = specifications.AllControlGroupTypeDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(2));
            Assert.That(allDetails[0].ControlGroupType, Is.EqualTo(ControlGroupTypes.StandardShift));
            Assert.That(allDetails[1].ControlGroupType, Is.EqualTo(ControlGroupTypes.GroupMemberShift));
        });
    }
}