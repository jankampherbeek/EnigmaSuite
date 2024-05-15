// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Core.Handlers;

/// <summary>Handler for the calculation of obliquity of the earths axis.</summary>
public interface IObliquityHandler
{
    /// <summary>Start the calculation.</summary>
    /// <param name="obliquityRequest"></param>
    /// <returns></returns>
    public double CalcObliquity(ObliquityRequest obliquityRequest);
}

/// <inheritdoc/>
public sealed class ObliquityHandler : IObliquityHandler
{
    private readonly IObliquityCalc _obliquityCalc;

    public ObliquityHandler(IObliquityCalc obliquityCalc)
    {
        _obliquityCalc = obliquityCalc;
    }

    /// <inheritdoc/>
    public double CalcObliquity(ObliquityRequest obliquityRequest)
    {
        double obliquity;
        try
        {
            obliquity = _obliquityCalc.CalculateObliquity(obliquityRequest.JdUt, true);        // always use true obliquity
            // TODO 0.3 remove option for true obliquity from Obliquityrequest.
        }
        catch (SwissEphException see)
        {
            Log.Error( "ObliquityHandler.CalcObliquity(): received error: {Msg}", see.Message);
            throw new EnigmaException("SE encounterd error when calculating obliquity");
        }
        return obliquity;
    }

}