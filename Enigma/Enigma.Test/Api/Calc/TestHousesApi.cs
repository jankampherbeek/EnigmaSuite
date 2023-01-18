// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Astron;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Api.Astron;


[TestFixture]
public class TestHousesApi
{
    private readonly double _jdUt = 123456.789;
    private FullHousesPosRequest _housesRequest;
    private FullHousesPositions _fullHousesPositions;
    private Mock<IHousesHandler> _mockHousesHandler;
    private readonly HouseSystems _houseSystem = HouseSystems.Apc;

    private IHousesApi _api;


    [SetUp]
    public void SetUp()
    {

        var _location = new Location("Anywhere", 50.0, 10.0);
        _housesRequest = new FullHousesPosRequest(_jdUt, _location, _houseSystem);
        _fullHousesPositions = CreateResponse();
        _mockHousesHandler = new Mock<IHousesHandler>();
        _mockHousesHandler.Setup(p => p.CalcHouses(_housesRequest)).Returns(_fullHousesPositions);
        _api = new HousesApi(_mockHousesHandler.Object);
    }


    [Test]
    public void TestHousesHappyFlow()
    {
        Assert.That(_api.GetHouses(_housesRequest), Is.EqualTo(_fullHousesPositions));
    }

    [Test]
    public void TestRequest()
    {
        Assert.That(() => _api.GetHouses(null!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestNullLocation()
    {
        FullHousesPosRequest errorRequest = new(_jdUt, null!, _houseSystem);
        Assert.That(() => _api.GetHouses(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    private static FullHousesPositions CreateResponse()
    {


        var cusps = new List<FullChartPointPos>
        {
            CreateFullChartPointPos(ChartPoints.Cusp5, 100.0, 101.1, 2.2, 99.9, 3.3),
            CreateFullChartPointPos(ChartPoints.Cusp6, 130.0, 131.1, 2.3, 119.9, 2.2)
        };

        FullChartPointPos mc = CreateFullChartPointPos(ChartPoints.Mc, 290.0, 292.1, 2.3, 310.9, 1.2);
        FullChartPointPos ascendant = CreateFullChartPointPos(ChartPoints.Ascendant, 20.0, 22.2, -1.1, 40.9, -3.5);
        FullChartPointPos vertex = CreateFullChartPointPos(ChartPoints.Vertex, 205.0, 202.2, -1.14, 220.9, -5.5);
        FullChartPointPos eastPoint = CreateFullChartPointPos(ChartPoints.EastPoint, 25.0, 27.2, -1.1, 45.9, -0.5);
        return new FullHousesPositions(cusps, mc, ascendant, vertex, eastPoint);
    }


    private static FullChartPointPos CreateFullChartPointPos(ChartPoints point, double longitude, double ra, double decl, double azimuth, double altitude)
    {
        PosSpeed distance = new (0.0, 0.0);
        PosSpeed psLongitude = new(longitude, 0.0);
        PosSpeed psLatitude = new(0.0, 0.0);
        PosSpeed psRightAscension = new(ra, 0.0);
        PosSpeed psDeclination = new(decl, 0.0);
        HorizontalCoordinates horCoord = new(azimuth, altitude);
        FullPointPos fpPos = new(psLongitude, psLatitude, psRightAscension, psDeclination, horCoord);
        return new FullChartPointPos(point, distance, fpPos);
    }  

}








