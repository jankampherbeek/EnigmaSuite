// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.Specials;

/// <summary>Request to calculate obliquity.</summary>
/// <param name="JdUt">Julian day for UT.</param>
/// <param name="TrueObliquity">True for true obliquity, false for mean obliquity.</param>
public record ObliquityRequest(double JdUt, bool TrueObliquity);