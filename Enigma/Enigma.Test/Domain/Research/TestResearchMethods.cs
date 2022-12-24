// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Research;

namespace Enigma.Test.Domain.Research;


[TestFixture]
public class TestResearchMethods
{

    [Test]
    public void TestRetrievingDetails()
    {
        ResearchMethods method = ResearchMethods.CountAspects;
        ResearchMethodDetails details = method.GetDetails();
        Assert.That(details, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(details.ResearchMethod, Is.EqualTo(ResearchMethods.CountAspects));
            Assert.That(details.TextId, Is.EqualTo("ref.enum.researchmethods.countaspects"));
        });
    }


    [Test]
    public void TestAvailabilityOfDetailsForAllEnums()
    {
        foreach (ResearchMethods method in Enum.GetValues(typeof(ResearchMethods)))
        {
            if (method != ResearchMethods.None)
            {
                ResearchMethodDetails details = method.GetDetails();
                Assert.That(details, Is.Not.Null);
                Assert.That(details.TextId, Is.Not.Empty);
            }
        }
    }


    [Test]
    public void TestRetrievingWithIndex()
    {
        int index = 4;
        ResearchMethods method = ResearchMethods.None.ResearchMethodForIndex(index);
        Assert.That(method, Is.EqualTo(ResearchMethods.CountOccupiedMidpoints));
    }


    [Test]
    public void TestRetrievingWithWrongIndex()
    {
        int index = 5000;
        Assert.That(() => _ = ResearchMethods.None.ResearchMethodForIndex(index), Throws.TypeOf<ArgumentException>());
    }


    [Test]
    public void TestAllResearchMethodDetails()
    {
        List<ResearchMethodDetails> allDetails = ResearchMethods.None.AllDetails();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Has.Count.EqualTo(6));
            Assert.That(allDetails[1].TextId, Is.EqualTo("ref.enum.researchmethods.countposinhouses"));
            Assert.That(allDetails[3].ResearchMethod, Is.EqualTo(ResearchMethods.CountUnaspected));
        });
    }






}
