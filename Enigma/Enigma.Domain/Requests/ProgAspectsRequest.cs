// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Requests;

/// <summary>Request for aspects between progressed positions and the radix.</summary>
/// <param name="RadixPoints">Radix points and positions to check.</param>
/// <param name="ProgPoints">Progressive points and positions to check.</param>
/// <param name="SupportedAspects">Aspecttypoes to check.</param>
/// <param name="Orb">Orb to use. The orb is fixed for progressive analysis.</param>
public record ProgAspectsRequest(Dictionary<ChartPoints, double> RadixPoints, 
    Dictionary<ChartPoints, double> ProgPoints, List<AspectTypes> SupportedAspects, double Orb);