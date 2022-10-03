// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.ReqResp;

namespace Enigma.Core.Calc.ReqResp;

public record JulianDayResponse : ValidatedResponse
{
    public double JulDayUt { get; }
    public double JulDayEt { get; }
    public double DeltaT { get; }


    public JulianDayResponse(double julDayUt, double julDayEt, double deltaT, bool success, string errorText) : base(success, errorText)
    {
        JulDayUt = julDayUt;
        JulDayEt = julDayEt;
        DeltaT = deltaT;
    }

}