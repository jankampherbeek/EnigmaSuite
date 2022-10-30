// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Locational;
using Enigma.Domain.Positional;
using Enigma4C.Core.Calc.Horizontal;
using Moq;

namespace Enigma.Test.Core.Calc.Horizontal;

[TestFixture]
public class TestHorizontalHandler
{
    private readonly double _delta = 0.00000001;
    private readonly double _jdUt = 123456.789;
    private readonly Location _location = new("Anywhere", 50.0, 10.0);
    private readonly EclipticCoordinates _eclipticCoordinates = new(160.0, 3.3);
    private readonly int _flags = 0;
    private readonly double[] _expectedResults = { 222.2, 33.3 };
    private readonly string _errorText = "Description of problem.";


    [Test]
    public void TestHappyFlow()
    {
        Mock<IHorizontalCalc> calcMock = CreateCalcMock();
        IHorizontalHandler handler = new HorizontalHandler(calcMock.Object);
        HorizontalResponse response = handler.CalcHorizontal(new HorizontalRequest(_jdUt, _location, _eclipticCoordinates));
        Assert.Multiple(() =>
        {
            Assert.That(response.HorizontalAzimuthAltitude.Azimuth, Is.EqualTo(_expectedResults[0]).Within(_delta));
            Assert.That(response.HorizontalAzimuthAltitude.Altitude, Is.EqualTo(_expectedResults[1]).Within(_delta));
            Assert.That(response.Success, Is.True);
            Assert.That(response.ErrorText, Is.EqualTo(""));
        });
    }

    [Test]
    public void TextSeException()
    {
        Mock<IHorizontalCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IHorizontalHandler handler = new HorizontalHandler(calcExceptionMock.Object);
        HorizontalResponse response = handler.CalcHorizontal(new HorizontalRequest(_jdUt, _location, _eclipticCoordinates));
        Assert.Multiple(() =>
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.ErrorText, Is.EqualTo(_errorText));
        });
    }

    private Mock<IHorizontalCalc> CreateCalcMock()
    {
        var mock = new Mock<IHorizontalCalc>();
        mock.Setup(p => p.CalculateHorizontal(_jdUt, _location, _eclipticCoordinates, _flags)).Returns(_expectedResults);
        return mock;
    }

    private Mock<IHorizontalCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IHorizontalCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.CalculateHorizontal(_jdUt, _location, _eclipticCoordinates, _flags)).Throws(exception);
        return mock;
    }
}
