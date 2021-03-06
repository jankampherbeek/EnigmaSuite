// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.ReqResp;

namespace Enigma.Core.Calc.ReqResp;

public record ObliquityResponse : ValidatedResponse
{
    public double ObliquityMean { get; }

    public double ObliquityTrue { get; }

    public ObliquityResponse(double obliquityMean, double obliquityTrue, bool success, string errorText) : base(success, errorText)
    {
        ObliquityMean = obliquityMean;
        ObliquityTrue = obliquityTrue;
    }

}