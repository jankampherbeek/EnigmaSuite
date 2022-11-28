// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Calc.CelPoints.ObliqueLongitude;

public class ObliqueLongitudeHandler : IObliqueLongitudeHandler
{
    private readonly IObliqueLongitudeCalculator _calculator;


    public ObliqueLongitudeHandler(IObliqueLongitudeCalculator calculator)
    {
        _calculator = calculator;
    }


    ObliqueLongitudeResponse IObliqueLongitudeHandler.CalcObliqueLongitude(ObliqueLongitudeRequest request)
    {
        List<NamedEclipticLongitude>? longitudes = null;
        bool success = true;
        string errorTxt = "";
        try
        {
            longitudes = _calculator.CalcObliqueLongitudes(request);
        }
        catch (Exception ex)
        {
            success = false;
            errorTxt = "Error in ObliqueLongitudeHandler" + ex.Message;
        }
        return new ObliqueLongitudeResponse(longitudes, success, errorTxt);
    }
}