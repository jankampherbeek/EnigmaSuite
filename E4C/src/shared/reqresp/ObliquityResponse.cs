// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.reqresp;

namespace E4C.shared.reqresp;

public record ObliquityResponse : ValidatedResponse
{
    public double Obliquity { get; }

    public ObliquityResponse(double obliquity, bool success, string errorText): base(success, errorText)
    {
        this.Obliquity = obliquity;
        
    }

}