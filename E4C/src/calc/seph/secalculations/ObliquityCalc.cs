// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using domain.shared;
using E4C.calc.seph.sefacade;
using E4C.Models.Domain;

namespace E4C.calc.seph.secalculations;

/// <summary>
/// Calculations for obliquity of the earths axis.
/// </summary>
public interface IObliquityCalc
{
    /// <summary>
    /// Calculate mean or true obliquity.
    /// </summary>
    /// <param name="julianDayUt">Julian Day for UT.</param>
    /// <param name="useTrueObliquity">True for true obliquity, false for mean obliquity.</param>
    /// <returns>The calculated obliquity.</returns>
    public double CalculateObliquity(double julianDayUt, bool useTrueObliquity);
}

/// <inheritdoc/>
public class ObliquityCalc : IObliquityCalc
{

    private readonly ISePosCelPointFacade _posCelPointFacade;

    /// <param name="celPointFacade">Facade for the calculation of celestial points.</param>
    /// <remarks>For the calculation by the SE, the obliquity is a special kind of celestial point, therefore the facade for cel points is used.</remarks>
    public ObliquityCalc(ISePosCelPointFacade celPointFacade) => _posCelPointFacade = celPointFacade;

    /// <inheritdoc/>
    public double CalculateObliquity(double julianDayUt, bool useTrueObliquity)
    {
        int flags = 0;
        double[] positions = _posCelPointFacade.PosCelPointFromSe(julianDayUt, Constants.SE_ECL_NUT, flags);
        double resultingPosition = useTrueObliquity ? positions[1] : positions[0];
        return resultingPosition;
    }
}