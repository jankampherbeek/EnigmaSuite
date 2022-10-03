// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Calc.ReqResp;

/// <summary>Request to calculate obliquity.</summary>
public record ObliquityRequest
{
    public double JdUt { get; }


    public ObliquityRequest(double jdUt)
    {
        JdUt = jdUt;
    }

}