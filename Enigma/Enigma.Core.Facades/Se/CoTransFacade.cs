// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Facades.Interfaces;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;


/// <inheritdoc/>
public class CoTransFacade : ICoTransFacade
{
    /// <inheritdoc/>
    /// <remarks>Throws SwissEphException if the CommonSE returns an error.</remarks>
    public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity)
    {
        double negativeObliquity = -Math.Abs(obliquity);
        double placeHolderForDistance = 1.0;
        double[] allEclipticCoordinates = new double[] { eclipticCoordinates[0], eclipticCoordinates[1], placeHolderForDistance };
        double[] equatorialResults = new double[3];
        ext_swe_cotrans(allEclipticCoordinates, equatorialResults, negativeObliquity);
        return equatorialResults;
    }

    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_cotrans")]
    private extern static void ext_swe_cotrans(double[] allEclipticCoordinates, double[] equatorialResults, double negativeObliquity);
}