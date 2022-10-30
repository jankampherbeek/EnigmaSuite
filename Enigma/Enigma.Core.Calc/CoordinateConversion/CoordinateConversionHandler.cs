// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Exceptions;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Calc.CoordinateConversion;


/// <inheritdoc/>
public class CoordinateConversionHandler : ICoordinateConversionHandler
{

    private readonly ICoordinateConversionCalc _conversionCalc;

    public CoordinateConversionHandler(ICoordinateConversionCalc conversionCalc) => _conversionCalc = conversionCalc;

    public CoordinateConversionResponse HandleConversion(CoordinateConversionRequest request)
    {
        string errorText = "";
        bool success = true;
        EquatorialCoordinates equatorialCoordinates = null;
        try
        {
            equatorialCoordinates = _conversionCalc.PerformConversion(request.EclCoord, request.Obliquity);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new CoordinateConversionResponse(equatorialCoordinates, success, errorText);
    }

}

