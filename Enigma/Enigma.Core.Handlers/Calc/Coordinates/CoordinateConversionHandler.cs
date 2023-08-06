// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Coordinates.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Core.Handlers.Calc.Coordinates;


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

