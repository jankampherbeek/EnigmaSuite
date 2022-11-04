// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;

namespace Enigma.Test.Domain.AstronCalculations;

[TestFixture]
public class TestSolSysPointPosSpeeds
{
    [Test]
    public void TestConstructionWithArray()
    {
        double delta = 0.00000001;
        double[] values = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
        SolSysPointPosSpeeds posSpeeds = new(values);
        Assert.Multiple(() =>
        {
            Assert.That(posSpeeds.MainPosSpeed.Position, Is.EqualTo(1.0).Within(delta));
            Assert.That(posSpeeds.MainPosSpeed.Speed, Is.EqualTo(2.0).Within(delta));
            Assert.That(posSpeeds.DeviationPosSpeed.Position, Is.EqualTo(3.0).Within(delta));
            Assert.That(posSpeeds.DeviationPosSpeed.Speed, Is.EqualTo(4.0).Within(delta));
            Assert.That(posSpeeds.DistancePosSpeed.Position, Is.EqualTo(5.0).Within(delta));
            Assert.That(posSpeeds.DistancePosSpeed.Speed, Is.EqualTo(6.0).Within(delta));
        });
    }

    [Test]
    public void TestConstructionWrongArray()
    {
        double[] values = { 1.0, 2.0, 3.0, 4.0, 5.0 };
        Assert.That(() => _ = new SolSysPointPosSpeeds(values), Throws.TypeOf<ArgumentException>());

    }

    [Test]
    public void TestConstructionWithPosSpeeds()
    {
        double delta = 0.00000001;
        PosSpeed mainPosSpeed = new(1.0, 2.0);
        PosSpeed deviationPosSpeed = new(3.0, 4.0);
        PosSpeed distancePosSpeed = new(5.0, 6.0);
        SolSysPointPosSpeeds posSpeeds = new(mainPosSpeed, deviationPosSpeed, distancePosSpeed);
        Assert.Multiple(() =>
        {
            Assert.That(posSpeeds.MainPosSpeed.Position, Is.EqualTo(1.0).Within(delta));
            Assert.That(posSpeeds.MainPosSpeed.Speed, Is.EqualTo(2.0).Within(delta));
            Assert.That(posSpeeds.DeviationPosSpeed.Position, Is.EqualTo(3.0).Within(delta));
            Assert.That(posSpeeds.DeviationPosSpeed.Speed, Is.EqualTo(4.0).Within(delta));
            Assert.That(posSpeeds.DistancePosSpeed.Position, Is.EqualTo(5.0).Within(delta));
            Assert.That(posSpeeds.DistancePosSpeed.Speed, Is.EqualTo(6.0).Within(delta));
        });
    }
}