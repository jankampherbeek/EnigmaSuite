// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.SolSysPoints;
using E4C.Shared.References;
using NUnit.Framework;

namespace E4CTest.calc.elements;



[TestFixture]
public class TestHypothetsRamCalc
{
    private SolSysPointsElementsCalc _calculator;
    private readonly double _delta = 0.0000001;

    [Test]
    public void TestCalcHypRamEclPosPersephone()
    {
        _calculator = new SolSysPointsElementsCalc(new CalcHelioPos());
        double jdUt = 2434406.817711;
        double[] result = _calculator.Calculate(SolarSystemPoints.PersephoneRam, jdUt);
        Assert.AreEqual(326.6011343685, result[0], _delta);
    }

    [Test]
    public void TestCalcHypRamEclPosHermes()
    {
        _calculator = new SolSysPointsElementsCalc(new CalcHelioPos());
        double jdUt = 2434406.817711;
        double[] result = _calculator.Calculate(SolarSystemPoints.HermesRam, jdUt);
        Assert.AreEqual(161.6211128197, result[0], _delta);
    }

    [Test]
    public void TestCalcHypRamEclPosDemeter()
    {
        _calculator = new SolSysPointsElementsCalc(new CalcHelioPos());
        double jdUt = 2434406.817711;
        double[] result = _calculator.Calculate(SolarSystemPoints.DemeterRam, jdUt);
        Assert.AreEqual(261.4081200589, result[0], _delta);
    }

}