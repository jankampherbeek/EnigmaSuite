// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Handlers.Calc.Coordinates.Helpers;


/// <inheritdoc/>
public sealed class HorizontalCalc : IHorizontalCalc
{
    private readonly IAzAltFacade _azAltFacade;

    public HorizontalCalc(IAzAltFacade azAltFacade) => _azAltFacade = azAltFacade;


    /// <inheritdoc/>
    public double[] CalculateHorizontal(double jdUt, Location location, EquatorialCoordinates equCoordinates, int flags)
    {
        var geoGraphicLonLat = new[] { location.GeoLong, location.GeoLat };
        var equatRaDecl = new[] { equCoordinates.RightAscension, equCoordinates.Declination };
        return _azAltFacade.RetrieveHorizontalCoordinates(jdUt, geoGraphicLonLat, equatRaDecl, flags);
    }
}
