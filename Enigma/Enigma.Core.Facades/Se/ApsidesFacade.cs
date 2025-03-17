// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using Enigma.Domain.References;

namespace Enigma.Facades.Se;

/// <summary>Facade for apsides</summary>
public interface ICalcApsidesFacade
{
    /// <summary>Calculate apsides for a given celestial body</summary>
    /// <param name="julianDay">Julian day for ET</param>
    /// <param name="seCelPointId">Id of the celestial body</param>
    /// <param name="method">Method to use, either mean or oscillating</param>
    /// <param name="flags">Combined value for flags</param>
    /// <returns>2 array with 6 positions. The first array contains values for the perihelion, the second array for the
    /// aphelion. The values in each array are: main position, deviation, distance, main speed, deviation speed,
    /// distance speed.</returns>
    public double[][] ApsidesFromSe(double julianDay, int seCelPointId, ApsidesMethods method, int flags);
}

/// <inheritdoc/>
[SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments")]
public sealed class CalcApsidesFacade: ICalcApsidesFacade 
{
    public double[][] ApsidesFromSe(double julianDay, int seCelPointId, ApsidesMethods method, int flags)
    {
        var methodId = 1;
        if (method == ApsidesMethods.oscillating) methodId = 2;
        StringBuilder resultValue = new(256);
        var ascNodePositions = new double[6];
        var descNodePositions = new double[6];
        var perihPositions = new double[6];
        var aphPositions = new double[6];
        //         _ = ext_swe_calc_ut(julianDay, seCelPointId, flags, positions, resultValue);
        _ = ext_swe_nod_aps_ut(julianDay, seCelPointId, flags, methodId, ascNodePositions, descNodePositions,
            perihPositions, aphPositions, resultValue);
        var result = new double[2][];
        result[0] = perihPositions;
        result[1] = aphPositions;
        return result;

    }


    /// <summary>Access dll to retrieve position for apside.</summary>
    /// <param name="tjd">Julian day for UT.</param>
    /// <param name="ipl">Identifier for the celestial point.</param>
    /// <param name="iflag">Combined values for flags.</param>
    /// <param name="method">Indicates if mean or oscillating position is expected</param>
    /// <param name="xxAscNod">Positions for ascending node, currently ignored.</param>
    /// <param name="xxDescNod">Positions for descending node, currently ignored.</param>
    /// <param name="xxPer">Positions for perihelium.</param>
    /// <param name="xxAph">Positions for aphelium.</param>
    /// <param name="serr">Error text, if any.</param>
    /// <returns>An indication if the calculation was successful.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_nod_aps_ut")]
    private static extern int ext_swe_nod_aps_ut(double tjd, int ipl, long iflag, int method, double[] xxAscNod, 
        double[] xxDescNod,  double[] xxPer, double[] xxAph, StringBuilder serr);
}
