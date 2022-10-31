// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;

namespace Enigma.Core.Calc.CoordinateConversion;

/// <inheritdoc/>
public class CoordinateConversionCalc : ICoordinateConversionCalc
{
    private readonly ICoTransFacade _coTransFacade;

    public CoordinateConversionCalc(ICoTransFacade coTransFacade) => _coTransFacade = coTransFacade;

    public EquatorialCoordinates PerformConversion(EclipticCoordinates eclCoord, double obliquity)
    {
        double[] ecliptic = { eclCoord.Longitude, eclCoord.Latitude };
        double[] equatorial = _coTransFacade.EclipticToEquatorial(ecliptic, obliquity);
        return new EquatorialCoordinates(equatorial[0], equatorial[1]);
    }
}