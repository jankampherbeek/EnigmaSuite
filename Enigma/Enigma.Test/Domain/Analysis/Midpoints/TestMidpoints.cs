// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;

namespace Enigma.Test.Domain.Analysis.Midpoints;

[TestFixture]
public class TestMidpointSpecifications
{
    [Test]
    public void TestRetrievingDetails()
    {
        MidpointTypes midpoint = MidpointTypes.Dial90;
        IMidpointSpecifications specifications = new MidpointSpecifications();

        MidpointDetails details = specifications.DetailsForMidpoint(midpoint);
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.Midpoint, Is.EqualTo(MidpointTypes.Dial90));
            Assert.That(details.Division, Is.EqualTo(4));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        IMidpointSpecifications specifications = new MidpointSpecifications();
        foreach (MidpointTypes type in Enum.GetValues(typeof(MidpointTypes)))
        {
            MidpointDetails details = specifications.DetailsForMidpoint(type);
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }
}