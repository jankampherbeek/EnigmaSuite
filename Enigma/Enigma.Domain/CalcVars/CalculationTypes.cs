// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.CalcVars;

/// <summary>Different methods to calculate a solar system point.</summary>
/// <remarks>
/// SE: Calculation via the Swiss Ephemeris.
/// Elements: Calculation based on ecliptical elements.
/// Numeric: Calculation based on simple numerics, mostly addition of a constant value. 
/// </remarks>
public enum CalculationTypes
{
    SE, Elements, Numeric
}

