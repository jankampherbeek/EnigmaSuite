// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Responses;

/// <summary>Response for the calculation of primary directions.</summary>
/// <param name="Matches">All matches within the checked timespan.</param>
/// <param name="Speculum">Speculum with astronomical values.</param>
/// <param name="ResultCode">Defines success or any failure.</param>
public record ProgPrimDirResponse(List<TimedProgMatch> Matches, Speculum Speculum, int ResultCode);

/// <summary>Definition of a specific match.</summary>
/// <param name="Significator">The point that is the significator.</param>
/// <param name="Promissor">The point that is the promissor.</param>
/// <param name="Aspect">The aspect that was formed.</param>
/// <param name="Jdnr">The julian day number when the aspect was exact.</param>
public record TimedProgMatch(ChartPoints Significator, ChartPoints Promissor, AspectTypes Aspect, double Jdnr);
