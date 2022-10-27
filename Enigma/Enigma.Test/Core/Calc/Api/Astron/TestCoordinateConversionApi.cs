// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.Astron;
using Enigma.Core.Calc.CoordinateConversion;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Positional;
using Moq;

namespace Enigma.Test.Core.Calc.Api.Astron;


[TestFixture]
public class TestCoordinateConversionApi
{
    private readonly double _delta = 0.00000001;
    private readonly double _obliquity = 23.447;
    private readonly bool _expectedSuccess = true;
    private readonly string _expectedErrorText = "";
    private readonly EquatorialCoordinates _expectedEqCoord = new EquatorialCoordinates(221.1, 4.4);
    private ICoordinateConversionApi _api;

    [SetUp]
    public void SetUp()
    {
        CoordinateConversionRequest _coordConvRequest = CreateConvRequest();
        EclipticCoordinates _eclCoordinates = new EclipticCoordinates(111.1, 2.2);
        var _mockCoordConvHandler = new Mock<ICoordinateConversionHandler>();
        _mockCoordConvHandler.Setup(p => p.HandleConversion(_coordConvRequest)).Returns(new CoordinateConversionResponse(_expectedEqCoord, _expectedSuccess, _expectedErrorText));
        _api = new CoordinateConversionApi(_mockCoordConvHandler.Object);
    }

    [Test]
    public void TestCoordinateConversionHappyFlow()
    {
        CoordinateConversionRequest _coordConvRequest = CreateConvRequest();
        CoordinateConversionResponse response = _api.getEquatorialFromEcliptic(_coordConvRequest);
        Assert.That(response.equatorialCoord.RightAscension, Is.EqualTo(_expectedEqCoord.RightAscension).Within(_delta));
        Assert.That(response.Success, Is.True);
        Assert.That(response.ErrorText, Is.EqualTo(""));
    }

    [Test]
    public void TestCoordinateConversionNullRequest()
    {
        Assert.That(() => _api.getEquatorialFromEcliptic(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestCoordinateConversionNullCoordinates()
    {
        CoordinateConversionRequest errorRequest = new CoordinateConversionRequest(null, _obliquity);
        Assert.That(() => _api.getEquatorialFromEcliptic(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    private CoordinateConversionRequest CreateConvRequest()
    {
        return new CoordinateConversionRequest(new EclipticCoordinates(222.2, 1.1), _obliquity);
    }
}








