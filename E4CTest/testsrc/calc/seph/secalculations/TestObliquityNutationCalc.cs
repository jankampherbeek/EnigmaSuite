// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.seph.secalculations;
using E4C.calc.seph.sefacade;
using Moq;
using NUnit.Framework;


namespace E4CTest.calc.seph.secalculations;

[TestFixture]
public class TestObliquityNutationCalc
{
    readonly double delta = 0.00000001;

    [Test]
    public void TestCalculateTrueObliquity()
    {
        bool trueObliquity = true;
        double expectedObliquity = 23.447;
        double jd = 12345.678;
        ObliquityNutationCalc calc = CreateObliquityNutationCalc();
        double result = calc.CalculateObliquity(jd, trueObliquity);
        Assert.AreEqual(expectedObliquity, result, delta);
    }

    [Test]
    public void TestCalculateMeanObliquity()
    {
        bool trueObliquity = false;
        double expectedObliquity = 23.448;
        double jd = 12345.678;
        ObliquityNutationCalc calc = CreateObliquityNutationCalc();
        double result = calc.CalculateObliquity(jd, trueObliquity);
        Assert.AreEqual(expectedObliquity, result, delta);
    }

    private static ObliquityNutationCalc CreateObliquityNutationCalc()
    {
        int celpointId = -1;
        int flags = 0;
        double jd = 12345.678;
        double[] positions = { 23.448, 23.447, 0.0, 0.0, 0.0, 0.0 };
        var mock = new Mock<ISePosCelPointFacade>();
        mock.Setup(p => p.PosCelPointFromSe(jd, celpointId, flags)).Returns(positions);
        ObliquityNutationCalc calc = new(mock.Object);
        return calc;
    }
}

