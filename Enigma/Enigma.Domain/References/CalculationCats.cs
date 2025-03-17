// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.References;

/// <summary>Different methods to calculate a celestial point.</summary>
/// <remarks>
/// CommonSE: Calculation of celestial points via the Swiss Ephemeris.
/// CommonElements: Calculation of celestial points based on ecliptical elements.
/// CommonFormula: Calculation of celestial points based on a formula. 
/// Mundane: Calculation of mundane points, either via the Swiss Ephemeris of based on a formula.
/// Lots: Calculation of Hellenistic lots.
/// Apsides: Calculation off non-lunar apsides.
/// </remarks>
public enum CalculationCats
{
    CommonSe = 0,
    CommonElements = 1,
    CommonFormula = 2,
    Mundane = 3,
    Lots = 4,
    ZodiacFixed = 5,
    Apsides = 6
}

