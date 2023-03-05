// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Calc.Coordinates.Helpers;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Facades.Interfaces;
using Moq;


namespace Enigma.Test.Core.Handlers.Calc.CelestialPoints.Helpers;

[TestFixture]
public class TestHorizontalCalc
{
    private readonly double _delta = 0.00000001;
    private readonly double _julianDay = 123456.789;
    private readonly double[] _geoGraphicCoordinates = { 10.0, 50.0 };
    private readonly Location _location = new("Anywhere", 10.0, 50.0);
    private EquatorialCoordinates _equCoordinates;
    private readonly double[] _eclipticValues = { 200.0, -2.0 };
    private readonly double[] _expectedHorCoord = { 222.2, 44.4 };
    private double[] _actualResults;
    private readonly int _flags = 0;


    [SetUp]
    public void SetUp()
    {
        _equCoordinates = new EquatorialCoordinates(200.0, -2.0);
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


    private void PerformCalculation()
    {
        var mockFacade = new Mock<IAzAltFacade>();
        mockFacade.Setup(p => p.RetrieveHorizontalCoordinates(_julianDay, _geoGraphicCoordinates, _eclipticValues, _flags)).Returns(_expectedHorCoord);
        var _horizontalCalc = new HorizontalCalc(mockFacade.Object);
        _actualResults = _horizontalCalc.CalculateHorizontal(_julianDay, _location, _equCoordinates, _flags);
    }



}