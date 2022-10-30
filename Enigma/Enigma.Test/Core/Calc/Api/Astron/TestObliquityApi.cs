// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.Astron;
using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.Obliquity;
using Enigma.Core.Calc.ReqResp;
using Moq;

namespace Enigma.Test.Core.Calc.Api.Astron;


[TestFixture]
public class TestObliquityApi
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly double _expectedTrueObliquity = 23.447;
    private readonly double _expectedMeanObliquity = 23.447001;
    private readonly bool _expectedSuccess = true;
    private readonly string _expectedErrorText = "";
    private ObliquityRequest _obliquityRequest;
    private Mock<IObliquityHandler> _mockObliquityHandler;

    private IObliquityApi _obliquityApi;


    [SetUp]
    public void SetUp()
    {
        _obliquityRequest = new ObliquityRequest(_jdUt);
        _mockObliquityHandler = new Mock<IObliquityHandler>();
        _mockObliquityHandler.Setup(p => p.CalcObliquity(_obliquityRequest)).Returns(new ObliquityResponse(_expectedMeanObliquity,  _expectedTrueObliquity, _expectedSuccess, _expectedErrorText));
        _obliquityApi = new ObliquityApi(_mockObliquityHandler.Object);
    }


    [Test]
    public void TestObliquityHappyFlow()
    {
        ObliquityResponse response = _obliquityApi.GetObliquity(_obliquityRequest);
        Assert.That(response.ObliquityTrue, Is.EqualTo(_expectedTrueObliquity).Within(_delta));
        Assert.That(response.ObliquityMean, Is.EqualTo(_expectedMeanObliquity).Within(_delta));
        Assert.IsTrue(response.Success);
        Assert.That(response.ErrorText, Is.EqualTo(_expectedErrorText));
    }

    [Test]
    public void TestObliquityNullRequest()
    {
        Assert.That(() => _obliquityApi.GetObliquity(null), Throws.TypeOf<ArgumentNullException>());
    }


}








