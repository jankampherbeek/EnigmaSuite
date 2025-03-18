// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Runtime.InteropServices;
using System.Text;
using Enigma.Domain.Dtos;

namespace Enigma.Facades.Se;

public interface ICalcOrbitalElementsFacade
{
    /// <summary>Calculate orbital elements.</summary>
    /// <param name="jd">Julian day number.</param>
    /// <param name="seId">Id of body in SE.</param>
    /// <param name="flags">Flags for calculation.</param>
    /// <returns>Record OrbitalElements.</returns>
    public OrbitalElements CalcOrbitalElements(double jd, int seId, int flags);
}

/// <inheritdoc/>
public class OrbitalElementsFacade: ICalcOrbitalElementsFacade
{
    /// <inheritdoc/>
    public OrbitalElements CalcOrbitalElements(double jd, int seId, int flags)
    {
        var orbElemValues = new double[17]; 
        StringBuilder resultValue = new(256);
        _ = ext_swe_get_orbital_elements(jd, seId, flags, orbElemValues, resultValue);
        return new OrbitalElements(orbElemValues);
    }
    
    /// <summary>Access dll to retrieve orbital elements.</summary>
    /// <param name="tjd">Julian day number</param>
    /// <param name="ipl">seId for chartpoint in SE</param>
    /// <param name="iflag">Flags for calculation</param>
    /// <param name="dret">Returned values for orbital elements</param>
    /// <param name="serr">Text if any error occurs</param>
    /// <returns>An indication if the calculation was successful</returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_get_orbital_elemements")]
    private static extern int ext_swe_get_orbital_elements(double tjd, int ipl, int iflag,  double[] dret, StringBuilder serr);
}