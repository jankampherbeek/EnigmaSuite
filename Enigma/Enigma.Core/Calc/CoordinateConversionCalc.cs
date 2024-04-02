// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Convert ecliptical longitude and latitude to equatorial right ascension and declination, using the SE.</summary>
public interface ICoordinateConversionCalc
{
    /// <summary>Convert ecliptic coordinates to equatorial coordinates.</summary>
    /// <param name="eclCoord">The ecliptic coordinates.</param>
    /// <param name="obliquity">True obliquity of the earths axis.</param>
    /// <returns>The equatorial coordinates.</returns>
    public EquatorialCoordinates PerformConversion(EclipticCoordinates eclCoord, double obliquity);
}


/// <summary>Direct conversions between celestial coördinates.</summary>
public interface IDirectConversionCalc
{
    /// <summary>Convert declination to longitude.</summary>
    /// <remarks>Does not take latitude into account, does not take correct hemisphere into account.</remarks>
    /// <param name="obliquity">Obliquity.</param>
    /// <param name="declination">Then declination to convert.</param>
    /// <returns></returns>
    public double DeclinationToLongitude(double obliquity, double declination);
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

/// <inheritdoc/>
public sealed class DirectConversionCalc: IDirectConversionCalc
{
    
    /// <inheritdoc/>
    public double DeclinationToLongitude(double obliquity, double declination)
    {
        double sinDeclRad = (Math.Sin(MathExtra.DegToRad(declination)));
        double sinOblRad = (Math.Sin(MathExtra.DegToRad(obliquity)));
        return MathExtra.RadToDeg(Math.Asin(sinDeclRad / sinOblRad));
    }
}