// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.AstronCalculations;

/// <summary>
/// Full results of calculation for houses, including Cusps, asc. Mc, Vertex, Eastpoint. Supports ecliptic, equatorial and horizontal coordinates.
/// </summary>
/// <param name="Cusps">List with full positions for Cusps, in the sequence 1 ..n. </param>
/// <param name="Mc"/>
/// <param name="Ascendant"/>
/// <param name="Vertex"/>
/// <param name="EastPoint"/>
public record FullHousesPositions(List<CuspFullPos> Cusps, CuspFullPos Mc, CuspFullPos Ascendant, CuspFullPos Vertex, CuspFullPos EastPoint);


/// <summary>Full position for  a single cusp or other mundane point.</summary>
/// <param name="Name">Name for mundane point.</param>
/// <param name="Longitude"/>
/// <param name="RaDecl">Equatorial coordinates.</param>
/// <param name="AzimuthAltitude">Horizontal coordinates.</param>
public record CuspFullPos(string Name, double Longitude, EquatorialCoordinates RaDecl, HorizontalCoordinates AzimuthAltitude);
