// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Requests;

/// <summary>Request for the calculation of primary directions.</summary>
/// <param name="PdDirMethod">Method for primary directions.</param>
/// <param name="PdKeys">Time key.</param>
/// <param name="Chart">Calculated chart.</param>
/// <param name="Promissors">List of promissors, Enigma uses only promissors that are available in the Chart.</param>
/// <param name="Significators">List of significators,
/// Enigma uses only significators that are available in the Chart.</param>
/// <param name="Aspects">Aspects to take into accoount.</param>
/// <param name="IncludeConverse">True if converse directions are required, otherwise false.
/// PD's that are direct are always calculated.</param>
/// <param name="Jdnr">Julian day number for the central date for the calculation.</param>
/// <param name="PeriodInYears">Total length of the period to check, the period is equally divided in parts before
/// and after jdnr.</param>
/// <param name="ObserverPos">Observer position, should be either geocentric or topocentric.</param>
public record ProgPrimDirRequest(
    PrimaryDirMethods PdDirMethod,
    PrimaryKeys PdKeys,
    CalculatedChart Chart,
    List<ChartPoints> Promissors,
    List<ChartPoints> Significators,
    List<AspectTypes> Aspects,
    bool IncludeConverse,
    double Jdnr,
    int PeriodInYears,
    ObserverPositions ObserverPos
    );
