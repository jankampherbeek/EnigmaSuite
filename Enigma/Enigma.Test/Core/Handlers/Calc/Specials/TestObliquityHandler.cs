﻿// Enigma Astrology Research.
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
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly double _expectedMeanObliquity = 23.447;
    private readonly double _expectedTrueObliquity = 23.448;
    private readonly string _errorText = "Description of problem.";

    [Test]
    public void TestMeanObliquity()
    {
        Mock<IObliquityCalc> calcMock = CreateCalcMock();
        IObliquityHandler handler = new ObliquityHandler(calcMock.Object);
        double resultMeanObliquity = handler.CalcObliquity(new ObliquityRequest(_jdUt, false));
        Assert.That(resultMeanObliquity, Is.EqualTo(_expectedMeanObliquity).Within(_delta));
    }

    [Test]
    public void TestHappyFlow()
    {
        Mock<IObliquityCalc> calcMock = CreateCalcMock();
        IObliquityHandler handler = new ObliquityHandler(calcMock.Object);
        double resultTrueObliquity = handler.CalcObliquity(new ObliquityRequest(_jdUt, true));
        Assert.That(resultTrueObliquity, Is.EqualTo(_expectedTrueObliquity).Within(_delta));
    }

    [Test]
    public void TextSeException()
    {
        Mock<IObliquityCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IObliquityHandler handler = new ObliquityHandler(calcExceptionMock.Object);
        var _ = Assert.Throws<EnigmaException>(() => handler.CalcObliquity(new ObliquityRequest(_jdUt, true)));
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