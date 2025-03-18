// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Test.Domain.Dtos;

[TestFixture]
public class TestOrbitalElements
{
    private const double DELTA = 1E-8;

    [Test]
    public void Testconstructor()
    {
        var elements = CreateArrayWithElements();
        var elementsRecord = new OrbitalElements(elements);
        Assert.Multiple(() =>
        {
            Assert.That(elementsRecord.SemiMajorAxis, Is.EqualTo(0.0).Within(DELTA));
            Assert.That(elementsRecord.Eccentricity, Is.EqualTo(1.0).Within(DELTA));
            Assert.That(elementsRecord.Inclination, Is.EqualTo(2.0).Within(DELTA));
            Assert.That(elementsRecord.LongAscNode, Is.EqualTo(3.0).Within(DELTA));
            Assert.That(elementsRecord.ArgPeriApsis, Is.EqualTo(4.0).Within(DELTA));
            Assert.That(elementsRecord.LongPeriApsis, Is.EqualTo(5.0).Within(DELTA));
            Assert.That(elementsRecord.MeanAnomalyEpoch, Is.EqualTo(6.0).Within(DELTA));
            Assert.That(elementsRecord.TrueAnomalyEpoch, Is.EqualTo(7.0).Within(DELTA));
            Assert.That(elementsRecord.EccAnomalyEpoch, Is.EqualTo(8.0).Within(DELTA));
            Assert.That(elementsRecord.MeanLongEpoch, Is.EqualTo(9.0).Within(DELTA));
            Assert.That(elementsRecord.SiderealOrbPeriodYears, Is.EqualTo(10.0).Within(DELTA));
            Assert.That(elementsRecord.MeanDailyMotion, Is.EqualTo(11.0).Within(DELTA));
            Assert.That(elementsRecord.TropicalPeriodYears, Is.EqualTo(12.0).Within(DELTA));
            Assert.That(elementsRecord.SynodicPeriodDays, Is.EqualTo(13.0).Within(DELTA));
            Assert.That(elementsRecord.TimePeriHelionPassage, Is.EqualTo(14.0).Within(DELTA));
            Assert.That(elementsRecord.PeriHelionDistance, Is.EqualTo(15.0).Within(DELTA));
            Assert.That(elementsRecord.ApHelionDistance, Is.EqualTo(16.0).Within(DELTA));
        });
    }

    private double[] CreateArrayWithElements()
    {
        var elements = new double[]
        {
            0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0
        };
        return elements;
    }


}