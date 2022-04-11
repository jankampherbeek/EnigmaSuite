// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.seph.secalculations;
using E4C.domain.shared.reqresp;
using E4C.exceptions;

namespace api.handlers;

public interface IObliquityHandler
{
    public ObliquityResponse CalcObliquity(ObliquityRequest request);
}


public class ObliquityHandler : IObliquityHandler
{
    private readonly IObliquityCalc _obliquityNutationCalc;

    public ObliquityHandler(IObliquityCalc obliquityNutationCalc)
    {
        _obliquityNutationCalc = obliquityNutationCalc;
    }

    public ObliquityResponse CalcObliquity(ObliquityRequest request)
    {
        double obliquity = 0.0;
        string errorText = "";
        bool success = true;
        try
        {
            obliquity = _obliquityNutationCalc.CalculateObliquity(request.JdUt, request.UseCalculationForTrue);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new ObliquityResponse(obliquity, success, errorText);
    }
}