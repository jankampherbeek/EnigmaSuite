// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.core.facades;
using E4C.core.shared.domain;
using E4C.domain.shared.specifications;

namespace E4C.core.astron.horizontal;

public interface IHorizontalCalc
{
    public double[] CalculateHorizontal(double jdUt, Location location, EclipticCoordinates eclipticCoordinates, int flags);
}



public class HorizontalCalc : IHorizontalCalc
{
    private readonly IAzAltFacade _azAltFacade;

    public HorizontalCalc(IAzAltFacade azAltFacade) => _azAltFacade = azAltFacade;

    public double[] CalculateHorizontal(double jdUt, Location location, EclipticCoordinates eclipticCoordinates, int flags)
    {
        var geoGraphicLonLat = new double[] {location.GeoLong, location.GeoLat};
        var eclipticLonLat = new double[] {eclipticCoordinates.Longitude, eclipticCoordinates.Latitude};
        return _azAltFacade.RetrieveHorizontalCoordinates(jdUt, geoGraphicLonLat, eclipticLonLat, flags);
    }
}
