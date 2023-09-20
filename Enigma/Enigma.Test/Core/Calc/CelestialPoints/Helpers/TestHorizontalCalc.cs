// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.Coordinates.Helpers;
using Enigma.Domain.Dtos;
using Enigma.Facades.Interfaces;
using Moq;

namespace Enigma.Test.Core.Calc.CelestialPoints.Helpers;

[TestFixture]
public class TestHorizontalCalc
{
    private const double DELTA = 0.00000001;
    private const double JULIAN_DAY = 123456.789;
    private readonly double[] _geoGraphicCoordinates = { 10.0, 50.0 };
    private readonly Location _location = new("Anywhere", 10.0, 50.0);
    private EquatorialCoordinates? _equCoordinates;
    private readonly double[] _eclipticValues = { 200.0, -2.0 };
    private readonly double[] _expectedHorCoord = { 222.2, 44.4 };
    private double[]? _actualResults;
    private const int FLAGS = 0;


    [SetUp]
    public void SetUp()
    {
        _equCoordinates = new EquatorialCoordinates(200.0, -2.0);
        PerformCalculation();
    }

    [Test]
    public void TestResultForAzimuth()
    {
        Assert.That(_actualResults![0], Is.EqualTo(_expectedHorCoord[0]).Within(DELTA));
    }

    [Test]
    public void TestResultForAltitude()
    {
        Assert.That(_actualResults![1], Is.EqualTo(_expectedHorCoord[1]).Within(DELTA));
    }


    private void PerformCalculation()
    {
        var mockFacade = new Mock<IAzAltFacade>();
        mockFacade.Setup(p => p.RetrieveHorizontalCoordinates(JULIAN_DAY, _geoGraphicCoordinates, _eclipticValues, FLAGS)).Returns(_expectedHorCoord);
        var horizontalCalc = new HorizontalCalc(mockFacade.Object);
        _actualResults = horizontalCalc.CalculateHorizontal(JULIAN_DAY, _location, _equCoordinates!, FLAGS);
    }



}