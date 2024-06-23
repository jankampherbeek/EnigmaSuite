// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Charts.Prog.PrimDir;

/// <summary>Request with dta for the calculation of primary directions.</summary>
/// <param name="Chart">A calculated chart.</param>
/// <param name="Significators">Significators to use.</param>
/// <param name="Promissors">Promissors to use.</param>
/// <param name="Aspects">Aspects to use.</param>
/// <param name="StartDate">Start date of the period to check.</param>
/// <param name="EndDate">End date of the period to check.</param>
/// <param name="Method">Primary directions method.</param>
/// <param name="TimeKey">The time key to use.</param>
/// <param name="Approach">Approach (inMundo or inZodiaca).</param>
/// <param name="ConverseOption">Which option to use for converse directions.</param>
public record PrimDirRequest(
    CalculatedChart Chart, 
    List<ChartPoints> Significators, 
    List<ChartPoints> Promissors,
    List<AspectTypes> Aspects,
    SimpleDate StartDate,
    SimpleDate EndDate,
    PrimDirMethods Method,
    PrimDirTimeKeys TimeKey,
    PrimDirApproaches Approach,
    PrimDirConverseOptions ConverseOption);