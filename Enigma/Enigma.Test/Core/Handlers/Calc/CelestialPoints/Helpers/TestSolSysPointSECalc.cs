// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc;
using Enigma.Core.Handlers.Calc.CelestialPoints.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;
using Enigma.Facades.Interfaces;
using Moq;

namespace Enigma.Test.Core.Handlers.Calc.CelestialPoints.Helpers;

[TestFixture]
public class TestCelPointCalc
{
    private const double JulianDayUt = 2123456.5;
    private const double Longitude = 52.0;
    private const double Latitude = 3.0;
    private const double Distance = 2.0;
    private const double LongSpeed = 0.7;
    private const double LatSpeed = -0.1;
    private const double DistSpeed = 0.02;
    private const double Delta = 0.00000001;
    private const int FlagsEcliptical = 0;

    [Test]
    public void TestCalculateCelPointLongitude()
    {
        PosSpeed[] result = CalculatePosSpeedForCelPoint();
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Position, Is.EqualTo(Longitude).Within(Delta));
            Assert.That(result[0].Speed, Is.EqualTo(LongSpeed).Within(Delta));
        });
    }

    [Test]
    public void TestCalculateCelPointLatitude()
    {
        PosSpeed[] result = CalculatePosSpeedForCelPoint();
        Assert.Multiple(() =>
        {
            Assert.That(result[1].Position, Is.EqualTo(Latitude).Within(Delta));
            Assert.That(result[1].Speed, Is.EqualTo(LatSpeed).Within(Delta));
        });
    }

    [Test]
    public void TestCalculateCelPointDistance()
    {
        PosSpeed[] result = CalculatePosSpeedForCelPoint();
        Assert.Multiple(() =>
        {
            Assert.That(result[2].Position, Is.EqualTo(Distance).Within(Delta));
            Assert.That(result[2].Speed, Is.EqualTo(DistSpeed).Within(Delta));
        });
    }

    private static PosSpeed[] CalculatePosSpeedForCelPoint()
    {
        var location = new Location("", 52.0, 6.0);
        var mockCalcUtFacade = new Mock<ICalcUtFacade>();
        mockCalcUtFacade.Setup(p => p.PositionFromSe(JulianDayUt, EnigmaConstants.SE_MARS, FlagsEcliptical)).Returns(new[] { Longitude, Latitude, Distance, LongSpeed, LatSpeed, DistSpeed });
        ICelPointSeCalc calc = new CelPointSeCalc(mockCalcUtFacade.Object, new ChartPointsMapping());
        return calc.CalculateCelPoint(ChartPoints.Mars, JulianDayUt, location, FlagsEcliptical);
    }

}