// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Collections.Immutable;

namespace Enigma.Domain.AstronCalculations;

/// <summary>
/// Full results of calculation for houses, including cusps, asc. mc, vertex, eastpoint. Supports ecliptic, equatorial and horizontal coordinates.
/// </summary>
public record FullHousesPositions
{

    public readonly ImmutableList<CuspFullPos> Cusps;
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
        Cusps = cusps.ToImmutableList();
        Mc = mc;
        Ascendant = ascendant;
        Vertex = vertex;
        EastPoint = eastpoint;
    }

}

/// <summary>Full position for  a single cusp or other mundane point.</summary>
/// <param name="Name">Name for mundane point.</param>
/// <param name="Longitude"/>
/// <param name="RaDecl">Equatorial coordinates.</param>
/// <param name="AzimuthAltitude">Horizontal coordinates.</param>
public record CuspFullPos(string Name, double Longitude, EquatorialCoordinates RaDecl, HorizontalCoordinates AzimuthAltitude);
