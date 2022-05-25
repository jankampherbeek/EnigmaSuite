// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.Positional;

namespace Enigma.Core.Calc.CoordinateConversion;

/// <summary>Convert ecliptical longitude and latitude to equatorial right ascension and declination.</summary>
public interface ICoordinateConversionCalc
{
    public EquatorialCoordinates PerformConversion(EclipticCoordinates eclCoord, double obliquity);

}

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