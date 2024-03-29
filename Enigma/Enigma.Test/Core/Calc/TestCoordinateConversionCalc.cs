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

    [Test]
    public void TestDeclinationToLongitudeHappyFLow()
    {
        IDirectConversionCalc convCalc = new DirectConversionCalc();
        const double obliquity = 23.447;
        const double declination = 5.0;
        const double expectedLe = 12.65259427;
        double longitudeEquivalent = convCalc.DeclinationToLongitude(obliquity, declination);
        Assert.That(longitudeEquivalent, Is.EqualTo(expectedLe).Within(DELTA));
    }
    
    [Test]
    public void TestDeclinationToLongitudeZeroDecl()
    {
        IDirectConversionCalc convCalc = new DirectConversionCalc();
        const double obliquity = 23.447;
        const double declination = 0.0;
        const double expectedLe = 0.0;
        double longitudeEquivalent = convCalc.DeclinationToLongitude(obliquity, declination);
        Assert.That(longitudeEquivalent, Is.EqualTo(expectedLe).Within(DELTA));
    }
    
    [Test]
    public void TestDeclinationToLongitudeNegativeDecl()
    {
        IDirectConversionCalc convCalc = new DirectConversionCalc();
        const double obliquity = 23.447;
        const double declination = -5.0;
        const double expectedLe = -12.65259427;
        double longitudeEquivalent = convCalc.DeclinationToLongitude(obliquity, declination);
        Assert.That(longitudeEquivalent, Is.EqualTo(expectedLe).Within(DELTA));
    }
    
    [Test]
    public void TestDeclinationToLongitudeMaxDecl()
    {
        IDirectConversionCalc convCalc = new DirectConversionCalc();
        const double obliquity = 23.447;
        const double declination = 23.447;
        const double expectedLe = 90.0;
        double longitudeEquivalent = convCalc.DeclinationToLongitude(obliquity, declination);
        Assert.That(longitudeEquivalent, Is.EqualTo(expectedLe).Within(DELTA));
    }

    [Test]
    public void TestDeclinationToLongitudeForPointWithZeroLatitude()
    {
        // TODO limit the range of delta
        double delta = 0.01;
        
        IDirectConversionCalc convCalc = new DirectConversionCalc();
        const double meanObliquity = 23.447072302623031;
        const double trueObliquity = 23.44538400133418;
        const double declination = -17.983487996715873;
        const double expectedLe = 309.1192883474571 - 360.0;
        double longitudeEquivalent = convCalc.DeclinationToLongitude(meanObliquity, declination);
        Assert.That(longitudeEquivalent, Is.EqualTo(expectedLe).Within(delta));
        
        // true obliquity: Off by:   0.013272381444934922d
        // mean obliquity: Off by:   0.0084837114662050794d
    }
    
    
    private static ICoTransFacade CreateFacadeFake(double[] ecliptical, double[] equatorial)
    {
        var fake = A.Fake<ICoTransFacade>();
        A.CallTo(() => fake.EclipticToEquatorial(ecliptical, OBLIQUITY)).Returns(equatorial);
        return fake;
    }

}
