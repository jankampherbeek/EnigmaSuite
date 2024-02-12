// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Convert ecliptical longitude and latitude to equatorial right ascension and declination.</summary>
public interface ICoordinateConversionCalc
{
    /// <summary>Convert ecliptic coordinates to equatorial coordinates.</summary>
    /// <param name="eclCoord">The ecliptic coordinates.</param>
    /// <param name="obliquity">True obliquity of the earths axis.</param>
    /// <returns>The equatorial coordinates.</returns>
    public EquatorialCoordinates PerformConversion(EclipticCoordinates eclCoord, double obliquity);

}

/// <inheritdoc/>
public sealed class CoordinateConversionCalc : ICoordinateConversionCalc
{
    private readonly ICoTransFacade _coTransFacade;

    public CoordinateConversionCalc(ICoTransFacade coTransFacade) => _coTransFacade = coTransFacade;

    /// <inheritdoc/>
    public EquatorialCoordinates PerformConversion(EclipticCoordinates eclCoord, double obliquity)
    {
        double[] ecliptic = { eclCoord.Longitude, eclCoord.Latitude };
        double[] equatorial = _coTransFacade.EclipticToEquatorial(ecliptic, obliquity);
        return new EquatorialCoordinates(equatorial[0], equatorial[1]);
    }
}