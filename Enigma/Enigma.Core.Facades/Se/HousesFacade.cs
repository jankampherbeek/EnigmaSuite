// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using Enigma.Domain.Exceptions;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;

/// Facade for the calculation of mundane points (housecusps, vertex etc.).
/// </summary>
public interface IHousesFacade
{
    /// <summary>
    /// Retrieve positions for house cusps and other mundane points.
    /// </summary>
    /// <param name="jdUt">Julian Day for UT.</param>
    /// <param name="flags">0 for tropical, 0 or SEFLG_SIDEREAL for sidereal (logical or).</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <param name="geoLon">Geographic longitude.</param>
    /// <param name="houseSystem">Indication for the house system within the Swiss Ephemeris.</param>
    /// <returns>A two dimensional array. The first array contains the cusps, starting from position 1 (position 0 is empty) and ordered by number. 
    /// The length is 13 (for systems with 12 cusps) or 37 (for Gauquelin houses (houseSystem 'G'), which have 36 cusps).
    /// The second array contains 10 positions with the following content:
    /// 0: = Ascendant, 1: MC, 2: ARMC, 3: Vertex, 4: equatorial ascendant( East point), 5: co-ascendant (Koch), 6: co-ascendant (Munkasey), 7: polar ascendant (Munkasey). 
    /// Positions 8 and 9 are empty.
    /// </returns>
    public double[][] RetrieveHouses(double jdUt, int flags, double geoLat, double geoLon, char houseSystem);
}

/// <inheritdoc/>
[SuppressMessage("Interoperability", "SYSLIB1054:Use \'LibraryImportAttribute\' instead of \'DllImportAttribute\' to generate P/Invoke marshalling code at compile time")]
public class HousesFacade : IHousesFacade
{
    /// <inheritdoc/>
    /// <remarks>Throws a SwissEphException if the CommonSE returns an error.</remarks>
    public double[][] RetrieveHouses(double jdUt, int flags, double geoLat, double geoLon, char houseSystem)
    {
        int nrOfCusps = houseSystem == 'G' ? 37 : 13;
        double[] cusps = new double[nrOfCusps];
        double[] mundanePoints = new double[10];

        int result = ext_swe_houses_ex(jdUt, flags, geoLat, geoLon, houseSystem, cusps, mundanePoints);
        if (result < 0)
        {
            string paramsSummary =
                $"jdUt: {jdUt}, flags: {flags}, geoLat: {geoLat}, geoLon: {geoLon}, houseSystem: {houseSystem}.";
            throw new SwissEphException($"{result}/SePosHousesFacade.PosHousesFromSe/{paramsSummary}");
        }
        double[][] positions = { cusps, mundanePoints };
        return positions;
    }
    
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_houses_ex")]
    private static extern int ext_swe_houses_ex(double tjdut, int flags, double geolat, double geolon, int hsys, double[] hcusp0, double[] ascmc0);
}