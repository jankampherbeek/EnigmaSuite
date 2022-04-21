// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.core.astron.horizontal;
using E4C.core.shared.domain;
using E4C.domain.shared.specifications;
using E4C.exceptions;
using E4C.shared.reqresp;
using Moq;
using NUnit.Framework;


namespace E4CTest.core.astron.horizontal;

[TestFixture] 
public class TestHorizontalHandler
{
    private readonly double _delta = 0.00000001;
    private readonly double _jdUt = 123456.789;
    private readonly Location _location = new("Anywhere", 50.0, 10.0);
    private readonly EclipticCoordinates _eclipticCoordinates = new(160.0, 3.3);
    private readonly int _flags = 0;
    private readonly double[] _expectedResults = {222.2, 33.3 };
    private readonly string _errorText = "Description of problem.";


    [Test]
    public void TestHappyFlow()
    {
        Mock<IHorizontalCalc> calcMock = CreateCalcMock();
        IHorizontalHandler handler = new HorizontalHandler(calcMock.Object);
        HorizontalResponse response = handler.CalcHorizontal(new HorizontalRequest(_jdUt, _location, _eclipticCoordinates));
        Assert.AreEqual(_expectedResults[0], response.HorizontalAzimuthAltitude.Azimuth, _delta);
        Assert.AreEqual(_expectedResults[1], response.HorizontalAzimuthAltitude.Altitude, _delta);
        Assert.IsTrue(response.Success);
        Assert.AreEqual("", response.ErrorText);
    }

    [Test]
    public void TextSeException()
    {
        Mock<IHorizontalCalc> calcExceptionMock = CreateCalcMockThrowingException();
        IHorizontalHandler handler = new HorizontalHandler(calcExceptionMock.Object);
        HorizontalResponse response = handler.CalcHorizontal(new HorizontalRequest(_jdUt, _location, _eclipticCoordinates));
        Assert.IsFalse(response.Success);
        Assert.AreEqual(_errorText, response.ErrorText);
    }


    private Mock<IHorizontalCalc> CreateCalcMock()
    {
        var mock = new Mock<IHorizontalCalc>();
        mock.Setup(p => p.CalculateHorizontal(_jdUt, _location, _eclipticCoordinates, _flags)).Returns(_expectedResults);
        return mock;
    }

    private Mock<IHorizontalCalc> CreateCalcMockThrowingException()
    {
        var mock = new Mock<IHorizontalCalc>();
        var exception = new SwissEphException(_errorText);
        mock.Setup(p => p.CalculateHorizontal(_jdUt, _location, _eclipticCoordinates, _flags)).Throws(exception);
        return mock;
    }
}
