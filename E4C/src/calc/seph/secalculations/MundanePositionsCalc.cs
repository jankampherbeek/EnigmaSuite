// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.seph.sefacade;
using E4C.domain.shared.references;
using E4C.domain.shared.positions;
using E4C.domain.shared.specifications;
using System.Collections.Generic;
using E4C.core.facades;

namespace E4C.calc.seph.secalculations;

/// <summary>
/// Calculations for mundane positions (houses etc.).
/// </summary>
public interface IMundanePositionsCalculator
{
    /// <summary>
    /// Full calculate house cusps, MC, Ascendant, Vertex and Eastpoint. Returns positions in ecliptic, equatorial and horizontal coordinates. 
    /// </summary>
    /// <param name="julianDayUt">Julian Day for UT.</param>
    /// <param name="obliquity">Obliquity of the earths axis.</param>
    /// <param name="flags">Flags with the required settings.</param>
    /// <param name="location">Location with coordinates.</param>
    /// <param name="houseSystem">The Housesystem to use, from the enum HouseSystems.</param>
    /// <returns>Instance of MundanePositions with the calculated values.</returns>
    public FullMundanePositions CalculateAllMundanePositions(double julianDayUt, double obliquity, int flags, Location location, HouseSystems houseSystem);
}

/// <inheritdoc/>
public class MundanePositionsCalculator : IMundanePositionsCalculator
{
    private readonly ISePosHousesFacade _sePosHousesFacade;
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IHorizontalCoordinatesFacade _horizontalCoordinatesFacade;
    private readonly IHouseSystemSpecs _houseSystemSpecifications;

    /// <inheritdoc/>
    public MundanePositionsCalculator(ISePosHousesFacade sePosHousesFacade, ICoTransFacade coordinateConversionFacade,
        IHorizontalCoordinatesFacade horizontalCoordinatesFacade, IHouseSystemSpecs houseSystemSpecifications)
    {
        _sePosHousesFacade = sePosHousesFacade;
        _coordinateConversionFacade = coordinateConversionFacade;
        _horizontalCoordinatesFacade = horizontalCoordinatesFacade;
        _houseSystemSpecifications = houseSystemSpecifications;
    }

    /// <inheritdoc/>
    public FullMundanePositions CalculateAllMundanePositions(double julianDayUt, double obliquity, int flags, Location location, HouseSystems houseSystem)
    {
        char houseSystemId = _houseSystemSpecifications.DetailsForHouseSystem(houseSystem).SeId;
        int nrOfCusps = _houseSystemSpecifications.DetailsForHouseSystem(houseSystem).NrOfCusps;
        double[][] longitudeValues = _sePosHousesFacade.PosHousesFromSe(julianDayUt, flags, location.GeoLat, location.GeoLong, houseSystemId);
        var cusps = new List<CuspFullPos>();
        for (int i = 0; i < nrOfCusps; i++)
        {
            double longitude = longitudeValues[0][i + 1];
            cusps.Add(CreateFullMundanePos(julianDayUt, obliquity, longitude, flags, location));
        }
        CuspFullPos mc = CreateFullMundanePos(julianDayUt, obliquity, longitudeValues[1][0], flags, location);
        CuspFullPos asc = CreateFullMundanePos(julianDayUt, obliquity, longitudeValues[1][1], flags, location);
        CuspFullPos vertex = CreateFullMundanePos(julianDayUt, obliquity, longitudeValues[1][3], flags, location);
        CuspFullPos eastPoint = CreateFullMundanePos(julianDayUt, obliquity, longitudeValues[1][4], flags, location);
        return new FullMundanePositions(cusps, mc, asc, vertex, eastPoint);
    }

    private CuspFullPos CreateFullMundanePos(double jdnr, double obliquity, double eclLongitude, int flags, Location location)
    {
        double latitude = 0.0;    // always zero for mundane positions.
        double distance = 1.0;    // placeholder.
        var geographicCoordinates = new double[] { location.GeoLong, location.GeoLat, distance };
        var eclipticCoordinates = new double[] { eclLongitude, latitude, distance };
        double[] equatorialCoordinates = _coordinateConversionFacade.EclipticToEquatorial(new double[] { eclLongitude, 0.0 }, obliquity);
        HorizontalPos horizontalPos = _horizontalCoordinatesFacade.CalculateHorizontalCoordinates(jdnr, geographicCoordinates, eclipticCoordinates, flags);
        return new CuspFullPos(eclLongitude, equatorialCoordinates[0], equatorialCoordinates[1], horizontalPos);
    }
}

