// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Core.Calc;

/// <summary>Handles the calculation of horizontal coordinates (azimuth and altitude).</summary>
public interface IHorizontalHandler
{
    /// <summary>Start the calculation of horizontal coordinates.</summary>
    /// <param name="request">Request with the astronomical information.</param>
    /// <returns>The horizontal coordinates.</returns>
    public HorizontalCoordinates CalcHorizontal(HorizontalRequest request);
}

public sealed class HorizontalHandler(IHorizontalCalc horizontalCalc) : IHorizontalHandler
{
    public HorizontalCoordinates CalcHorizontal(HorizontalRequest request)
    {
        const int flags = EnigmaConstants.SEFLG_EQUATORIAL;    // flags for horizontal coordinates, only equatorial is used.
        var azimuthAltitude = horizontalCalc.CalculateHorizontal(request.JdUt, request.Location, request.EquCoordinates, flags);
        return new HorizontalCoordinates(azimuthAltitude[0], azimuthAltitude[1]);
    }

}