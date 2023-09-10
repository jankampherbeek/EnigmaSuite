// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Domain.Responses;

/// <summary>Response with aspects between progressed positions and the radix.</summary>
/// <param name="Aspects">Aspects that were found.</param>
/// <param name="ResultCode">Zero if ok, otherwise errorcode.</param>
public record ProgAspectsResponse(List<DefinedAspect> Aspects, int ResultCode);