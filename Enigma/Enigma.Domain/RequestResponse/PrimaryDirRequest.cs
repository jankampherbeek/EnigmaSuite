// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Calc.Progressive;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;

namespace Enigma.Domain.RequestResponse;

/// <summary>Request for the calculation of primary directions.</summary>
/// <param name="PdSystem">System of primary directions.</param>
/// <param name="PdKeys">Time key.</param>
/// <param name="Chart">Calculated chart.</param>
/// <param name="Promissors">List of promissors, Enigma uses only promissors that are available in the Chart.</param>
/// <param name="Significators">List of significators, Enigma uses only significators that are available in the Chart.</param>
/// <param name="IncludeConverse">True if converse directions are required, otherwise false. PD's that are direct are always calculated.</param>
/// <param name="FirstDateTime">Start date for the calculations or time of event if LastDateTime is null.</param>
/// <param name="LastDateTime">End date for the calculations, null if the calculations are for an event.</param>
public record PrimaryDirRequest(
    PrimarySystems PdSystem,
    PrimaryKeys PdKeys,
    CalculatedChart Chart,
    List<ChartPoints> Promissors,
    List<ChartPoints> Significators,
    bool IncludeConverse,
    SimpleDateTime FirstDateTime,
    SimpleDateTime? LastDateTime
    );
