// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Moq;

namespace Enigma.Test.Core.Handlers;

[TestFixture]
public class TestCoordinateConversionHandler
{
    private const double OBLIQUITY = 23.447;
    private const double DELTA = 0.00000001;

    [Test]
    public void TestHappyFlow()
    {
        var eclCoord = new EclipticCoordinates(222.2, 1.1);
        var eqCoord = new EquatorialCoordinates(223.3, -3.3);
        var request = new CoordinateConversionRequest(eclCoord, OBLIQUITY);
        Mock<ICoordinateConversionCalc> calcMock = CreateCalcMock(eclCoord, eqCoord);
        ICoordinateConversionHandler handler = new CoordinateConversionHandler(calcMock.Object);
        EquatorialCoordinates coordinates = handler.HandleConversion(request);
        Assert.Multiple(() =>
        {
            Assert.That(coordinates.RightAscension, Is.EqualTo(eqCoord.RightAscension).Within(DELTA));
            Assert.That(coordinates.Declination, Is.EqualTo(eqCoord.Declination).Within(DELTA));
        });
    }


    private static Mock<ICoordinateConversionCalc> CreateCalcMock(EclipticCoordinates eclCoord, EquatorialCoordinates eqCoord)
    {
        var mock = new Mock<ICoordinateConversionCalc>();
        mock.Setup(p => p.PerformConversion(eclCoord, OBLIQUITY)).Returns(eqCoord);
        return mock;
    }

}