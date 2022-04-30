// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.Core.Astron.ObliqueLongitude;
using E4C.Shared.ReqResp;

namespace E4C.Core.Api.Astron;

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