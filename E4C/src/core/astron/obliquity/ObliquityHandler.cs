// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.Exceptions;
using E4C.Shared.ReqResp;

namespace E4C.Core.Astron.Obliquity;

public interface IObliquityHandler
{
    ObliquityResponse CalcObliquity(ObliquityRequest obliquityRequest);
}


public class ObliquityHandler : IObliquityHandler
{
    private readonly IObliquityCalc _obliquityCalc;

    public ObliquityHandler(IObliquityCalc obliquityCalc)
    {
        _obliquityCalc = obliquityCalc;
    }

    public ObliquityResponse CalcObliquity(ObliquityRequest obliquityRequest)
    {
        double obliquity = 0.0;
        string errorText = "";
        bool success = true;
        try
        {
            obliquity = _obliquityCalc.CalculateObliquity(obliquityRequest.JdUt, obliquityRequest.UseCalculationForTrue);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new ObliquityResponse(obliquity, success, errorText);
    }
}