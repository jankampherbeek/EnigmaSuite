// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
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
    public double[] CalculateHorizontal(double jdUt, Location location, EclipticCoordinates eclipticCoordinates, int flags)
    {
        var geoGraphicLonLat = new double[] { location.GeoLong, location.GeoLat };
        var eclipticLonLat = new double[] { eclipticCoordinates.Longitude, eclipticCoordinates.Latitude };
        return _azAltFacade.RetrieveHorizontalCoordinates(jdUt, geoGraphicLonLat, eclipticLonLat, flags);
    }
}
