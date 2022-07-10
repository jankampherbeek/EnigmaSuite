// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Exceptions;
using System.Runtime.InteropServices;

namespace Enigma.Core.Calc.SeFacades;

/// <summary>
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
public class HousesFacade : IHousesFacade
{
    /// <inheritdoc/>
    /// <remarks>Throws a SwissEphException if the SE returns an error.</remarks>
    public double[][] RetrieveHouses(double jdUt, int flags, double geoLat, double geoLon, char houseSystem)
    {
           int _nrOfCusps = houseSystem == 'G' ? 37 : 13;
           double[] cusps = new double[_nrOfCusps];
           double[] mundanePoints = new double[10];

           int result = ext_swe_houses_ex(jdUt, flags, geoLat, geoLon, (int)houseSystem, cusps, mundanePoints);
           if (result < 0)
           {
               string paramsSummary = string.Format("jdUt: {0}, flags: {1}, geoLat: {2}, geoLon: {3}, houseSystem: {4}.", jdUt, flags, geoLat, geoLon, houseSystem);
               throw new SwissEphException(string.Format("{0}/{1}/{2}", result, "SePosHousesFacade.PosHousesFromSe", paramsSummary));
           }
   
        double[][] positions = { cusps, mundanePoints };
        return positions;

    }
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_houses_ex")]
    private extern static int ext_swe_houses_ex(double tjdut, int flags, double geolat, double geolon, int hsys, double[] hcusp0, double[] ascmc0);
}