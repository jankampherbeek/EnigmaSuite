// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Astron;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.Specials;
using Moq;

namespace Enigma.Test.Api.Astron;


[TestFixture]
public class TestObliquityApi
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly double _expectedTrueObliquity = 23.447;
    private ObliquityRequest _obliquityRequest;
    private Mock<IObliquityHandler> _mockObliquityHandler;

    private IObliquityApi _obliquityApi;


    [SetUp]
    public void SetUp()
    {
        _obliquityRequest = new ObliquityRequest(_jdUt, true);
        _mockObliquityHandler = new Mock<IObliquityHandler>();
        _mockObliquityHandler.Setup(p => p.CalcObliquity(_obliquityRequest)).Returns(_expectedTrueObliquity);
        _obliquityApi = new ObliquityApi(_mockObliquityHandler.Object);
    }


    [Test]
    public void TestObliquityHappyFlow()
    {
        double response = _obliquityApi.GetObliquity(_obliquityRequest);
        Assert.That(response, Is.EqualTo(_expectedTrueObliquity).Within(_delta));
    }

    [Test]
    public void TestObliquityNullRequest()
    {
        ObliquityRequest? request = null;
        Assert.That(() => _obliquityApi.GetObliquity(request!), Throws.TypeOf<ArgumentNullException>());
    }


}








