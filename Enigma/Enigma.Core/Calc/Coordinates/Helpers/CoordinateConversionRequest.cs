// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;

namespace Enigma.Core.Calc.Coordinates.Helpers;

/// <summary>Request for the conversion of ecliptical coordinates to equatorial coordinates.</summary>
/// <param name="EclCoord">Ecliptical coordinates.</param>
/// <param name="Obliquity">True obliquity of the earth's axis.</param>
public record CoordinateConversionRequest(EclipticCoordinates EclCoord, double Obliquity);
