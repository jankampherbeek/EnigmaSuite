// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;

namespace E4C.domain.shared.positions;

/// <summary>
/// Full results of calculation for mundane positions (cusps, asc. mc, vertex, eastpoint), supports ecliptic, equatorial and horizontal coordinates.
/// </summary>
public record FullMundanePositions
{

    public readonly List<CuspFullPos> Cusps;
    public readonly CuspFullPos Mc;
    public readonly CuspFullPos Ascendant;
    public readonly CuspFullPos Vertex;
    public readonly CuspFullPos EastPoint;

    /// <summary>
    /// Constructor for record CalculatedMundanePositions.
    /// </summary>
    /// <param name="cusps">List with full positions for cusps, in the sequence 1 ..n. </param>
    /// <param name="mc"/>
    /// <param name="ascendant"/>
    /// <param name="vertex"/>
    /// <param name="eastpoint"/>
    public FullMundanePositions(List<CuspFullPos> cusps, CuspFullPos mc, CuspFullPos ascendant, CuspFullPos vertex, CuspFullPos eastpoint)
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
    public readonly double RightAscension;
    public readonly double Declination;
    public readonly HorizontalPos AzimuthAltitude;

    /// <summary>
    /// Constructor for the full position of a cusp/mundane point.
    /// </summary>
    /// <param name="longitude"/>
    /// <param name="rightAscension"/>
    /// <param name="declination"/>
    /// <param name="azimuthAltitude">Horizontal coordinates.</param>
    public CuspFullPos(double longitude, double rightAscension, double declination, HorizontalPos azimuthAltitude)
    {
        Longitude = longitude;
        RightAscension = rightAscension;
        Declination = declination;
        AzimuthAltitude = azimuthAltitude;
    }
}