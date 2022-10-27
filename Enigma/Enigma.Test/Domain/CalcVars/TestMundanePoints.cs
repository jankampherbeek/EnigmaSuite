// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Test.Domain.CalcVars;

[TestFixture]
public class TestMundanePoints
{
    private IMundanePointSpecifications specifications;

    [SetUp]
    public void SetUp()
    {
        specifications = new MundanePointSpecifications();
    }


    [Test]
    public void TestRetrievingDetails()
    {
        MundanePointDetails details = specifications.DetailsForPoint(MundanePoints.Ascendant);
        Assert.IsNotNull(details);
        Assert.That(details.MundanePoint, Is.EqualTo(MundanePoints.Ascendant));
        Assert.That(details.TextId, Is.EqualTo("ref.enum.mundanepoint.id.asc"));
        Assert.That(details.TextIdAbbreviated, Is.EqualTo("ref.enum.mundanepoint.idabbr.asc"));
    }

    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (MundanePoints point in Enum.GetValues(typeof(MundanePoints)))
        {
            MundanePointDetails details = specifications.DetailsForPoint(point);
            Assert.IsNotNull(details);
            Assert.IsTrue(details.TextId.Length > 0);
        }
    }

    [Test]
    public void TestRetrievingWithIndex()
    {
        int mundanePointIndex = 2;
        MundanePointDetails mundanePointDetails = specifications.DetailsForPoint(mundanePointIndex);
        Assert.IsNotNull(mundanePointDetails);
        Assert.That(mundanePointDetails.MundanePoint, Is.EqualTo(MundanePoints.EastPoint));        

    }

    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int mundanePointIndex = 300;
        Assert.That(() => _ = specifications.DetailsForPoint(mundanePointIndex), Throws.TypeOf<ArgumentException>());
    }


}