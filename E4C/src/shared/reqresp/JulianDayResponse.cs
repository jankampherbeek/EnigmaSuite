// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace E4C.Shared.ReqResp;

public record JulianDayResponse : ValidatedResponse
{
    public double JulDay{ get; }

    public JulianDayResponse(double julDay, bool success, string errorText) : base(success, errorText)
    {
        JulDay = julDay;

    }

}