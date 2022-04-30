// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Shared.Domain;
using E4C.Core.Util;
using E4C.Shared.ReqResp;
using System;
using System.Collections.Generic;

namespace E4C.Core.Astron.ObliqueLongitude;


public interface IObliqueLongitudeHandler
{
    public ObliqueLongitudeResponse CalcObliqueLongitude(ObliqueLongitudeRequest request);
}

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