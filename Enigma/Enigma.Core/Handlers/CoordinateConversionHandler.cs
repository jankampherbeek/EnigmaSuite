// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Handlers;


/// <summary>Handles the conversion from ecliptical to equatorial coordinates.</summary>
public interface ICoordinateConversionHandler
{
    /// <summary>Start conversion from ecliptical to equatorial coordinates..</summary>
    /// <param name="request">Request with astronomical details.</param>
    /// <returns>Equatorial coordinates.</returns>
    public EquatorialCoordinates HandleConversion(CoordinateConversionRequest request);
}

/// <inheritdoc/>
public sealed class CoordinateConversionHandler : ICoordinateConversionHandler
{

    private readonly ICoordinateConversionCalc _conversionCalc;

    public CoordinateConversionHandler(ICoordinateConversionCalc conversionCalc) => _conversionCalc = conversionCalc;

    /// <inheritdoc/>
    public EquatorialCoordinates HandleConversion(CoordinateConversionRequest request)
    {
        return _conversionCalc.PerformConversion(request.EclCoord, request.Obliquity);
    }

}

