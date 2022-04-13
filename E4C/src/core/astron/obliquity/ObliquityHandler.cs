// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.exceptions;
using E4C.shared.reqresp;

namespace E4C.core.astron.obliquity;

public interface IObliquityHandler
{
    public ObliquityResponse CalcObliquity(ObliquityRequest request);
}


public class ObliquityHandler : IObliquityHandler
{
    private readonly IObliquityCalc _obliquityCalc;

    public ObliquityHandler(IObliquityCalc obliquityCalc)
    {
        _obliquityCalc = obliquityCalc;
    }

    public ObliquityResponse CalcObliquity(ObliquityRequest request)
    {
        double obliquity = 0.0;
        string errorText = "";
        bool success = true;
        try
        {
            obliquity = _obliquityCalc.CalculateObliquity(request.JdUt, request.UseCalculationForTrue);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new ObliquityResponse(obliquity, success, errorText);
    }
}