// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc.ObliqueLongitude;
using Enigma.Core.Calc.ReqResp;

namespace Enigma.Core.Calc.Api.Astron;

public interface IObliqueLongitudeApi
{
    public ObliqueLongitudeResponse GetObliqueLongitude(ObliqueLongitudeRequest request);

}

public class ObliqueLongitudeApi : IObliqueLongitudeApi
{
    private readonly IObliqueLongitudeHandler _handler;

    public ObliqueLongitudeApi(IObliqueLongitudeHandler handler)
    {
        _handler = handler;
    }

    public ObliqueLongitudeResponse GetObliqueLongitude(ObliqueLongitudeRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.NullOrEmpty(request.SolSysPointCoordinates);
        return _handler.CalcObliqueLongitude(request);
    }
}