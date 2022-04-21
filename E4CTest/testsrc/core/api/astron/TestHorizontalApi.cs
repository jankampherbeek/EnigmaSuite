// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.api.astron;
using E4C.core.astron.horizontal;
using E4C.core.shared.domain;
using E4C.domain.shared.specifications;
using E4C.shared.domain;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;
using System;

namespace E4CTest.core.api.astron;


[TestFixture]
public class TestHorizontalApi
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly double _expectedAzimuth = 222.2;
    private readonly double _expectedAltitude = 45.45;
    private EclipticCoordinates _eclCoordinates;
    private HorizontalCoordinates _horCoordinates;
    private Location _location;
    private readonly bool _expectedSuccess = true;
    private readonly string _expectedErrorText = "";
    private HorizontalRequest _horizontalRequest;
    private Mock<IHorizontalHandler> _mockHorHandler;
    private IHorizontalApi _api;

    [SetUp]
    public void SetUp()
    {
        _location = new Location("Anywhere", 55.5, 22.2);
        _eclCoordinates = new EclipticCoordinates(111.1, 2.2);
        _horCoordinates = new HorizontalCoordinates(_expectedAzimuth, _expectedAltitude);
        _horizontalRequest = new HorizontalRequest(_jdUt, _location, _eclCoordinates);
        _mockHorHandler = new Mock<IHorizontalHandler>();
        _mockHorHandler.Setup(p => p.CalcHorizontal(_horizontalRequest)).Returns(new HorizontalResponse(_horCoordinates, _expectedSuccess, _expectedErrorText));
        _api = new HorizontalApi(_mockHorHandler.Object);
    }


    [Test]
    public void TestHorizontalHappyFlow()
    {
        HorizontalResponse response = _api.getHorizontal(_horizontalRequest);
        Assert.AreEqual(_expectedAzimuth, response.HorizontalAzimuthAltitude.Azimuth, _delta);
        Assert.AreEqual(_expectedAltitude, response.HorizontalAzimuthAltitude.Altitude, _delta);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
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








