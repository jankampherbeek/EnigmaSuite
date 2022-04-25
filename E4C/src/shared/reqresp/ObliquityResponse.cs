// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace E4C.Shared.ReqResp;

public record ObliquityResponse : ValidatedResponse
{
    public double Obliquity { get; }

    public ObliquityResponse(double obliquity, bool success, string errorText) : base(success, errorText)
    {
        this.Obliquity = obliquity;

    }

}