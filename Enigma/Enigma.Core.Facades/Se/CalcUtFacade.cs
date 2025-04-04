﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

namespace Enigma.Facades.Se;


/// <summary>Facade for the calculation of the positions of celestial points (planets, nodes etc.).</summary> 
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface ICalcUtFacade
{
    /// <summary>Retrieve positions for a celestial point.</summary>
    /// <remarks>Calls the function ext_swe_calc_ut from the CommonSE.</remarks>
    /// <param name="julianDay">Julian day calculated for UT.</param>
    /// <param name="seCelPointId">Identifier for the celestial point as used by the CommonSE.</param>
    /// <param name="flags">Combined value for flags to define the desired calculation.</param>
    /// <returns>Array with 6 positions, subsequently: longitude, latitude, distance, longitude speed, latitude speed and distance speed.</returns>
    public double[] PositionFromSe(double julianDay, int seCelPointId, int flags);
}


/// <inheritdoc/>
/// <remarks>Throws a SwissEphException if the CommonSE returns an error.</remarks>
[SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments")]
public sealed class CalcUtFacade : ICalcUtFacade
{
    /// <inheritdoc/>
    public double[] PositionFromSe(double julianDay, int seCelPointId, int flags)
    {
        StringBuilder resultValue = new(256);
        double[] positions = new double[6];
        _ = ext_swe_calc_ut(julianDay, seCelPointId, flags, positions, resultValue);
        return positions;
    }

    /// <summary>Access dll to retrieve position for celestial point.</summary>
    /// <param name="tjd">Julian day for UT.</param>
    /// <param name="ipl">Identifier for the celestial point.</param>
    /// <param name="iflag">Combined values for flags.</param>
    /// <param name="xx">The resulting positions.</param>
    /// <param name="serr">Error text, if any.</param>
    /// <returns>An indication if the calculation was successful.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_calc_ut")]
    private static extern int ext_swe_calc_ut(double tjd, int ipl, long iflag, double[] xx, StringBuilder serr);
}