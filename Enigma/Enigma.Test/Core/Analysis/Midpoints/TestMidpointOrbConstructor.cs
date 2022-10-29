// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Midpoints;
using Enigma.Domain.Analysis;

namespace Enigma.Test.Core.Analysis.Midpoints;

[TestFixture]
public class TestMidpointOrbConstructor
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestOrb4SolSysPoint()
    {
        IMidpointOrbConstructor _orbConstructor = new MidpointOrbConstructor();
        MidpointDetails midpointDetails = new(MidpointTypes.Dial45, 8, "", 0.125);
        double actualOrb = _orbConstructor.DefineOrb(midpointDetails);
        double expectedOrb = 1.6 * 0.125;
        Assert.That(actualOrb, Is.EqualTo(expectedOrb).Within(_delta));
    }

}
