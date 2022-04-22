// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.CoordinateConversion;
using E4C.Core.Facades;
using E4C.Core.Shared.Domain;
using Moq;
using NUnit.Framework;


namespace E4CTest.core.astron.coordinateconversion;

[TestFixture]
public class TestCoordinateConversionCalc
{
    private readonly double _longitude = 100.0;
    private readonly double _latitude = -1.0;
    private readonly double _expectedRightAsc = 99.0;
    private readonly double _expectedDeclination = 2.2;
    private readonly double _obliquity = 23.447;
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestConversion()
    {
        var ecliptical = new double[] { _longitude, _latitude };
        var eclCoord = new EclipticCoordinates(_longitude, _latitude);
        var equatorial = new double[] { _expectedRightAsc, _expectedDeclination };
        Mock<ICoTransFacade> mockFacade = CreateFacadeMock(ecliptical, equatorial);
        ICoordinateConversionCalc convCalc = new CoordinateConversionCalc(mockFacade.Object);
        EquatorialCoordinates equatCoord = convCalc.PerformConversion(eclCoord, _obliquity);
        Assert.AreEqual(_expectedRightAsc, equatCoord.RightAscension, _delta);
        Assert.AreEqual(_expectedDeclination, equatCoord.Declination, _delta);
    }
    
    
    private Mock<ICoTransFacade> CreateFacadeMock(double[] ecliptical, double[] equatorial)
    { 
        var mock = new Mock<ICoTransFacade>();
        mock.Setup(p => p.EclipticToEquatorial(ecliptical, _obliquity)).Returns(equatorial);
        return mock;
    }
    
 




}
