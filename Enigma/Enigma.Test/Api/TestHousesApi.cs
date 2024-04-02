// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using FakeItEasy;

namespace Enigma.Test.Api;


[TestFixture]
public class TestHousesApi
{
    private const double JD_UT = 123456.789;
    private FullHousesPosRequest? _housesRequest;
    private Dictionary<ChartPoints, FullPointPos>? _fullHousesPositions;
    private IHousesHandler? _housesHandlerFake;
    private CalculationPreferences? _calculationPreferences;
    private IHousesApi? _api;


    [SetUp]
    public void SetUp()
    {
        _calculationPreferences = CreateCalculationPreferences();
        var location = new Location("Anywhere", 50.0, 10.0);
        _housesRequest = new FullHousesPosRequest(JD_UT, location, _calculationPreferences);
        _fullHousesPositions = CreateResponse();
        _housesHandlerFake = A.Fake<IHousesHandler>();
        A.CallTo(() => _housesHandlerFake.CalcHouses(_housesRequest)).Returns(_fullHousesPositions);
        _api = new HousesApi(_housesHandlerFake);
    }


    [Test]
    public void TestHousesHappyFlow()
    {
        Assert.That(_api!.GetHouses(_housesRequest!), Is.EqualTo(_fullHousesPositions));
    }

    [Test]
    public void TestRequest()
    {
        Assert.That(() => _api!.GetHouses(null!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestNullLocation()
    {
        FullHousesPosRequest errorRequest = new(JD_UT, null!, _calculationPreferences!);
        Assert.That(() => _api!.GetHouses(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    private static Dictionary<ChartPoints, FullPointPos> CreateResponse()
    {
        FullPointPos posCusp5 = CreateFullPointPos(100.0, 101.1, 2.2, 99.9, 3.3);
        FullPointPos posCusp6 = CreateFullPointPos(130.0, 131.1, 2.3, 119.9, 2.2);
        FullPointPos posMc = CreateFullPointPos(290.0, 292.1, 2.3, 310.9, 1.2);
        FullPointPos posAscendant = CreateFullPointPos(20.0, 22.2, -1.1, 40.9, -3.5);
        FullPointPos posVertex = CreateFullPointPos(205.0, 202.2, -1.14, 220.9, -5.5);
        FullPointPos posEastPoint = CreateFullPointPos(25.0, 27.2, -1.1, 45.9, -0.5);
        return new Dictionary<ChartPoints, FullPointPos>
        {
            { ChartPoints.Mc, posMc },
            { ChartPoints.Ascendant, posAscendant },
            { ChartPoints.Vertex, posVertex },
            { ChartPoints.EastPoint, posEastPoint },
            { ChartPoints.Cusp5, posCusp5 },
            { ChartPoints.Cusp6, posCusp6 }
        };
    }


    private static FullPointPos CreateFullPointPos(double longitude, double ra, double decl, double azimuth, double altitude)
    {
        PosSpeed psDistance = new(0.0, 0.0);
        PosSpeed psLongitude = new(longitude, 0.0);
        PosSpeed psLatitude = new(0.0, 0.0);
        PosSpeed psRightAscension = new(ra, 0.0);
        PosSpeed psDeclination = new(decl, 0.0);
        PosSpeed psAzimuth = new(azimuth, 0.0);
        PosSpeed psAltitude = new(altitude, 0.0);
        PointPosSpeeds ppsEcliptical = new(psLongitude, psLatitude, psDistance);
        PointPosSpeeds ppsEquatorial = new(psRightAscension, psDeclination, psDistance);
        PointPosSpeeds ppsHorizontal = new(psAzimuth, psAltitude, psDistance);
        return new FullPointPos(ppsEcliptical, ppsEquatorial, ppsHorizontal);
    }

    private static CalculationPreferences CreateCalculationPreferences()
    {
        List<ChartPoints> points = new()
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mc,
            ChartPoints.Ascendant,
            ChartPoints.Vertex,
            ChartPoints.EastPoint,
            ChartPoints.Cusp5,
            ChartPoints.Cusp6
        };
        return new CalculationPreferences(points, ZodiacTypes.Tropical, Ayanamshas.None, 
            CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ProjectionTypes.TwoDimensional, 
            HouseSystems.Apc);
    }

}








