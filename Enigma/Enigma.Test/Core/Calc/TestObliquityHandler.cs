// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Requests;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;


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
        IObliquityCalc calcFake = CreateCalcFake();
        IObliquityHandler handler = new ObliquityHandler(calcFake);
        double resultMeanObliquity = handler.CalcObliquity(new ObliquityRequest(JD_UT, false));
        Assert.That(resultMeanObliquity, Is.EqualTo(EXPECTED_MEAN_OBLIQUITY).Within(DELTA));
    }


    [Test]
    public void TextSeException()
    {
        IObliquityCalc calcExceptionFake = CreateCalcFakeThrowingException();
        IObliquityHandler handler = new ObliquityHandler(calcExceptionFake);
        _ = Assert.Throws<EnigmaException>(() => handler.CalcObliquity(new ObliquityRequest(JD_UT, false)));
    }

    private static IObliquityCalc CreateCalcFake()
    {
        var calcFake = A.Fake<IObliquityCalc>();
        
        A.CallTo(() => calcFake.CalculateObliquity(JD_UT, false)).Returns(EXPECTED_MEAN_OBLIQUITY);
        return calcFake;
    }

    private static IObliquityCalc CreateCalcFakeThrowingException()
    {
        var calcFake = A.Fake<IObliquityCalc>();
        var exception = new SwissEphException(ERROR_TEXT);
        A.CallTo(() => calcFake.CalculateObliquity(JD_UT, false)).Throws(exception);
        return calcFake;
    }

}