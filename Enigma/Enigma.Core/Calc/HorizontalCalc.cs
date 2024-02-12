// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Calculate the horizontal coordinates.</summary>
public interface IHorizontalCalc
{
    /// <summary>Perform the calculation of the horizontal coordinates.</summary>
    /// <param name="jdUt">Julian day for UT.</param>
    /// <param name="location"/>
    /// <param name="equCoordinates"/>
    /// <param name="flags">Flags for the SE.</param>
    /// <returns>Calculated horizontal coordinates (azimuth and altitude).</returns>
    public double[] CalculateHorizontal(double jdUt, Location location, EquatorialCoordinates equCoordinates, int flags);
}

/// <inheritdoc/>
public sealed class HorizontalCalc : IHorizontalCalc
{
    private readonly IAzAltFacade _azAltFacade;

    public HorizontalCalc(IAzAltFacade azAltFacade) => _azAltFacade = azAltFacade;


    /// <inheritdoc/>
    public double[] CalculateHorizontal(double jdUt, Location location, EquatorialCoordinates equCoordinates, int flags)
    {
        var geoGraphicLonLat = new[] { location.GeoLong, location.GeoLat };
        var equatRaDecl = new[] { equCoordinates.RightAscension, equCoordinates.Declination };
        return _azAltFacade.RetrieveHorizontalCoordinates(jdUt, geoGraphicLonLat, equatRaDecl, flags);
    }
}
