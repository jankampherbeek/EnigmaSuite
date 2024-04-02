// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Calculations for obliquity of the earths axis.</summary>
public interface IObliquityCalc
{
    /// <summary>Calculate mean or true obliquity.</summary>
    /// <param name="julianDayUt">Julian Day for UT.</param>
    /// <param name="useTrueObliquity">True for true obliquity, false for mean obliquity.</param>
    /// <returns>The calculated obliquity.</returns>
    public double CalculateObliquity(double julianDayUt, bool useTrueObliquity);
}

/// <inheritdoc/>
public sealed class ObliquityCalc : IObliquityCalc
{
    private readonly ICalcUtFacade _calcUtFacade;

    /// <param name="calcUtFacade">Facade for the calculation of celestial points by the CommonSE.</param>
    /// <remarks>For the calculation by the CommonSE, the obliquity is a special kind of celestial point, therefore the facade for cel points is used.</remarks>
    public ObliquityCalc(ICalcUtFacade calcUtFacade) => _calcUtFacade = calcUtFacade;

    /// <inheritdoc/>
    public double CalculateObliquity(double julianDayUt, bool useTrueObliquity)
    {
        const int flags = 0;
        double[] positions = _calcUtFacade.PositionFromSe(julianDayUt, EnigmaConstants.SE_ECL_NUT, flags);
        return useTrueObliquity ? positions[0] : positions[1];
    }
}