// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.seph;
using E4C.calc.seph.secalculations;
using E4C.domain.shared.positions;
using E4C.domain.shared.reqresp;
using E4C.exceptions;

namespace E4C.api.handlers;

/// <summary>
/// Handles the calculation a full set of mundane positions for all relevant mundane points.
/// </summary>
public interface IMundanePosHandler
{
    /// <summary>
    /// Perform a calculation of all mundane positions.
    /// </summary>
    /// <param name="request"/>
    /// <returns>A valdiated response with teh results or an error indication.</returns>
    public FullMundanePosResponse CalculateAllMundanePositions(FullMundanePosRequest request);
}


/// <inheritdoc/>
public class MundanePosHandler : IMundanePosHandler
{
    private readonly IMundanePositionsCalculator _mundPosCalc;
    private readonly IObliquityNutationCalc _oblCalc;
    private readonly IFlagDefinitions _flagDefs;


    public MundanePosHandler(IMundanePositionsCalculator mundPosCalc, IObliquityNutationCalc oblCalc, IFlagDefinitions flagDefs)
    {
        _mundPosCalc = mundPosCalc;
        _oblCalc = oblCalc;
        _flagDefs = flagDefs;
    }

    /// <inheritdoc/>
    public FullMundanePosResponse CalculateAllMundanePositions(FullMundanePosRequest request)
    {
        FullMundanePositions? positions = null;
        try
        {
            double obliquity = _oblCalc.CalculateObliquity(request.JdUt, true);
            int flags = _flagDefs.DefineFlags(request);
            positions = _mundPosCalc.CalculateAllMundanePositions(request.JdUt, obliquity, flags, request.ChartLocation, request.HouseSystem);
            return new FullMundanePosResponse(positions, true, "");
        } 
        catch (SwissEphException see)
        {
            return new FullMundanePosResponse(positions, false, see.Message);
        }
        
    }




}
