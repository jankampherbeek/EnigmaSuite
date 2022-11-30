// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;
using Enigma.Domain.Enums;

namespace Enigma.Test.Domain.AstronCalculations;

[TestFixture]
public class TestProjectionTYpes
{
    [Test]
    public void TestRetrievingDetails()
    {
        ProjectionTypes projType = ProjectionTypes.obliqueLongitude;
        string expectedId = "ref.enum.projectiontype.obliquelongitude";
        string rbId = projType.TextInRbId();
        Assert.That(rbId, Is.EqualTo(expectedId));
    }
}
