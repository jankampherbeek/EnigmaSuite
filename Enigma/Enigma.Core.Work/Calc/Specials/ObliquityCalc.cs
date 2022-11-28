// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Work.Calc.Specials;

/// <inheritdoc/>
public class ObliquityCalc : IObliquityCalc
{
    private readonly ICalcUtFacade _posCelPointFacade;

    /// <param name="celPointFacade">Facade for the calculation of celestial points.</param>
    /// <remarks>For the calculation by the SE, the obliquity is a special kind of celestial point, therefore the facade for cel points is used.</remarks>
    public ObliquityCalc(ICalcUtFacade celPointFacade) => _posCelPointFacade = celPointFacade;

    /// <inheritdoc/>
    public double CalculateObliquity(double julianDayUt, bool useTrueObliquity)
    {
        int flags = 0;
        double[] positions = _posCelPointFacade.PosCelPointFromSe(julianDayUt, EnigmaConstants.SE_ECL_NUT, flags);
        double resultingPosition = useTrueObliquity ? positions[1] : positions[0];
        return resultingPosition;
    }
}