// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.SolSysPoints;
using Enigma.Domain.Enums;

namespace EnigmaTest.Core.Calc.SolSysPoints;

[TestFixture]
public class TestHypothetsRamCalc
{

    private readonly double _delta = 0.0000001;

    [Test]
    public void TestCalcHypRamEclPosPersephone()
    {
        var calculator = new SolSysPointsElementsCalc(new CalcHelioPos());
        double jdUt = 2434406.817711;
        double[] result = calculator.Calculate(SolarSystemPoints.PersephoneRam, jdUt);
        Assert.That(result[0], Is.EqualTo(326.6011343685).Within(_delta));
    }

    [Test]
    public void TestCalcHypRamEclPosHermes()
    {
        var calculator = new SolSysPointsElementsCalc(new CalcHelioPos());
        double jdUt = 2434406.817711;
        double[] result = calculator.Calculate(SolarSystemPoints.HermesRam, jdUt);
        Assert.That(result[0], Is.EqualTo(161.6211128197).Within(_delta));
    }

    [Test]
    public void TestCalcHypRamEclPosDemeter()
    {
        var calculator = new SolSysPointsElementsCalc(new CalcHelioPos());
        double jdUt = 2434406.817711;
        double[] result = calculator.Calculate(SolarSystemPoints.DemeterRam, jdUt);
        Assert.That(result[0], Is.EqualTo(261.4081200589).Within(_delta));
    }

}