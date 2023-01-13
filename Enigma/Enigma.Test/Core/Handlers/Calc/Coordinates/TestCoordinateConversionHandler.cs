// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Coordinates;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Core.Handlers.Calc.Coordinates;

[TestFixture]
public class TestCoordinateConversionHandler
{
    private readonly double _obliquity = 23.447;
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestHappyFlow()
    {
        var eclCoord = new EclipticCoordinates(222.2, 1.1);
        var eqCoord = new EquatorialCoordinates(223.3, -3.3);
        var request = new CoordinateConversionRequest(eclCoord, _obliquity);
        Mock<ICoordinateConversionCalc> calcMock = CreateCalcMock(eclCoord, eqCoord);
        ICoordinateConversionHandler handler = new CoordinateConversionHandler(calcMock.Object);
        EquatorialCoordinates coordinates = handler.HandleConversion(request);
        Assert.Multiple(() =>
        {
            Assert.That(coordinates.RightAscension, Is.EqualTo(eqCoord.RightAscension).Within(_delta));
            Assert.That(coordinates.Declination, Is.EqualTo(eqCoord.Declination).Within(_delta));
        });
    }


    private Mock<ICoordinateConversionCalc> CreateCalcMock(EclipticCoordinates eclCoord, EquatorialCoordinates eqCoord)
    {
        var mock = new Mock<ICoordinateConversionCalc>();
        mock.Setup(p => p.PerformConversion(eclCoord, _obliquity)).Returns(eqCoord);
        return mock;
    }

}