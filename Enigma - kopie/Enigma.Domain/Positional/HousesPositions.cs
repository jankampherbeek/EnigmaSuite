// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Positional;

/// <summary>
/// Full results of calculation for houses, including cusps, asc. mc, vertex, eastpoint. Supports ecliptic, equatorial and horizontal coordinates.
/// </summary>
public record FullHousesPositions
{

    public readonly List<CuspFullPos> Cusps;
    public readonly CuspFullPos Mc;
    public readonly CuspFullPos Ascendant;
    public readonly CuspFullPos Vertex;
    public readonly CuspFullPos EastPoint;

    /// <summary>
    /// Constructor for record FullHousePositions.
    /// </summary>
    /// <param name="cusps">List with full positions for cusps, in the sequence 1 ..n. </param>
    /// <param name="mc"/>
    /// <param name="ascendant"/>
    /// <param name="vertex"/>
    /// <param name="eastpoint"/>
    public FullHousesPositions(List<CuspFullPos> cusps, CuspFullPos mc, CuspFullPos ascendant, CuspFullPos vertex, CuspFullPos eastpoint)
    {
        Cusps = cusps;
        Mc = mc;
        Ascendant = ascendant;
        Vertex = vertex;
        EastPoint = eastpoint;
    }

}

/// <summary>
/// Full position for  a single cusp or other mundane point.
/// </summary>
public record CuspFullPos
{
    public readonly double Longitude;
    public readonly EquatorialCoordinates RaDecl;
    public readonly HorizontalCoordinates AzimuthAltitude;

    /// <summary>
    /// Constructor for the full position of a cusp/mundane point.
    /// </summary>
    /// <param name="longitude"/>
    /// <param name="raDecl">Equatorial coordinates.</param>
    /// <param name="azimuthAltitude">Horizontal coordinates.</param>
    public CuspFullPos(double longitude, EquatorialCoordinates raDecl, HorizontalCoordinates azimuthAltitude)
    {
        Longitude = longitude;
        RaDecl = raDecl;
        AzimuthAltitude = azimuthAltitude;
    }
}