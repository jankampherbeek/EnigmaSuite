// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4CTest.core.astron.obliquity;

using E4C.core.astron.obliquity;
using E4C.domain.shared.reqresp;
using E4C.exceptions;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;

[TestFixture]
public class TestObliquityHandler
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly bool _useTrueObliquity = true;
    private readonly double _expectedObliquity = 23.447;
    private readonly string _errorText = "Description of problem.";

    [Test]
    public void TestHappyFlow()
    {
        Mock<IObliquityCalc> calcMock = CreateCalcMock();
        IObliquityHandler handler = new ObliquityHandler(calcMock.Object);
        ObliquityResponse response = handler.CalcObliquity(new ObliquityRequest(_jdUt, _useTrueObliquity));
        Assert.AreEqual(_expectedObliquity, response.Obliquity, _delta);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
    }

    [Test]
    public void TextSeException()
    {
        Mock<IObliquityCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IObliquityHandler handler = new ObliquityHandler(calcExceptionMock.Object);
        ObliquityResponse response = handler.CalcObliquity(new ObliquityRequest(_jdUt, _useTrueObliquity));
        Assert.IsFalse(response.Success);
        Assert.AreEqual(_errorText, response.ErrorText);
    }


    private Mock<IObliquityCalc> CreateCalcMock()
    {
        var mock = new Mock<IObliquityCalc>();
        mock.Setup(p => p.CalculateObliquity(_jdUt, _useTrueObliquity)).Returns(_expectedObliquity);
        return mock;
    }

    private Mock<IObliquityCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IObliquityCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.CalculateObliquity(_jdUt, _useTrueObliquity)).Throws(exception);
        return mock;
    }

}