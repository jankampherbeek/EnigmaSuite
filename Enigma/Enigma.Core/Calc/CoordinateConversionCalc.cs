// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Calc;

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