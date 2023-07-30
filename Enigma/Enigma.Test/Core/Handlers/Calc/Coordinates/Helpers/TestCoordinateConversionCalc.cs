// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Coordinates.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Facades.Interfaces;
using Moq;


namespace Enigma.Test.Core.Handlers.Calc.Coordinates.Helpers;

[TestFixture]
public class TestCoordinateConversionCalc
{
    private const double Longitude = 100.0;
    private const double Latitude = -1.0;
    private const double ExpectedRightAsc = 99.0;
    private const double ExpectedDeclination = 2.2;
    private const double Obliquity = 23.447;
    private const double Delta = 0.00000001;

    [Test]
    public void TestConversion()
    {
        var ecliptical = new[] { Longitude, Latitude };
        var eclCoord = new EclipticCoordinates(Longitude, Latitude);
        var equatorial = new[] { ExpectedRightAsc, ExpectedDeclination };
        Mock<ICoTransFacade> mockFacade = CreateFacadeMock(ecliptical, equatorial);
        ICoordinateConversionCalc convCalc = new CoordinateConversionCalc(mockFacade.Object);
        EquatorialCoordinates equatCoord = convCalc.PerformConversion(eclCoord, Obliquity);
        Assert.Multiple(() =>
        {
            Assert.That(equatCoord.RightAscension, Is.EqualTo(ExpectedRightAsc).Within(Delta));
            Assert.That(equatCoord.Declination, Is.EqualTo(ExpectedDeclination).Within(Delta));
        });
    }

    private static Mock<ICoTransFacade> CreateFacadeMock(double[] ecliptical, double[] equatorial)
    {
        var mock = new Mock<ICoTransFacade>();
        mock.Setup(p => p.EclipticToEquatorial(ecliptical, Obliquity)).Returns(equatorial);
        return mock;
    }

}
