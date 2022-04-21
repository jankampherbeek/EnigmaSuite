// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.api.astron;
using E4C.core.astron.coordinateconversion;
using E4C.core.astron.horizontal;
using E4C.core.astron.obliquity;
using E4C.core.shared.domain;
using E4C.domain.shared.specifications;
using E4C.shared.domain;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;
using System;

namespace E4CTest.core.api.astron;


[TestFixture]
public class TestCoordinateConversionApi
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly double _obliquity = 23.447;
    private HorizontalCoordinates _horCoordinates;
    private EclipticCoordinates _eclCoordinates;
    private Location _location;
    private readonly bool _expectedSuccess = true;
    private readonly string _expectedErrorText = "";
    private CoordinateConversionRequest _coordConvRequest;
    private EquatorialCoordinates _expectedEqCoord;
    private Mock<ICoordinateConversionHandler> _mockCoordConvHandler;
    private ICoordinateConversionApi _api;

    [SetUp]
    public void SetUp()
    {
        _location = new Location("Anywhere", 55.5, 22.2);
        _coordConvRequest = new CoordinateConversionRequest(new EclipticCoordinates(222.2, 1.1), _obliquity);
        _expectedEqCoord = new EquatorialCoordinates(221.1, 4.4);
        _eclCoordinates = new EclipticCoordinates(111.1, 2.2);
        _mockCoordConvHandler = new Mock<ICoordinateConversionHandler>();
        _mockCoordConvHandler.Setup(p => p.HandleConversion(_coordConvRequest)).Returns(new CoordinateConversionResponse(_expectedEqCoord, _expectedSuccess, _expectedErrorText));

        _api = new CoordinateConversionApi(_mockCoordConvHandler.Object);
    }

    [Test]
    public void TestCoordinateConversionHappyFlow()
    {
        CoordinateConversionResponse response = _api.getEquatorialFromEcliptic(_coordConvRequest);
        Assert.AreEqual(_expectedEqCoord.RightAscension, response.equatorialCoord.RightAscension, _delta);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
    }

    [Test]
    public void TestCoordinateConversionNullRequest()
    {
        Assert.That(() => _api.getEquatorialFromEcliptic(null), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void TestCoordinateConversionNullCoordinates()
    {
        CoordinateConversionRequest errorRequest = new CoordinateConversionRequest(null, _obliquity);
        Assert.That(() => _api.getEquatorialFromEcliptic(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }
}








