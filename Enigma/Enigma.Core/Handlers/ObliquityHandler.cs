// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Core.Handlers;


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
            obliquity = _obliquityCalc.CalculateObliquity(obliquityRequest.JdUt, false);        // always use mean obliquity
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