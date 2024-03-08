// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Facades.Se;
using FakeItEasy;

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
        ICoTransFacade facadeFake = CreateFacadeFake(ecliptical, equatorial);
        ICoordinateConversionCalc convCalc = new CoordinateConversionCalc(facadeFake);
        EquatorialCoordinates equatCoord = convCalc.PerformConversion(eclCoord, OBLIQUITY);
        Assert.Multiple(() =>
        {
            Assert.That(equatCoord.RightAscension, Is.EqualTo(EXPECTED_RIGHT_ASC).Within(DELTA));
            Assert.That(equatCoord.Declination, Is.EqualTo(EXPECTED_DECLINATION).Within(DELTA));
        });
    }

    private static ICoTransFacade CreateFacadeFake(double[] ecliptical, double[] equatorial)
    {
        var fake = A.Fake<ICoTransFacade>();
        A.CallTo(() => fake.EclipticToEquatorial(ecliptical, OBLIQUITY)).Returns(equatorial);
        return fake;
    }

}
