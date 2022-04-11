// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.positions;
using E4C.domain.shared.references;
using E4C.Models.Domain;
using NUnit.Framework;
using System;

namespace E4CTest.domain.shared.references;


[TestFixture]
public class TestSolSysPointPosSpeeds
{
    [Test]
    public void TestConstructionWithArray()
    {
        double delta = 0.00000001;
        double[] values = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
        SolSysPointPosSpeeds posSpeeds = new(values);
        Assert.AreEqual(1.0, posSpeeds.MainPosSpeed.Position, delta);
        Assert.AreEqual(2.0, posSpeeds.MainPosSpeed.Speed, delta);
        Assert.AreEqual(3.0, posSpeeds.DeviationPosSpeed.Position, delta);
        Assert.AreEqual(4.0, posSpeeds.DeviationPosSpeed.Speed, delta);
        Assert.AreEqual(5.0, posSpeeds.DistancePosSpeed.Position, delta);
        Assert.AreEqual(6.0, posSpeeds.DistancePosSpeed.Speed, delta);
    }

    [Test]
    public void TestConstructionWrongArray()
    {
        double[] values = { 1.0, 2.0, 3.0, 4.0, 5.0 };
        Assert.Throws<ArgumentException>(() => _ = new SolSysPointPosSpeeds(values));
    }

    [Test]
    public void TestConstructionWithPosSpeeds()
    {
        double delta = 0.00000001;
        PosSpeed mainPosSpeed = new(1.0, 2.0);
        PosSpeed deviationPosSpeed = new(3.0, 4.0);
        PosSpeed distancePosSpeed = new(5.0, 6.0);
        SolSysPointPosSpeeds posSpeeds = new(mainPosSpeed, deviationPosSpeed, distancePosSpeed);
        Assert.AreEqual(1.0, posSpeeds.MainPosSpeed.Position, delta);
        Assert.AreEqual(2.0, posSpeeds.MainPosSpeed.Speed, delta);
        Assert.AreEqual(3.0, posSpeeds.DeviationPosSpeed.Position, delta);
        Assert.AreEqual(4.0, posSpeeds.DeviationPosSpeed.Speed, delta);
        Assert.AreEqual(5.0, posSpeeds.DistancePosSpeed.Position, delta);
        Assert.AreEqual(6.0, posSpeeds.DistancePosSpeed.Speed, delta);
    }


}