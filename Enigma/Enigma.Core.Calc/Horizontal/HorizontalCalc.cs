// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.Locational;
using Enigma.Domain.Positional;

namespace Enigma4C.Core.Calc.Horizontal;


public class HorizontalCalc : IHorizontalCalc
{
    private readonly IAzAltFacade _azAltFacade;

    public HorizontalCalc(IAzAltFacade azAltFacade) => _azAltFacade = azAltFacade;

    public double[] CalculateHorizontal(double jdUt, Location location, EclipticCoordinates eclipticCoordinates, int flags)
    {
        var geoGraphicLonLat = new double[] { location.GeoLong, location.GeoLat };
        var eclipticLonLat = new double[] { eclipticCoordinates.Longitude, eclipticCoordinates.Latitude };
        return _azAltFacade.RetrieveHorizontalCoordinates(jdUt, geoGraphicLonLat, eclipticLonLat, flags);
    }
}
