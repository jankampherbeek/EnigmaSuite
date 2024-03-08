// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using FakeItEasy;

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
        var calcFake = CreateCalcFake(eclCoord, eqCoord);
        ICoordinateConversionHandler handler = new CoordinateConversionHandler(calcFake);
        EquatorialCoordinates coordinates = handler.HandleConversion(request);
        Assert.Multiple(() =>
        {
            Assert.That(coordinates.RightAscension, Is.EqualTo(eqCoord.RightAscension).Within(DELTA));
            Assert.That(coordinates.Declination, Is.EqualTo(eqCoord.Declination).Within(DELTA));
        });
    }


    private static ICoordinateConversionCalc CreateCalcFake(EclipticCoordinates eclCoord, EquatorialCoordinates eqCoord)
    {
        var calcFake = A.Fake<ICoordinateConversionCalc>();
        A.CallTo(() => calcFake.PerformConversion(eclCoord, OBLIQUITY)).Returns(eqCoord);
        return calcFake;
    }

}