// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Facades.Interfaces;
using System.Runtime.InteropServices;
using System.Text;

namespace Enigma.Facades.Se;



/// <inheritdoc/>
/// <remarks>Throws a SwissEphException if the SE returns an error.</remarks>
public class CalcUtFacade : ICalcUtFacade
{
    /// <inheritdoc/>
    public double[] PosCelPointFromSe(double julianDay, int seCelPointId, int flags)
    {
        StringBuilder _resultValue = new(256);
        double[] _positions = new double[6];
        _ = ext_swe_calc_ut(julianDay, seCelPointId, flags, _positions, _resultValue);
        return _positions;
    }

    /// <summary>Access dll to retrieve position for celestial point.</summary>
    /// <param name="tjd">Julian day for UT.</param>
    /// <param name="ipl">Identifier for the celestial point.</param>
    /// <param name="iflag">Combined values for flags.</param>
    /// <param name="xx">The resulting positions.</param>
    /// <param name="serr">Error text, if any.</param>
    /// <returns>An indication if the calculation was succesfull.</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_calc_ut")]
    private extern static int ext_swe_calc_ut(double tjd, int ipl, long iflag, double[] xx, StringBuilder serr);
}