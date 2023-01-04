// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.AstronCalculations;

/// <summary>
/// Horizontal coordinates.
/// </summary>
/// <param name="Azimuth">Azimuth, starts at south (0 degrees) in western direction (90 degrees) etc. North = 180 degrees and east = 270 degrees.</param>
/// <param name="Altitude">Altitude (height above horizon, negative is below horizon).</param>
public record HorizontalCoordinates(double Azimuth, double Altitude);
