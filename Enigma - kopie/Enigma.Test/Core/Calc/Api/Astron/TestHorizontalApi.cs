// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.Astron;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Locational;
using Enigma.Domain.Positional;
using Enigma4C.Core.Calc.Horizontal;
using Moq;

namespace Enigma.Test.Core.Calc.Api.Astron;


[TestFixture]
public class TestHorizontalApi
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly double _expectedAzimuth = 222.2;
    private readonly double _expectedAltitude = 45.45;
    private readonly bool _expectedSuccess = true;
    private readonly string _expectedErrorText = "";
    private readonly Location _location = new("Anywhere", 55.5, 22.2);
    private readonly EclipticCoordinates _eclCoordinates = new EclipticCoordinates(111.1, 2.2);
    private IHorizontalApi _api;

    [SetUp]
    public void SetUp()
    {
        var _horCoordinates = new HorizontalCoordinates(_expectedAzimuth, _expectedAltitude);
        var _mockHorHandler = new Mock<IHorizontalHandler>();
        _mockHorHandler.Setup(p => p.CalcHorizontal(It.IsAny<HorizontalRequest>())).Returns(new HorizontalResponse(_horCoordinates, _expectedSuccess, _expectedErrorText));
        _api = new HorizontalApi(_mockHorHandler.Object);
    }


    [Test]
    public void TestHorizontalHappyFlow()
    {

        var _horizontalRequest = new HorizontalRequest(_jdUt, _location, _eclCoordinates);
        HorizontalResponse response = _api.getHorizontal(_horizontalRequest);
        Assert.That(response.HorizontalAzimuthAltitude.Azimuth, Is.EqualTo(_expectedAzimuth).Within(_delta));
        Assert.That(response.HorizontalAzimuthAltitude.Altitude, Is.EqualTo(_expectedAltitude).Within(_delta));
        Assert.That(response.Success, Is.True);
        Assert.That(response.ErrorText, Is.EqualTo(""));
    }

    [Test]
    public void TestHorizontalNullRequest()
    {
        Assert.That(() => _api.getHorizontal(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestHorizontalLocationNullRequest()
    {
        HorizontalRequest request = new HorizontalRequest(_jdUt, null, _eclCoordinates);
        Assert.That(() => _api.getHorizontal(request), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestHorizontalEclcoordinatesNullRequest()
    {
        HorizontalRequest request = new HorizontalRequest(_jdUt, _location, null);
        Assert.That(() => _api.getHorizontal(request), Throws.TypeOf<ArgumentNullException>());
    }



}








