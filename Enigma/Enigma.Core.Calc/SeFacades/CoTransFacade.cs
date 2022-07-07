// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Exceptions;
using System.Runtime.InteropServices;

namespace Enigma.Core.Calc.SeFacades;


/// <summary>Facade for the conversion between ecliptic and equatorial coordinates.</summary>
/// <remarks>Enables accessing the SE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface ICoTransFacade
{
    /// <summary>Convert ecliptic to equatorial coordinates.</summary>
    /// <remarks>Calls the function ext_swe_cotrans from the SE.</remarks>/// 
    /// <param name="eclipticCoordinates">Array with subsequently longitude and latitude.</param>
    /// <param name="obliquity"/>
    /// <returns>Array with subsequently right ascension and declination.</returns>
    public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity);
}


/// <inheritdoc/>
public class CoTransFacade : ICoTransFacade
{
    /// <inheritdoc/>
    /// <remarks>Throws SwissEphException if the SE returns an error.</remarks>
    public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity)
    {
              double negativeObliquity = -(Math.Abs(obliquity));
              double placeHolderForDistance = 1.0;
              double[] allEclipticCoordinates = new double[] { eclipticCoordinates[0], eclipticCoordinates[1], placeHolderForDistance };
              double[] equatorialResults = new double[3];
              int result = ext_swe_cotrans(allEclipticCoordinates, equatorialResults, negativeObliquity);
              if (result < 0)
              {
                  string eclCoordinatesText = eclipticCoordinates.Length == 3 ? string.Format("eclipticCoordinates: {0}, {1}, {2}", eclipticCoordinates[0], eclipticCoordinates[1], eclipticCoordinates[2]) :
                      string.Format("Number of eclipticCoordinates is {0}.", eclipticCoordinates.Length);
                  string paramsSummary = string.Format("{0}, obliquity : {1}.", eclCoordinatesText, obliquity);
                  throw new SwissEphException(string.Format("{0}/{1}/{2}", result, "CoordinateConversionFacade.EclipticToEquatorial", paramsSummary));
              }
              return equatorialResults;
    }

    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_cotrans")]
    private extern static int ext_swe_cotrans(double[] allEclipticCoordinates, double[] equatorialResults, double negativeObliquity);
}