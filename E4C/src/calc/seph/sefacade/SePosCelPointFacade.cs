// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.exceptions;
using System.Runtime.InteropServices;

namespace E4C.calc.seph.sefacade;


/// <summary>
/// Facade for the calculation of the positions of celestial points (planets, nodes etc.).
/// </summary> 
public interface ISePosCelPointFacade
{
    /// <summary>
    /// Retrieve positions for a celestial point.
    /// </summary>
    /// <param name="julianDay">Julian day calculated for UT.</param>
    /// <param name="seCelPointId">Identifier for the celestial point as used by the SE.</param>
    /// <param name="flags">Combined value for flags to define the desired calculation.</param>
    /// <returns>Array with 6 positions, subsequently: longitude, latitude, distance, longitude speed, latitude speed and distance speed.</returns>
    public double[] PosCelPointFromSe(double julianDay, int seCelPointId, int flags);
}

/// <inheritdoc/>
/// <remarks>Throws a SwissEphException if the SE returns an error.</remarks>
public class SePosCelPointFacade : ISePosCelPointFacade
{
    /// <inheritdoc/>
    public double[] PosCelPointFromSe(double julianDay, int seCelPointId, int flags)
    {
        string _resultValue = "";
        var _positions = new double[6];

        int result = ext_swe_calc_ut(julianDay, seCelPointId, flags, _positions, _resultValue);
        if (result < 0)
        {
            string paramsSummary = string.Format("julianDay: {0}, seCelPointId: {1}, flags: {2}.");
            throw new SwissEphException(string.Format("{0}/{1}/{2}", result, "SePosCelPointFacade.PosCelPointFromSe", paramsSummary));
        }
        return _positions;
    }

    /// <summary>
    /// Access dll to retrieve position for celestial point.
    /// </summary>
    /// <param name="tjd">Julian day for UT.</param>
    /// <param name="ipl">Identifier for the celestial point.</param>
    /// <param name="iflag">Combined values for flags.</param>
    /// <param name="xx">The resulting positions.</param>
    /// <param name="serr">Error text, if any.</param>
    /// <returns>An indication if the calculation was succesfull.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_calc_ut")]
    private extern static int ext_swe_calc_ut(double tjd, int ipl, long iflag, double[] xx, string serr);
}