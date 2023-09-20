// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Test.Dtos;

[TestFixture]
public class TestCelPointPosSpeeds
{
    [Test]
    public void TestConstructionWithArray()
    {
        const double delta = 0.00000001;
        double[] values = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
        PointPosSpeeds posSpeeds = new(values);
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
        Assert.That(() => _ = new PointPosSpeeds(values), Throws.TypeOf<ArgumentException>());

    }

    [Test]
    public void TestConstructionWithPosSpeeds()
    {
        const double delta = 0.00000001;
        PosSpeed mainPosSpeed = new(1.0, 2.0);
        PosSpeed deviationPosSpeed = new(3.0, 4.0);
        PosSpeed distancePosSpeed = new(5.0, 6.0);
        PointPosSpeeds posSpeeds = new(mainPosSpeed, deviationPosSpeed, distancePosSpeed);
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