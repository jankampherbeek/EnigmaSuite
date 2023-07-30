// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Specials;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Exceptions;
using Moq;

namespace Enigma.Test.Core.Handlers.Calc.Specials;


[TestFixture]
public class TestObliquityHandler
{
    private const double JdUt = 123456.789;
    private const double Delta = 0.00000001;
    private const double ExpectedMeanObliquity = 23.447;
    private const string ErrorText = "Description of problem.";

    [Test]
    public void TestMeanObliquity()
    {
        Mock<IObliquityCalc> calcMock = CreateCalcMock();
        IObliquityHandler handler = new ObliquityHandler(calcMock.Object);
        double resultMeanObliquity = handler.CalcObliquity(new ObliquityRequest(JdUt, false));
        Assert.That(resultMeanObliquity, Is.EqualTo(ExpectedMeanObliquity).Within(Delta));
    }


    [Test]
    public void TextSeException()
    {
        Mock<IObliquityCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IObliquityHandler handler = new ObliquityHandler(calcExceptionMock.Object);
        var _ = Assert.Throws<EnigmaException>(() => handler.CalcObliquity(new ObliquityRequest(JdUt, false)));
    }

    private static Mock<IObliquityCalc> CreateCalcMock()
    {
        var mock = new Mock<IObliquityCalc>();
        mock.Setup(p => p.CalculateObliquity(JdUt, false)).Returns(ExpectedMeanObliquity);
        return mock;
    }

    private static Mock<IObliquityCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IObliquityCalc>();
        var exception = new SwissEphException(ErrorText);
        mock.Setup(p => p.CalculateObliquity(JdUt, false)).Throws(exception);
        return mock;
    }

}