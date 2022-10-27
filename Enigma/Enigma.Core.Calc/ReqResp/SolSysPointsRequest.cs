// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.CalcVars;
using Enigma.Domain.Locational;

namespace Enigma.Core.Calc.ReqResp;

/// <summary>
/// Data for a request to calculate one or more positions for a solar system point.
/// </summary>
public record SolSysPointsRequest
{
    public readonly double JulianDayUt;
    public readonly Location ChartLocation;
    public readonly CalculationPreferences ActualCalculationPreferences;

    /// <param name="julianDayUt">Julian day for universal time.</param>
    /// <param name="location">Location (only latitude and longitude are used).</param>
    /// <param name="calculationPreferences">User preferences for the calculation.</param>

    public SolSysPointsRequest(double julianDayUt, Location location, CalculationPreferences calculationPreferences)
    {
        JulianDayUt = julianDayUt;
        ChartLocation = location;
        ActualCalculationPreferences = calculationPreferences;
    }
}