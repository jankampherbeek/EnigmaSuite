// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Requests;

/// <summary>Data for a request to calculate symbolic directions.</summary>
/// <param name="JdRadix">Julian day for the radix.</param>
/// <param name="JdEvent">Julian day for the event.</param>
/// <param name="ConfigSym">User preferences for the calculation of symbolic directions.</param>
/// <param name="RadixPoints">Used radixpoints and positions in longitude.</param>
public record SymDirEventRequest(double JdRadix,  double JdEvent, ConfigProgSymDir ConfigSym, 
    Dictionary<ChartPoints, double> RadixPoints);
