// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Exceptions;

namespace Enigma.Core.Calc.Obliquity;

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
        double obliquityMean = 0.0;
        double obliquityTrue = 0.0;
        string errorText = "";
        bool success = true;
        try
        {
            obliquityMean = _obliquityCalc.CalculateObliquity(obliquityRequest.JdUt, false);
            obliquityTrue = _obliquityCalc.CalculateObliquity(obliquityRequest.JdUt, true);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new ObliquityResponse(obliquityMean, obliquityTrue, success, errorText);
    }
}