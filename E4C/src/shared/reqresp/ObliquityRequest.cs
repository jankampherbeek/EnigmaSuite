// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.Shared.ReqResp;

/// <summary>Request to calculate obliquity.</summary>
public record ObliquityRequest
{
    public double JdUt { get; }
    public bool UseCalculationForTrue { get; }


    public ObliquityRequest(double jdUt, bool useCalculationForTrue)
    {
        JdUt = jdUt;
        UseCalculationForTrue = useCalculationForTrue;
    }

}