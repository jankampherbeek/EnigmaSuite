// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.Obliquity;
using E4C.Core.Facades;
using Moq;
using NUnit.Framework;


namespace E4CTest.core.astron.obliquity;

[TestFixture]
public class TestObliquityCalc
{
    readonly double delta = 0.00000001;

    [Test]
    public void TestCalculateTrueObliquity()
    {
        bool trueObliquity = true;
        double expectedObliquity = 23.447;
        double jd = 12345.678;
        ObliquityCalc calc = CreateObliquityCalc();
        double result = calc.CalculateObliquity(jd, trueObliquity);
        Assert.AreEqual(expectedObliquity, result, delta);
    }

    [Test]
    public void TestCalculateMeanObliquity()
    {
        bool trueObliquity = false;
        double expectedObliquity = 23.448;
        double jd = 12345.678;
        ObliquityCalc calc = CreateObliquityCalc();
        double result = calc.CalculateObliquity(jd, trueObliquity);
        Assert.AreEqual(expectedObliquity, result, delta);
    }

    private static ObliquityCalc CreateObliquityCalc()
    {
        int celpointId = -1;
        int flags = 0;
        double jd = 12345.678;
        double[] positions = { 23.448, 23.447, 0.0, 0.0, 0.0, 0.0 };
        var mock = new Mock<ICalcUtFacade>();
        mock.Setup(p => p.PosCelPointFromSe(jd, celpointId, flags)).Returns(positions);
        ObliquityCalc calc = new(mock.Object);
        return calc;
    }
}

