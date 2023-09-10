// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Calc.Specials.Helpers;

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
        return useTrueObliquity ? positions[1] : positions[0];
    }
}