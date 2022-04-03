// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.addpoints;
using E4C.Models.Domain;
using NUnit.Framework;

namespace E4CTest.calc.addpoints;



[TestFixture]
public class TestHypothetsRamCalc
{
    private CalcHypRamEclPos _calculator;
    private double _delta = 0.0000001;

    [Test]
    public void TestCalcHypRamEclPos()
    {
        _calculator = new CalcHypRamEclPos();
        double jdUt = 2434406.817711;
        double[] result = _calculator.Calculate(SolarSystemPoints.PersephoneRam, jdUt);
        Assert.AreEqual(326.6011343685, result[0], _delta);         
        result = _calculator.Calculate(SolarSystemPoints.HermesRam, jdUt);
        Assert.AreEqual(161.6211128197, result[0], _delta);       
        result = _calculator.Calculate(SolarSystemPoints.DemeterRam, jdUt);
        Assert.AreEqual(261.4081200589, result[0], _delta);          



        // Persephone 326.6 (Vulcanus) 26 g 36 m AQ (PD)  
        // Hermes  161.62 (Vulcanus) 11 g 37 m VI (PD)
        // Demeter 261.41   wp 259.85 (Vulcanus)  19 g 51 m SA (PD)
    }

}