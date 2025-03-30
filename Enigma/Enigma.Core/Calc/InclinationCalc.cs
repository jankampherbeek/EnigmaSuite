// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Handles the calculation of inclinations.</summary>
public interface IInclinationCalc
{
    /// <summary>Calculates the inclination.</summary>
    /// <param name="point">The ChartPoint for which the inclination is calculated.</param>
    /// <param name="jdUt">Julian Day for UT.</param>
    /// <param name="flags">Flags for calculation.</param>
    /// <returns>Value of inclination</returns>
    public double CalcInclination(ChartPoints point, double jdUt, int flags);
}

public class InclinationCalc(IOrbitalElementsFacade orbElemFacade) : IInclinationCalc
{
    private IOrbitalElementsFacade _orbElemFacade = orbElemFacade;

    public double CalcInclination(ChartPoints point, double jdUt, int flags)
    {
        var seId = point.GetDetails().CalcId;
        var inclination = _orbElemFacade.CalcOrbitalElements(jdUt, seId, flags).Inclination;
        return inclination;
    }
} 


