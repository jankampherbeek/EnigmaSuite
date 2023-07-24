// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Astron;
using Enigma.Api.Calc;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Calc.Coordinates.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Api.Astron;


[TestFixture]
public class TestCoordinateConversionApi
{
    private readonly double _delta = 0.00000001;
    private readonly double _obliquity = 23.447;
    private readonly EquatorialCoordinates _expectedEqCoord = new(221.1, 4.4);
    private ICoordinateConversionApi _api;

    [SetUp]
    public void SetUp()
    {
        CoordinateConversionRequest _coordConvRequest = CreateConvRequest();
        var _mockCoordConvHandler = new Mock<ICoordinateConversionHandler>();
        _mockCoordConvHandler.Setup(p => p.HandleConversion(_coordConvRequest)).Returns(_expectedEqCoord);
        _api = new CoordinateConversionApi(_mockCoordConvHandler.Object);
    }

    [Test]
    public void TestCoordinateConversionHappyFlow()
    {
        CoordinateConversionRequest _coordConvRequest = CreateConvRequest();
        EquatorialCoordinates coordinates = _api.GetEquatorialFromEcliptic(_coordConvRequest);
        Assert.That(coordinates.RightAscension, Is.EqualTo(_expectedEqCoord.RightAscension).Within(_delta));
    }

    [Test]
    public void TestCoordinateConversionNullRequest()
    {
        CoordinateConversionRequest? request = null;
        Assert.That(() => _api.GetEquatorialFromEcliptic(request!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestCoordinateConversionNullCoordinates()
    {
        EclipticCoordinates? eclCoord = null;
        CoordinateConversionRequest errorRequest = new(eclCoord!, _obliquity);
        Assert.That(() => _api.GetEquatorialFromEcliptic(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    private CoordinateConversionRequest CreateConvRequest()
    {
        return new CoordinateConversionRequest(new EclipticCoordinates(222.2, 1.1), _obliquity);
    }
}

