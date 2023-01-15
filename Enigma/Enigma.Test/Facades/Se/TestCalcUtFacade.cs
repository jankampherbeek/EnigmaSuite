// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Facades.Interfaces;
using Enigma.Facades.Se;

namespace Enigma.Test.Facades.Se;


[TestFixture]
public class TestCalcUtFacade
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestPositionFromSe()
    {
        double expectedLongitudeMoon = 121 + 45.0 / 60.0 + 39.0 / 3600.0;        // voor deze tijd 1g45m38s via Enigma
        string path = @"c:\\enigma_ar\se";
        SeInitializer.SetEphePath(path);
        double julianDay = 2434406.81711 + 0.00034953704;
        int idMoon = 1;
        int flags = 2; 
        ICalcUtFacade facade = new CalcUtFacade();
        double[] positions = facade.PositionFromSe(julianDay, idMoon, flags);  // positions: Array with 6 positions, subsequently: longitude, latitude, distance, longitude speed, latitude speed and distance speed.
        Assert.That(expectedLongitudeMoon, Is.EqualTo(positions[0]).Within(_delta));

  /*  Expected: 121.7617493612468d +/ -1E-08.0d
  But was:  121.76083333333334d
  Off by:   0.00091602791346190315d
   */
        
        }
}
