// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Domain.Requests;

/// <summary>Request to calculate a complete set of fully defined mundane positions./// </summary>
/// <param name="JdUt">Julian Day for UT.</param>
/// <param name="ChartLocation">The location with the correct coordinates.</param>
/// <param name="CalcPrefs">Calculation preferences.</param>
public record FullHousesPosRequest(double JdUt, Location? ChartLocation, CalculationPreferences CalcPrefs);
