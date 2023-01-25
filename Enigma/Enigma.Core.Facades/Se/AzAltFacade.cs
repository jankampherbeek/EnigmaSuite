// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Exceptions;
using Enigma.Facades.Interfaces;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;



/// <inheritdoc/>
public class AzAltFacade : IAzAltFacade
{
    /// <inheritdoc/>
    /// <remarks>Throws SwissEphException if CommonSE returns an error.</remarks>
    public double[] RetrieveHorizontalCoordinates(double julianDayUt, double[] geoGraphicCoordinates, double[] eclipticCoordinates, int flags)
    {

        double[] horizontalCoordinates = new double[3];  // at index 2 the apparent altitude is given, which is ignored.
        int result = ext_swe_azalt(julianDayUt, flags, geoGraphicCoordinates, 0, 0, eclipticCoordinates, horizontalCoordinates);
        if (result < 0)
        {
            string geoGraphCoordinatesText = geoGraphicCoordinates.Length == 3 ? string.Format("geoGraphicCoordinates: {0}, {1}, {2}", geoGraphicCoordinates[0], geoGraphicCoordinates[1], geoGraphicCoordinates[2]) :
                 string.Format("Number of geographicCoordinates is {0}.", geoGraphicCoordinates.Length);
            string eclCoordinatesText = eclipticCoordinates.Length == 3 ? string.Format("eclipticCoordinates: {0}, {1}, {2}", eclipticCoordinates[0], eclipticCoordinates[1], eclipticCoordinates[2]) :
                string.Format("Number of eclipticCoordinates is {0}.", eclipticCoordinates.Length);
            string paramsSummary = string.Format($"julianDayUt: {0}, geoGraphicCoordinates: {1}, eclipticCoordinates: {2}, flags: {3}.", julianDayUt, geoGraphCoordinatesText, eclCoordinatesText, flags);
            throw new SwissEphException(string.Format("{0}/{1}/{2}", result, "CalculateHorizontalCoordinatesFacade.CalculateHorizontalCoordinat", paramsSummary));
        }
        return new double[] { horizontalCoordinates[0], horizontalCoordinates[1] };

    }

    /// <summary>
    /// Access dll to retrieve horizontal positions.
    /// </summary>
    /// <param name="tjd">Julian day for UT.</param>
    /// <param name="iflag">Flag: always SE_ECL2HOR = 0.</param>
    /// <param name="geoCoordinates">Geographic longitude, altitude and height above sea (ignored for real altitude).</param>
    /// <param name="atPress">Atmospheric pressure in mbar, ignored for true altitude.</param>
    /// <param name="atTemp">Atmospheric temperature in degrees Celsius, ignored for true altitude.</param>
    /// <param name="eclipticCoordinates">Ecliptic longitude, latitude and distance.</param>
    /// <param name="horizontalCoordinates">Resulting values for azimuth, true altitude and apparent altitude.</param>
    /// <returns>An indication if the calculation was successful. Negative values indicate an error.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_azalt")]
    private extern static int ext_swe_azalt(double tjd, long iflag, double[] geoCoordinates, double atPress, double atTemp, double[] eclipticCoordinates, double[] horizontalCoordinates);
}
