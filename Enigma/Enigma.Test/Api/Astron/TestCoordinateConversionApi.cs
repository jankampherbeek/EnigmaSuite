﻿// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Astron;
using Enigma.Api.Interfaces;
using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.RequestResponse;
using Moq;

namespace Enigma.Test.Api.Astron;


[TestFixture]
public class TestCoordinateConversionApi
{
    private readonly double _delta = 0.00000001;
    private readonly double _obliquity = 23.447;
    private readonly bool _expectedSuccess = true;
    private readonly string _expectedErrorText = "";
    private readonly EquatorialCoordinates _expectedEqCoord = new(221.1, 4.4);
    private ICoordinateConversionApi _api;

    [SetUp]
    public void SetUp()
    {
        CoordinateConversionRequest _coordConvRequest = CreateConvRequest();
        var _mockCoordConvHandler = new Mock<ICoordinateConversionHandler>();
        _mockCoordConvHandler.Setup(p => p.HandleConversion(_coordConvRequest)).Returns(new CoordinateConversionResponse(_expectedEqCoord, _expectedSuccess, _expectedErrorText));
        _api = new CoordinateConversionApi(_mockCoordConvHandler.Object);
    }

    [Test]
    public void TestCoordinateConversionHappyFlow()
    {
        CoordinateConversionRequest _coordConvRequest = CreateConvRequest();
        CoordinateConversionResponse response = _api.GetEquatorialFromEcliptic(_coordConvRequest);
        Assert.Multiple(() =>
        {
            Assert.That(response.EquatorialCoord.RightAscension, Is.EqualTo(_expectedEqCoord.RightAscension).Within(_delta));
            Assert.That(response.Success, Is.True);
            Assert.That(response.ErrorText, Is.EqualTo(""));
        });
    }

    [Test]
    public void TestCoordinateConversionNullRequest()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.That(() => _api.GetEquatorialFromEcliptic(null), Throws.TypeOf<ArgumentNullException>());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Test]
    public void TestCoordinateConversionNullCoordinates()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        CoordinateConversionRequest errorRequest = new(null, _obliquity);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.That(() => _api.GetEquatorialFromEcliptic(errorRequest), Throws.TypeOf<ArgumentNullException>());
    }

    private CoordinateConversionRequest CreateConvRequest()
    {
        return new CoordinateConversionRequest(new EclipticCoordinates(222.2, 1.1), _obliquity);
    }
}







