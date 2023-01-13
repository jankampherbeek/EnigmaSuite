// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc;

/// <summary>Different methods to calculate a celestial point.</summary>
/// <remarks>
/// CelPointSE: Calculation of celestial points via the Swiss Ephemeris.
/// CelPointElements: Calculation of celestial points based on ecliptical elements.
/// CelPointFormula: Calculation of celestial points based on a formula. 
/// Mundane: Calculation of mundane points, either via the Swiss Ephemeris of based on a formula.
/// Specific: Calculation of points that requiere a specific approach (Arabic points etc.).
/// </remarks>
public enum CalculationTypes
{
    CelPointSE = 0,
    CelPointElements = 1,
    CelPointFormula = 2,
    Mundane = 3,
    Specific = 4
}

