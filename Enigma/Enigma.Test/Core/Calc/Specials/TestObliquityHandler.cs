// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Specials;
using Enigma.Core.Handlers;
using Enigma.Core.Interfaces;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Requests;
using Moq;

namespace Enigma.Test.Core.Calc.Specials;


[TestFixture]
public class TestObliquityHandler
{
    private const double JD_UT = 123456.789;
    private const double DELTA = 0.00000001;
    private const double EXPECTED_MEAN_OBLIQUITY = 23.447;
    private const string ERROR_TEXT = "Description of problem.";

    [Test]
    public void TestMeanObliquity()
    {
        Mock<IObliquityCalc> calcMock = CreateCalcMock();
        IObliquityHandler handler = new ObliquityHandler(calcMock.Object);
        double resultMeanObliquity = handler.CalcObliquity(new ObliquityRequest(JD_UT, false));
        Assert.That(resultMeanObliquity, Is.EqualTo(EXPECTED_MEAN_OBLIQUITY).Within(DELTA));
    }


    [Test]
    public void TextSeException()
    {
        Mock<IObliquityCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IObliquityHandler handler = new ObliquityHandler(calcExceptionMock.Object);
        _ = Assert.Throws<EnigmaException>(() => handler.CalcObliquity(new ObliquityRequest(JD_UT, false)));
    }

    private static Mock<IObliquityCalc> CreateCalcMock()
    {
        var mock = new Mock<IObliquityCalc>();
        mock.Setup(p => p.CalculateObliquity(JD_UT, false)).Returns(EXPECTED_MEAN_OBLIQUITY);
        return mock;
    }

    private static Mock<IObliquityCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IObliquityCalc>();
        var exception = new SwissEphException(ERROR_TEXT);
        mock.Setup(p => p.CalculateObliquity(JD_UT, false)).Throws(exception);
        return mock;
    }

}