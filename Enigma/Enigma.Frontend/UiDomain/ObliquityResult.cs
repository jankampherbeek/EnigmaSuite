// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.UiDomain;
public class ObliquityResult
{
    public string ObliquityMeanText { get; }
    public string ObliquityTrueText { get; }
    public bool NoErrors { get; }
    public readonly string ErrorText = "- Error -";

    public ObliquityResult(ObliquityResponse response)
    {
        if (response.Success)
        {
            NoErrors = true;
            ObliquityMeanText = response.ObliquityMean.ToString().Replace(',', '.');
            ObliquityTrueText = response.ObliquityTrue.ToString().Replace(',', '.');
        }
        else
        {
            NoErrors = false;
            ObliquityMeanText = ErrorText;
            ObliquityTrueText = ErrorText;
        }

    }
}