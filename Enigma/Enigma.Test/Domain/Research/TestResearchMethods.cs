// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Research.Domain;
using Enigma.Domain.Research;

namespace Enigma.Test.Domain.Research;


[TestFixture]
public  class TestResearchMethods
{
    [Test]
    public void TestTextInRbId()
    {
        ResearchMethods method = ResearchMethods.CountOccupiedMidpoints;
        string expectedId = "ref.enum.researchmethods.countoccupiedmidpoints";
        string rbId = method.TextInRbId();
        Assert.That(rbId, Is.EqualTo(expectedId));
    }
}
