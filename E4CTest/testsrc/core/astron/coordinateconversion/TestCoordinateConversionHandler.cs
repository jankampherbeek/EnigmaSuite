// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.CoordinateConversion;
using E4C.Core.Shared.Domain;
using E4C.Exceptions;
using E4C.Shared.ReqResp;
using Moq;
using NUnit.Framework;


namespace E4CTest.core.astron.coordinateconversion;

[TestFixture]
public class TestCoordinateConversionHandler
{

    private CoordinateConversionRequest _request;
    private EclipticCoordinates _eclCoord;
    private EquatorialCoordinates _eqCoord;
    private readonly double _obliquity = 23.447;
    private readonly double _delta = 0.00000001;
    private readonly string _errorText = "Description of error.";

    [Test]
    public void TestHappyFlow()
    {
        _eclCoord = new EclipticCoordinates(222.2, 1.1);
        _eqCoord = new EquatorialCoordinates(223.3, -3.3);
        _request = new CoordinateConversionRequest(_eclCoord, _obliquity);
        Mock<ICoordinateConversionCalc> calcMock = CreateCalcMock();
        ICoordinateConversionHandler handler = new CoordinateConversionHandler(calcMock.Object);
        CoordinateConversionResponse response = handler.HandleConversion(_request);
        Assert.AreEqual(_eqCoord.RightAscension, response.equatorialCoord.RightAscension, _delta);
        Assert.AreEqual(_eqCoord.Declination, response.equatorialCoord.Declination, _delta);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
    }

    [Test]
    public void TestSeException()
    {
        _eclCoord = new EclipticCoordinates(222.2, 1.1);
        _eqCoord = new EquatorialCoordinates(223.3, -3.3);
        _request = new CoordinateConversionRequest(_eclCoord, _obliquity);
        Mock<ICoordinateConversionCalc> calcMock = CreateCalcMockThrowingException();
        ICoordinateConversionHandler handler = new CoordinateConversionHandler(calcMock.Object);
        CoordinateConversionResponse response = handler.HandleConversion(_request);
        Assert.IsFalse(response.Success);
        Assert.AreEqual(_errorText, response.ErrorText);
    }



    private Mock<ICoordinateConversionCalc> CreateCalcMock()
    {
        var mock = new Mock<ICoordinateConversionCalc>();
        mock.Setup(p => p.PerformConversion(_eclCoord, _obliquity)).Returns(_eqCoord);
        return mock;
    }


    private Mock<ICoordinateConversionCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<ICoordinateConversionCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.PerformConversion(_eclCoord, _obliquity)).Throws(exception);
        return mock;
    }


}