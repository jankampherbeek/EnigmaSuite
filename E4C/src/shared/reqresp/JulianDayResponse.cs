// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.reqresp;

namespace E4C.shared.reqresp;

public record JulianDayResponse : ValidatedResponse
{
    public double JulDay{ get; }

    public JulianDayResponse(double julDay, bool success, string errorText) : base(success, errorText)
    {
        JulDay = julDay;

    }

}