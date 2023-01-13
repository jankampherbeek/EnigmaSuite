// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Handlers.Interfaces;

using Enigma.Domain.Calc.Specials;

/// <summary>Handler for the calculation of obliquity of the earths axis.</summary>
public interface IObliquityHandler
{
    /// <summary>Start the calculation.</summary>
    /// <param name="obliquityRequest"></param>
    /// <returns></returns>
    public double CalcObliquity(ObliquityRequest obliquityRequest);
}

/// <summary>Calculations for obliquity of the earths axis.</summary>
public interface IObliquityCalc
{
    /// <summary>Calculate mean or true obliquity.</summary>
    /// <param name="julianDayUt">Julian Day for UT.</param>
    /// <param name="useTrueObliquity">True for true obliquity, false for mean obliquity.</param>
    /// <returns>The calculated obliquity.</returns>
    public double CalculateObliquity(double julianDayUt, bool useTrueObliquity);
}