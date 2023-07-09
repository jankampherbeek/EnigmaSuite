// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.Constants;

namespace Enigma.Core.Handlers.Calc.Coordinates;


public sealed class HorizontalHandler : IHorizontalHandler
{
    private readonly IHorizontalCalc _horizontalCalc;

    public HorizontalHandler(IHorizontalCalc horizontalCalc) => _horizontalCalc = horizontalCalc;

    public HorizontalCoordinates CalcHorizontal(HorizontalRequest request)
    {
        int flags = EnigmaConstants.SeflgEquatorial;    // flags for horizontal coordinates, only equatorial is used.
        double[] azimuthAltitude = _horizontalCalc.CalculateHorizontal(request.JdUt, request.Location, request.EquCoordinates, flags);
        return new HorizontalCoordinates(azimuthAltitude[0], azimuthAltitude[1]);
    }

}