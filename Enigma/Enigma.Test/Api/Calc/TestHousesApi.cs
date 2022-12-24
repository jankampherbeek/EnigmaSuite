// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Astron;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Api.Astron;


[TestFixture]
public class TestHousesApi
{
    private readonly double _jdUt = 123456.789;
    private readonly string _expectedErrorText = "";
    private FullHousesPosRequest _housesRequest;
    private FullHousesPosResponse _housesResponse;
    private Mock<IHousesHandler> _mockHousesHandler;
    private readonly HouseSystems _houseSystem = HouseSystems.Apc;

    private IHousesApi _api;


    [SetUp]
    public void SetUp()
    {

        var _location = new Location("Anywhere", 50.0, 10.0);
        _housesRequest = new FullHousesPosRequest(_jdUt, _location, _houseSystem);
        _housesResponse = CreateResponse();
        _mockHousesHandler = new Mock<IHousesHandler>();
        _mockHousesHandler.Setup(p => p.CalcHouses(_housesRequest)).Returns(_housesResponse);
        _api = new HousesApi(_mockHousesHandler.Object);
    }


    [Test]
    public void TestHousesHappyFlow()
    {
        FullHousesPosResponse actualResponse = _api.GetHouses(_housesRequest);
        Assert.Multiple(() =>
        {
            Assert.That(actualResponse, Is.EqualTo(_housesResponse));
            Assert.That(actualResponse.Success, Is.True);
            Assert.That(actualResponse.ErrorText, Is.EqualTo(_expectedErrorText));
        });
    }

    [Test]
    public void TestRequest()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.That(() => _api.GetHouses(null), Throws.TypeOf<ArgumentNullException>());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Test]
    public void TestNullLocation()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        FullHousesPosRequest errorRequest = new(_jdUt, null, _houseSystem);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.That(() => _api.GetHouses(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    private static FullHousesPosResponse CreateResponse()
    {
        var cusps = new List<CuspFullPos>
        {
            new CuspFullPos("Cusp 5", 100.0, new EquatorialCoordinates(101.1, 2.2), new HorizontalCoordinates(99.9, 3.3)),
            new CuspFullPos("Cusp 6", 130.0, new EquatorialCoordinates(131.1, 2.3), new HorizontalCoordinates(119.9, 2.2))
        };
        CuspFullPos mc = new(MundanePoints.Mc.ToString(), 290.0, new EquatorialCoordinates(292.1, 2.3), new HorizontalCoordinates(310.9, 1.2));
        CuspFullPos ascendant = new(MundanePoints.Ascendant.ToString(), 20.0, new EquatorialCoordinates(22.2, -1.1), new HorizontalCoordinates(40.9, -3.5));
        CuspFullPos vertex = new(MundanePoints.Vertex.ToString(), 205.0, new EquatorialCoordinates(202.2, -1.14), new HorizontalCoordinates(220.9, -5.5));
        CuspFullPos eastPoint = new(MundanePoints.EastPoint.ToString(), 25.0, new EquatorialCoordinates(27.2, -1.1), new HorizontalCoordinates(45.9, -0.5));
        FullHousesPositions fullHousesPositions = new(cusps, mc, ascendant, vertex, eastPoint);
        return new FullHousesPosResponse(fullHousesPositions, true, "");

    }

}








