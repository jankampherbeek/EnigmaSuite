// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Facades.Interfaces;
using Moq;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestCoordinateConversionCalc
{
    private const double LONGITUDE = 100.0;
    private const double LATITUDE = -1.0;
    private const double EXPECTED_RIGHT_ASC = 99.0;
    private const double EXPECTED_DECLINATION = 2.2;
    private const double OBLIQUITY = 23.447;
    private const double DELTA = 0.00000001;

    [Test]
    public void TestConversion()
    {
        var ecliptical = new[] { LONGITUDE, LATITUDE };
        var eclCoord = new EclipticCoordinates(LONGITUDE, LATITUDE);
        var equatorial = new[] { EXPECTED_RIGHT_ASC, EXPECTED_DECLINATION };
        Mock<ICoTransFacade> mockFacade = CreateFacadeMock(ecliptical, equatorial);
        ICoordinateConversionCalc convCalc = new CoordinateConversionCalc(mockFacade.Object);
        EquatorialCoordinates equatCoord = convCalc.PerformConversion(eclCoord, OBLIQUITY);
        Assert.Multiple(() =>
        {
            Assert.That(equatCoord.RightAscension, Is.EqualTo(EXPECTED_RIGHT_ASC).Within(DELTA));
            Assert.That(equatCoord.Declination, Is.EqualTo(EXPECTED_DECLINATION).Within(DELTA));
        });
    }

    private static Mock<ICoTransFacade> CreateFacadeMock(double[] ecliptical, double[] equatorial)
    {
        var mock = new Mock<ICoTransFacade>();
        mock.Setup(p => p.EclipticToEquatorial(ecliptical, OBLIQUITY)).Returns(equatorial);
        return mock;
    }

}
