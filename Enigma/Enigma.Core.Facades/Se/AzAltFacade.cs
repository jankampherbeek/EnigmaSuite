// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Enigma.Domain.Exceptions;
using Enigma.Facades.Interfaces;
using Serilog;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;

/// <inheritdoc/>
[SuppressMessage("Interoperability", "SYSLIB1054:Use \'LibraryImportAttribute\' instead of \'DllImportAttribute\' to generate P/Invoke marshalling code at compile time")]
public class AzAltFacade : IAzAltFacade
{
    /// <inheritdoc/>
    /// <remarks>Throws SwissEphException if CommonSE returns an error.</remarks>
    public double[] RetrieveHorizontalCoordinates(double julianDayUt, double[] geoGraphicCoordinates, double[] equCoordinates, int flags)
    {
        double[] horizontalCoordinates = new double[3];  // at index 2 the apparent altitude is given, which is ignored.
        int result = ext_swe_azalt(julianDayUt, flags, geoGraphicCoordinates, 0, 0, equCoordinates, horizontalCoordinates);

        if (result >= 0) return new[] { horizontalCoordinates[0], horizontalCoordinates[1] };
        string geoCoordinatesText = $"Number of geographicCoordinates is {geoGraphicCoordinates.Length:D}.";
        if (geoGraphicCoordinates.Length > 1)
        {
            geoCoordinatesText = $"geoGraphicCoordinates: {geoGraphicCoordinates[0]}, {geoGraphicCoordinates[1]}";
        }
        string equCoordinatesOk = $"equCoordinates: {equCoordinates[0]}, {equCoordinates[1]}";
        string equCoordinatesWrong = $"Number of equCoordinates is {equCoordinates.Length}.";
        string equCoordinatesText = equCoordinates.Length > 1 ? equCoordinatesOk : equCoordinatesWrong;
        string jdText = julianDayUt.ToString(CultureInfo.InvariantCulture);
        string flagText = flags.ToString();
        Log.Error("Exception thrown: AzAltFacade.RetrieveHorizontalCoordinates: jd : {JdText} {GeoCoordinatesText} {EquCoordinatesText} Flags: {FlagText} Returncode {Result}", 
            jdText, geoCoordinatesText, equCoordinatesText, flagText, result);
        throw new SwissEphException("Error in AzAltFacade.RetrieveHorizontalCoordinates");
    }

    /// <summary>
    /// Access dll to retrieve horizontal positions.
    /// </summary>
    /// <param name="tjd">Julian day for UT.</param>
    /// <param name="iflag">Flag: always SE_EQU2HOR = 1.</param>
    /// <param name="geoCoordinates">Geographic longitude, altitude and height above sea (ignored for real altitude).</param>
    /// <param name="atPress">Atmospheric pressure in mbar, ignored for true altitude.</param>
    /// <param name="atTemp">Atmospheric temperature in degrees Celsius, ignored for true altitude.</param>
    /// <param name="equatorialCoordinates">Right ascension, declinationa and distance.</param>
    /// <param name="horizontalCoordinates">Resulting values for azimuth, true altitude and apparent altitude.</param>
    /// <returns>An indication if the calculation was successful. Negative values indicate an error.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_azalt")]
    private static extern int ext_swe_azalt(double tjd, long iflag, double[] geoCoordinates, double atPress, double atTemp, double[] equatorialCoordinates, double[] horizontalCoordinates);
}
