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

namespace Enigma.Test.Api.Calc;


[TestFixture]
public class TestHorizontalApi
{
    private const double JdUt = 123456.789;
    private const double Delta = 0.00000001;
    private const double ExpectedAzimuth = 222.2;
    private const double ExpectedAltitude = 45.45;
    private readonly Location _location = new("Anywhere", 55.5, 22.2);
    private readonly EquatorialCoordinates _equCoordinates = new(111.1, 2.2);
    private IHorizontalApi? _api;

    [SetUp]
    public void SetUp()
    {
        var horCoordinates = new HorizontalCoordinates(ExpectedAzimuth, ExpectedAltitude);
        var mockHorHandler = new Mock<IHorizontalHandler>();
        mockHorHandler.Setup(p => p.CalcHorizontal(It.IsAny<HorizontalRequest>())).Returns(horCoordinates);
        _api = new HorizontalApi(mockHorHandler.Object);
    }


    [Test]
    public void TestHorizontalHappyFlow()
    {
        var horizontalRequest = new HorizontalRequest(JdUt, _location, _equCoordinates);
        HorizontalCoordinates horCoordinates = _api!.GetHorizontal(horizontalRequest);
        Assert.Multiple(() =>
        {
            Assert.That(horCoordinates.Azimuth, Is.EqualTo(ExpectedAzimuth).Within(Delta));
            Assert.That(horCoordinates.Altitude, Is.EqualTo(ExpectedAltitude).Within(Delta));
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
        HorizontalRequest request = new(JdUt, location!, _equCoordinates);
        Assert.That(() => _api!.GetHorizontal(request), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestHorizontalEclcoordinatesNullRequest()
    {
        EquatorialCoordinates? coordinates = null;
        HorizontalRequest request = new(JdUt, _location, coordinates!);
        Assert.That(() => _api!.GetHorizontal(request), Throws.TypeOf<ArgumentNullException>());
    }

}








