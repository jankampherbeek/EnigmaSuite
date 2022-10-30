// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.Obliquity;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Exceptions;
using Moq;

namespace Enigma.Test.Core.Calc.Obliquity;


[TestFixture]
public class TestObliquityHandler
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly double _expectedMeanObliquity = 23.447;
    private readonly double _expectedTrueObliquity = 23.448;
    private readonly string _errorText = "Description of problem.";

    [Test]
    public void TestHappyFlow()
    {
        Mock<IObliquityCalc> calcMock = CreateCalcMock();
        IObliquityHandler handler = new ObliquityHandler(calcMock.Object);
        ObliquityResponse response = handler.CalcObliquity(new ObliquityRequest(_jdUt));
        Assert.Multiple(() =>
        {
            Assert.That(response.ObliquityMean, Is.EqualTo(_expectedMeanObliquity).Within(_delta));
            Assert.That(response.ObliquityTrue, Is.EqualTo(_expectedTrueObliquity).Within(_delta));
            Assert.That(response.Success, Is.True);
            Assert.That(response.ErrorText, Is.EqualTo(""));
        });
    }

    [Test]
    public void TextSeException()
    {
        Mock<IObliquityCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IObliquityHandler handler = new ObliquityHandler(calcExceptionMock.Object);
        ObliquityResponse response = handler.CalcObliquity(new ObliquityRequest(_jdUt));
        Assert.Multiple(() =>
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.ErrorText, Is.EqualTo(_errorText));
        });
    }

    private Mock<IObliquityCalc> CreateCalcMock()
    {
        var mock = new Mock<IObliquityCalc>();
        mock.Setup(p => p.CalculateObliquity(_jdUt, true)).Returns(_expectedTrueObliquity);
        mock.Setup(p => p.CalculateObliquity(_jdUt, false)).Returns(_expectedMeanObliquity);
        return mock;
    }

    private Mock<IObliquityCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IObliquityCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.CalculateObliquity(_jdUt, true)).Throws(exception);
        return mock;
    }

}