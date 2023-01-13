// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Astron;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Moq;

namespace Enigma.Test.Api.Astron;


[TestFixture]
public class TestHorizontalApi
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly double _expectedAzimuth = 222.2;
    private readonly double _expectedAltitude = 45.45;
    private readonly Location _location = new("Anywhere", 55.5, 22.2);
    private readonly EclipticCoordinates _eclCoordinates = new(111.1, 2.2);
    private IHorizontalApi _api;

    [SetUp]
    public void SetUp()
    {
        var _horCoordinates = new HorizontalCoordinates(_expectedAzimuth, _expectedAltitude);
        var _mockHorHandler = new Mock<IHorizontalHandler>();
        _mockHorHandler.Setup(p => p.CalcHorizontal(It.IsAny<HorizontalRequest>())).Returns(_horCoordinates);
        _api = new HorizontalApi(_mockHorHandler.Object);
    }


    [Test]
    public void TestHorizontalHappyFlow()
    {
        var _horizontalRequest = new HorizontalRequest(_jdUt, _location, _eclCoordinates);
        HorizontalCoordinates horCoordinates = _api.GetHorizontal(_horizontalRequest);
        Assert.Multiple(() =>
        {
            Assert.That(horCoordinates.Azimuth, Is.EqualTo(_expectedAzimuth).Within(_delta));
            Assert.That(horCoordinates.Altitude, Is.EqualTo(_expectedAltitude).Within(_delta));
        });
    }

    [Test]
    public void TestHorizontalNullRequest()
    {
        HorizontalRequest? request = null;
        Assert.That(() => _api.GetHorizontal(request!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestHorizontalLocationNullRequest()
    {
        Location? location = null;
        HorizontalRequest request = new(_jdUt, location!, _eclCoordinates);
        Assert.That(() => _api.GetHorizontal(request), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestHorizontalEclcoordinatesNullRequest()
    {
        EclipticCoordinates? coordinates = null;
        HorizontalRequest request = new(_jdUt, _location, coordinates!);
        Assert.That(() => _api.GetHorizontal(request), Throws.TypeOf<ArgumentNullException>());
    }

}








