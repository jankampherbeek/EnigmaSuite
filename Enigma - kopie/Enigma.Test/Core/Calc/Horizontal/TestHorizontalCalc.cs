// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Obliquity;
using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.Locational;
using Enigma.Domain.Positional;
using Enigma4C.Core.Calc.Horizontal;
using Moq;


namespace Enigma.Test.Core.Calc.Horizontal;

[TestFixture]
public class TestHorizontalCalc
{
    private readonly double _delta = 0.00000001;
    private readonly double _julianDay = 123456.789;
    private readonly double[] _geoGraphicCoordinates = { 10.0, 50.0 };
    private readonly Location _location = new("Anywhere", 10.0, 50.0);
    private EclipticCoordinates _eclipticCoordinates;
    private readonly double[] _eclipticValues = { 200.0, -2.0 };
    private readonly double[] _expectedHorCoord = { 222.2, 44.4 };
    private double[] _actualResults;
    private readonly int _flags = 0;


    [SetUp]
    public void SetUp()
    {
        _eclipticCoordinates = new EclipticCoordinates(200.0, -2.0);
        PerformCalculation();
    }

    [Test]
    public void TestResultForAzimuth()
    {
        Assert.That(_actualResults[0], Is.EqualTo(_expectedHorCoord[0]).Within(_delta));
    }

    [Test]
    public void TestResultForAltitude()
    {
        Assert.That(_actualResults[1], Is.EqualTo(_expectedHorCoord[1]).Within(_delta));
    }


    public void PerformCalculation()
    {
        var mockFacade = new Mock<IAzAltFacade>();
        mockFacade.Setup(p => p.RetrieveHorizontalCoordinates(_julianDay, _geoGraphicCoordinates, _eclipticValues, _flags)).Returns(_expectedHorCoord);
        var _horizontalCalc = new HorizontalCalc(mockFacade.Object);
        _actualResults = _horizontalCalc.CalculateHorizontal(_julianDay, _location, _eclipticCoordinates, _flags);
    }




    private static ObliquityCalc CreateObliquityCalc()
    {
        int celpointId = -1;
        int flags = 0;
        double jd = 12345.678;
        double[] positions = { 23.448, 23.447, 0.0, 0.0, 0.0, 0.0 };
        var mock = new Mock<ICalcUtFacade>();
        mock.Setup(p => p.PosCelPointFromSe(jd, celpointId, flags)).Returns(positions);
        ObliquityCalc calc = new(mock.Object);
        return calc;
    }
}