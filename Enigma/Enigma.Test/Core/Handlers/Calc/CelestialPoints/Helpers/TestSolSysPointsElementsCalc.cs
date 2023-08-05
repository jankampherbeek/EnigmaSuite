// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.CelestialPoints.Helpers;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;

namespace Enigma.Test.Core.Handlers.Calc.CelestialPoints.Helpers;

[TestFixture]
public class TestHypothetsRamCalc
{
    private const double DELTA = 0.0000001;

    [Test]
    public void TestCalcHypRamEclPosPersephone()
    {
        var calculator = new CelPointsElementsCalc(new CalcHelioPos());
        const double jdUt = 2434406.817711;
        double[] result = calculator.Calculate(ChartPoints.PersephoneRam, jdUt, ObserverPositions.GeoCentric);
        Assert.That(result[0], Is.EqualTo(326.6011343685).Within(DELTA));
    }

    [Test]
    public void TestCalcHypRamEclPosHermes()
    {
        var calculator = new CelPointsElementsCalc(new CalcHelioPos());
        const double jdUt = 2434406.817711;
        double[] result = calculator.Calculate(ChartPoints.HermesRam, jdUt, ObserverPositions.GeoCentric);
        Assert.That(result[0], Is.EqualTo(161.6211128197).Within(DELTA));
    }

    [Test]
    public void TestCalcHypRamEclPosDemeter()
    {
        var calculator = new CelPointsElementsCalc(new CalcHelioPos());
        const double jdUt = 2434406.817711;
        double[] result = calculator.Calculate(ChartPoints.DemeterRam, jdUt, ObserverPositions.GeoCentric);
        Assert.That(result[0], Is.EqualTo(261.4081200589).Within(DELTA));
    }

}