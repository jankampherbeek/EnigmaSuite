// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api;
using Enigma.Api.Interfaces;
using Enigma.Core.Calc;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Moq;

namespace Enigma.Test.Api.Calc;


[TestFixture]
public class TestCoordinateConversionApi
{
    private const double DELTA = 0.00000001;
    private const double OBLIQUITY = 23.447;
    private readonly EquatorialCoordinates _expectedEqCoord = new(221.1, 4.4);
    private ICoordinateConversionApi? _api;

    [SetUp]
    public void SetUp()
    {
        CoordinateConversionRequest coordConvRequest = CreateConvRequest();
        var mockCoordConvHandler = new Mock<ICoordinateConversionHandler>();
        mockCoordConvHandler.Setup(p => p.HandleConversion(coordConvRequest)).Returns(_expectedEqCoord);
        _api = new CoordinateConversionApi(mockCoordConvHandler.Object);
    }

    [Test]
    public void TestCoordinateConversionHappyFlow()
    {
        CoordinateConversionRequest coordConvRequest = CreateConvRequest();
        EquatorialCoordinates coordinates = _api!.GetEquatorialFromEcliptic(coordConvRequest);
        Assert.That(coordinates.RightAscension, Is.EqualTo(_expectedEqCoord.RightAscension).Within(DELTA));
    }

    [Test]
    public void TestCoordinateConversionNullRequest()
    {
        CoordinateConversionRequest? request = null;
        Assert.That(() => _api!.GetEquatorialFromEcliptic(request!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestCoordinateConversionNullCoordinates()
    {
        EclipticCoordinates? eclCoord = null;
        CoordinateConversionRequest errorRequest = new(eclCoord!, OBLIQUITY);
        Assert.That(() => _api!.GetEquatorialFromEcliptic(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    private static CoordinateConversionRequest CreateConvRequest()
    {
        return new CoordinateConversionRequest(new EclipticCoordinates(222.2, 1.1), OBLIQUITY);
    }
}

