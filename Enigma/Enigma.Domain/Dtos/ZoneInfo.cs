// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Information about a time zone and DST</summary>
/// <param name="Offset">Actual offset, including offset for DST</param>
/// <param name="TzName">Abbreviation of the zone or an indication of the offset</param>
/// <param name="Dst">True if DST applies</param>
public record ZoneInfo(double Offset, string TzName, bool Dst);