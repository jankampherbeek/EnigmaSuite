// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.facades;
using E4C.core.shared.domain;
using E4C.domain.shared.specifications;
using E4C.exceptions;
using E4C.shared.domain;
using E4C.shared.reqresp;

namespace E4C.core.astron.horizontal;

public interface IHorizontalHandler
{
    public HorizontalResponse CalcHorizontal(HorizontalRequest request);
}


public class HorizontalHandler : IHorizontalHandler
{
    private readonly IHorizontalCalc _horizontalCalc;

    public HorizontalHandler(IHorizontalCalc horizontalCalc) => _horizontalCalc = horizontalCalc;

    public HorizontalResponse CalcHorizontal(HorizontalRequest request)
    {
        string errorText = "";
        bool success = true;
        int flags = 0;    // flags for horizontal coordinates.
        var horCoord = new HorizontalCoordinates(0.0, 0.0); 
        try
        {
            double[] azimuthAltitude = _horizontalCalc.CalculateHorizontal(request.JdUt, request.ChartLocation, request.EclCoord, flags);
            horCoord = new HorizontalCoordinates(azimuthAltitude[0], azimuthAltitude[1]);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new HorizontalResponse(horCoord, success, errorText);
    }
}