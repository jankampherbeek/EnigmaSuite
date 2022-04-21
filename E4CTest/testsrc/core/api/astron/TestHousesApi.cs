// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.api.astron;
using E4C.core.astron.houses;
using E4C.core.shared.domain;
using E4C.domain.shared.references;
using E4C.domain.shared.specifications;
using E4C.shared.domain;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace E4CTest.core.api.astron;


[TestFixture]
public class TestHousesApi
{
    private readonly double _jdUt = 123456.789;
    private readonly string _expectedErrorText = "";
    private FullHousesPosRequest _housesRequest;
    private FullHousesPosResponse _housesResponse;
    private Mock<IHousesHandler> _mockHousesHandler;
    HouseSystems _houseSystem = HouseSystems.Apc;

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
        FullHousesPosResponse actualResponse = _api.getHouses(_housesRequest);
        Assert.AreEqual(_housesResponse, actualResponse);
        Assert.IsTrue(actualResponse.Success);
        Assert.AreEqual(_expectedErrorText, actualResponse.ErrorText);
    }

    [Test]
    public void TestRequest()
    {
        Assert.That(() => _api.getHouses(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestNullLocation()
    {
        FullHousesPosRequest errorRequest = new(_jdUt, null, _houseSystem);
        Assert.That(() => _api.getHouses(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    private FullHousesPosResponse CreateResponse()
    {
        var cusps = new List<CuspFullPos>();
        cusps.Add(new CuspFullPos(100.0, new EquatorialCoordinates(101.1, 2.2), new HorizontalCoordinates(99.9, 3.3)));
        cusps.Add(new CuspFullPos(130.0, new EquatorialCoordinates(131.1, 2.3), new HorizontalCoordinates(119.9, 2.2)));
        CuspFullPos mc = new CuspFullPos(290.0, new EquatorialCoordinates(292.1, 2.3), new HorizontalCoordinates(310.9, 1.2));
        CuspFullPos ascendant = new CuspFullPos(20.0, new EquatorialCoordinates(22.2, -1.1), new HorizontalCoordinates(40.9, -3.5));
        CuspFullPos vertex = new CuspFullPos(205.0, new EquatorialCoordinates(202.2, -1.14), new HorizontalCoordinates(220.9, -5.5));
        CuspFullPos eastPoint = new CuspFullPos(25.0, new EquatorialCoordinates(27.2, -1.1), new HorizontalCoordinates(45.9, -0.5));
        FullHousesPositions fullHousesPositions = new FullHousesPositions(cusps, mc, ascendant, vertex, eastPoint);
        return new FullHousesPosResponse(fullHousesPositions, true, "");

    }

}








