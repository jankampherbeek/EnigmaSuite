// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.AstronCalculations;

/// <summary>Different methods to calculate a celestial point.</summary>
/// <remarks>
/// SE: Calculation via the Swiss Ephemeris (for celestial points and for houses).
/// Elements: Calculation based on ecliptical elements (for celestial points only).
/// Formula: Calculation based on a formula (for celestial points and for houses). 
/// </remarks>
public enum CalculationTypes
{
    SE = 0,
    Elements = 1,
    Numeric = 2
}

