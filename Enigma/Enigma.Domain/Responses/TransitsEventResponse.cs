// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Responses;

/// <summary>Response for the calculation of transit positions.</summary>
/// <param name="Positions">Positions for each point.</param>
/// <param name="ResultCode">Defines success or any failure.</param>
public record TransitsEventResponse(Dictionary<ChartPoints, ProgPositions> Positions, int ResultCode);