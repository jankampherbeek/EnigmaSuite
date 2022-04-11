// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using api.handlers;
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
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IFlagDefinitions _flagDefs;


    public MundanePosHandler(IMundanePositionsCalculator mundPosCalc, IObliquityHandler obliquityHandler, IFlagDefinitions flagDefs)
    {
        _mundPosCalc = mundPosCalc;
        _obliquityHandler = obliquityHandler;
        _flagDefs = flagDefs;
    }

    /// <inheritdoc/>
    public FullMundanePosResponse CalculateAllMundanePositions(FullMundanePosRequest request)
    {
        FullMundanePositions? positions = null;
        string errorText = "";
        bool success = true;
        try
        {
            var obliquityRequest = new ObliquityRequest(request.JdUt, true);
            ObliquityResponse obliquityResponse = _obliquityHandler.CalcObliquity(obliquityRequest);
            double obliquity = obliquityResponse.Obliquity;
            success = obliquityResponse.Success;
            errorText = obliquityResponse.ErrorText;
            int flags = _flagDefs.DefineFlags(request);
            positions = _mundPosCalc.CalculateAllMundanePositions(request.JdUt, obliquity, flags, request.ChartLocation, request.HouseSystem);
        } 
        catch (SwissEphException see)
        {
            errorText += see.Message;
            success = false;
        }
        return new FullMundanePosResponse(positions, success, errorText);
    }




}
