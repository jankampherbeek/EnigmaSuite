// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.api;
using E4C.core.astron.coordinateconversion;
using E4C.core.astron.obliquity;
using E4C.core.shared.domain;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;
using System;

namespace E4CTest.core.api;


[TestFixture]
public class TestApiForObliquity
{
    private readonly double _jdUt = 123456.789;
    private readonly double _delta = 0.00000001;
    private readonly bool _useTrueObliquity = true;
    private readonly double _expectedObliquity = 23.447;
    private readonly double _obliquity = 23.447;
    private readonly bool _expectedSuccess = true;
    private readonly string _expectedErrorText = "";
    private CoordinateConversionRequest _coordConvRequest;
    private EquatorialCoordinates _expectedEqCoord;
    private ObliquityRequest _obliquityRequest;
    private Mock<IObliquityHandler> _mockObliquityHandler;
    private Mock<ICoordinateConversionHandler> _mockCoordConvHandler;
    private IAstronApi _api;

    [SetUp]
    public void SetUp()
    {
        _coordConvRequest = new CoordinateConversionRequest(new EclipticCoordinates(222.2, 1.1), _obliquity);
        _expectedEqCoord = new EquatorialCoordinates(221.1, 4.4);
        _obliquityRequest = new ObliquityRequest(_jdUt, _useTrueObliquity);
        _mockObliquityHandler = new Mock<IObliquityHandler>();
        _mockObliquityHandler.Setup(p => p.CalcObliquity(_obliquityRequest)).Returns(new ObliquityResponse(_expectedObliquity, _expectedSuccess, _expectedErrorText));
        _mockCoordConvHandler = new Mock<ICoordinateConversionHandler>();
        _mockCoordConvHandler.Setup(p => p.HandleConversion(_coordConvRequest)).Returns(new CoordinateConversionResponse(_expectedEqCoord, _expectedSuccess, _expectedErrorText));
        _api = new AstronApi(_mockCoordConvHandler.Object, _mockObliquityHandler.Object);
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
    public void TestObliquityHappyFlow()
    {
        ObliquityResponse response = _api.getObliquity(_obliquityRequest);
        Assert.AreEqual(_expectedObliquity, response.Obliquity, _delta);
        Assert.IsTrue(response.Success);
        Assert.AreEqual(_expectedErrorText, response.ErrorText);
    }

    [Test]
    public void TestObliquityNullRequest()
    {
        Assert.That(() => _api.getObliquity(null), Throws.TypeOf <ArgumentNullException> ());
    }


}








