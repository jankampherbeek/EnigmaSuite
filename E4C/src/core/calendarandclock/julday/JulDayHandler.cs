// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.Exceptions;
using E4C.Shared.ReqResp;

namespace E4C.Core.CalendarAndClock.JulDay;

public interface IJulDayHandler
{
    public JulianDayResponse CalcJulDay(JulianDayRequest request);
}


public class JulDayHandler : IJulDayHandler
{
    private readonly IJulDayCalc _julDayCalc;

    public JulDayHandler(IJulDayCalc julDayCalc) => _julDayCalc = julDayCalc;


    public JulianDayResponse CalcJulDay(JulianDayRequest request)
    {
        double julDay = 0.0;
        string errorText = "";
        bool success = true;
        try
        {
            julDay = _julDayCalc.CalcJulDay(request.DateTime);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new JulianDayResponse(julDay, success, errorText);
    }

}