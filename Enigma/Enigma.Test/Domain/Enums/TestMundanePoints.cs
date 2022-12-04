// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Test.Domain.Enums;

[TestFixture]
public class TestMundanePoints
{

    [Test]
    public void TestRetrievingDetails()
    {
        MundanePointDetails details =  MundanePoints.Ascendant.GetDetails();
        Assert.Multiple(() =>
        {
            Assert.That(details, Is.Not.Null);
            Assert.That(details.MundanePoint, Is.EqualTo(MundanePoints.Ascendant));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.mundanepoint.id.asc"));
            Assert.That(details.TextIdAbbreviated, Is.EqualTo("ref.enum.mundanepoint.idabbr.asc"));
        });
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (MundanePoints point in Enum.GetValues(typeof(MundanePoints)))
        {
            MundanePointDetails details = MundanePoints.Mc.GetDetails();
            Assert.That(details, Is.Not.Null);
            Assert.That(details.TextId, Is.Not.Empty);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int mundanePointIndex = 2;
        MundanePoints mundanePoint = MundanePoints.Vertex.MundanePointForIndex(mundanePointIndex);
        Assert.That(mundanePoint, Is.EqualTo(MundanePoints.EastPoint));
    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int mundanePointIndex = 300;
        Assert.That(() => _ = MundanePoints.EastPoint.MundanePointForIndex(mundanePointIndex), Throws.TypeOf<ArgumentException>());
    }


}