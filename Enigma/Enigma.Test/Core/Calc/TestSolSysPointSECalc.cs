// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Interfaces;
using Moq;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestCelPointCalc
{
    private const double JULIAN_DAY_UT = 2123456.5;
    private const double LONGITUDE = 52.0;
    private const double LATITUDE = 3.0;
    private const double DISTANCE = 2.0;
    private const double LONG_SPEED = 0.7;
    private const double LAT_SPEED = -0.1;
    private const double DIST_SPEED = 0.02;
    private const double DELTA = 0.00000001;
    private const int FLAGS_ECLIPTICAL = 0;

    [Test]
    public void TestCalculateCelPointLongitude()
    {
        PosSpeed[] result = CalculatePosSpeedForCelPoint();
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Position, Is.EqualTo(LONGITUDE).Within(DELTA));
            Assert.That(result[0].Speed, Is.EqualTo(LONG_SPEED).Within(DELTA));
        });
    }

    [Test]
    public void TestCalculateCelPointLatitude()
    {
        PosSpeed[] result = CalculatePosSpeedForCelPoint();
        Assert.Multiple(() =>
        {
            Assert.That(result[1].Position, Is.EqualTo(LATITUDE).Within(DELTA));
            Assert.That(result[1].Speed, Is.EqualTo(LAT_SPEED).Within(DELTA));
        });
    }

    [Test]
    public void TestCalculateCelPointDistance()
    {
        PosSpeed[] result = CalculatePosSpeedForCelPoint();
        Assert.Multiple(() =>
        {
            Assert.That(result[2].Position, Is.EqualTo(DISTANCE).Within(DELTA));
            Assert.That(result[2].Speed, Is.EqualTo(DIST_SPEED).Within(DELTA));
        });
    }

    private static PosSpeed[] CalculatePosSpeedForCelPoint()
    {
        var location = new Location("", 52.0, 6.0);
        var mockCalcUtFacade = new Mock<ICalcUtFacade>();
        mockCalcUtFacade.Setup(p => p.PositionFromSe(JULIAN_DAY_UT, EnigmaConstants.SE_MARS, FLAGS_ECLIPTICAL)).Returns(new[] { LONGITUDE, LATITUDE, DISTANCE, LONG_SPEED, LAT_SPEED, DIST_SPEED });
        ICelPointSeCalc calc = new CelPointSeCalc(mockCalcUtFacade.Object, new ChartPointsMapping());
        return calc.CalculateCelPoint(ChartPoints.Mars, JULIAN_DAY_UT, location, FLAGS_ECLIPTICAL);
    }

}