// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Domain.RequestResponse;

/// <summary>Data for a request to calculate transits.</summary>
/// <param name="JulianDayUt">Julian day for universal time.</param>
/// <param name="Location">Location (only latitude and longitude are used).</param>
/// <param name="CalculationPreferences">User preferences for the calculation.</param> 
public record TransitsEventRequest(double JulianDayUt, Location Location,
    CalculationPreferences CalculationPreferences);

