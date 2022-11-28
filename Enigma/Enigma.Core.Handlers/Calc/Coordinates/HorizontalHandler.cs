// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Exceptions;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Calc.Coordinates;


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