﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Exceptions;
using Serilog;

namespace Enigma.Core.Handlers.Calc.Specials;


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
            obliquity = _obliquityCalc.CalculateObliquity(obliquityRequest.JdUt, obliquityRequest.TrueObliquity);
        }
        catch (SwissEphException see)
        {
            string errorText = "ObliquityHandler.CalcObliquity(): received error: " + see.Message;
            Log.Error(errorText);
            throw new EnigmaException(errorText);
        }
        return obliquity;
    }

}