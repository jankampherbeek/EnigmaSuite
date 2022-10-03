// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Positional;

namespace Enigma.Core.Calc.CoordinateConversion;

/// <summary>Handles the conversion from ecliptical to equatorial coordinates.</summary>
public interface ICoordinateConversionHandler
{
    public CoordinateConversionResponse HandleConversion(CoordinateConversionRequest request);
}


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

