// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Moq;

namespace Enigma.Test.Api;


[TestFixture]
public class TestHorizontalApi
{
    private const double JD_UT = 123456.789;
    private const double DELTA = 0.00000001;
    private const double EXPECTED_AZIMUTH = 222.2;
    private const double EXPECTED_ALTITUDE = 45.45;
    private readonly Location _location = new("Anywhere", 55.5, 22.2);
    private readonly EquatorialCoordinates _equCoordinates = new(111.1, 2.2);
    private IHorizontalApi? _api;

    [SetUp]
    public void SetUp()
    {
        var horCoordinates = new HorizontalCoordinates(EXPECTED_AZIMUTH, EXPECTED_ALTITUDE);
        var mockHorHandler = new Mock<IHorizontalHandler>();
        mockHorHandler.Setup(p => p.CalcHorizontal(It.IsAny<HorizontalRequest>())).Returns(horCoordinates);
        _api = new HorizontalApi(mockHorHandler.Object);
    }


    [Test]
    public void TestHorizontalHappyFlow()
    {
        var horizontalRequest = new HorizontalRequest(JD_UT, _location, _equCoordinates);
        HorizontalCoordinates horCoordinates = _api!.GetHorizontal(horizontalRequest);
        Assert.Multiple(() =>
        {
            Assert.That(horCoordinates.Azimuth, Is.EqualTo(EXPECTED_AZIMUTH).Within(DELTA));
            Assert.That(horCoordinates.Altitude, Is.EqualTo(EXPECTED_ALTITUDE).Within(DELTA));
        });
    }

    [Test]
    public void TestHorizontalNullRequest()
    {
        HorizontalRequest? request = null;
        Assert.That(() => _api!.GetHorizontal(request!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestHorizontalLocationNullRequest()
    {
        Location? location = null;
        HorizontalRequest request = new(JD_UT, location!, _equCoordinates);
        Assert.That(() => _api!.GetHorizontal(request), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestHorizontalEclcoordinatesNullRequest()
    {
        EquatorialCoordinates? coordinates = null;
        HorizontalRequest request = new(JD_UT, _location, coordinates!);
        Assert.That(() => _api!.GetHorizontal(request), Throws.TypeOf<ArgumentNullException>());
    }

}








