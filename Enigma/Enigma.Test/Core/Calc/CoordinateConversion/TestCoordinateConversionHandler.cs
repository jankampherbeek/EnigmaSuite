// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.CoordinateConversion;
using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Positional;
using Moq;

namespace Enigma.Test.Core.Calc.CoordinateConversion;

[TestFixture]
public class TestCoordinateConversionHandler
{
    private readonly double _obliquity = 23.447;
    private readonly double _delta = 0.00000001;
    private readonly string _errorText = "Description of error.";

    [Test]
    public void TestHappyFlow()
    {
        var eclCoord = new EclipticCoordinates(222.2, 1.1);
        var eqCoord = new EquatorialCoordinates(223.3, -3.3);
        var request = new CoordinateConversionRequest(eclCoord, _obliquity);
        Mock<ICoordinateConversionCalc> calcMock = CreateCalcMock(eclCoord, eqCoord);
        ICoordinateConversionHandler handler = new CoordinateConversionHandler(calcMock.Object);
        CoordinateConversionResponse response = handler.HandleConversion(request);
        Assert.Multiple(() =>
        {
            Assert.That(response.EquatorialCoord.RightAscension, Is.EqualTo(eqCoord.RightAscension).Within(_delta));
            Assert.That(response.EquatorialCoord.Declination, Is.EqualTo(eqCoord.Declination).Within(_delta));
            Assert.That(response.Success, Is.True);
            Assert.That(response.ErrorText, Is.EqualTo(""));
        });
    }

    [Test]
    public void TestSeException()
    {
        var eclCoord = new EclipticCoordinates(222.2, 1.1);
        var eqCoord = new EquatorialCoordinates(223.3, -3.3);
        var request = new CoordinateConversionRequest(eclCoord, _obliquity);
        Mock<ICoordinateConversionCalc> calcMock = CreateCalcMockThrowingException(eclCoord);
        ICoordinateConversionHandler handler = new CoordinateConversionHandler(calcMock.Object);
        CoordinateConversionResponse response = handler.HandleConversion(request);
        Assert.Multiple(() =>
        {
            Assert.IsFalse(response.Success);
            Assert.That(response.ErrorText, Is.EqualTo(_errorText));
        });
    }

    private Mock<ICoordinateConversionCalc> CreateCalcMock(EclipticCoordinates eclCoord, EquatorialCoordinates eqCoord)
    {
        var mock = new Mock<ICoordinateConversionCalc>();
        mock.Setup(p => p.PerformConversion(eclCoord, _obliquity)).Returns(eqCoord);
        return mock;
    }


    private Mock<ICoordinateConversionCalc> CreateCalcMockThrowingException(EclipticCoordinates eclCoord)
    {
        var mock = new Mock<ICoordinateConversionCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.PerformConversion(eclCoord, _obliquity)).Throws(exception);
        return mock;
    }


}