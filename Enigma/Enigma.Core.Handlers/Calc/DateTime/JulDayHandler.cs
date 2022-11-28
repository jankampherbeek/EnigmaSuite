// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.Exceptions;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Calc.DateTime;


public class JulDayHandler : IJulDayHandler
{
    private readonly IJulDayCalc _julDayCalc;

    public JulDayHandler(IJulDayCalc julDayCalc) => _julDayCalc = julDayCalc;


    public JulianDayResponse CalcJulDay(JulianDayRequest request)
    {
        double julDayUt = 0.0;
        double julDayEt = 0.0;
        double deltaT = 0.0;
        string errorText = "";
        bool success = true;
        try
        {
            julDayUt = _julDayCalc.CalcJulDayUt(request.DateTime);
            deltaT = _julDayCalc.CalcDeltaT(julDayUt);
            julDayEt = julDayUt + deltaT;
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new JulianDayResponse(julDayUt, julDayEt, deltaT, success, errorText);
    }

}