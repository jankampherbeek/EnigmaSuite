// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

/// <summary>
/// Presentable data for a BLA position
/// </summary>
/// <param name="PointTextGlyph">Glyph for the chart point</param>
/// <param name="PosText">Sexagesimal position without sign</param>
/// <param name="SignGlyph">Glyph for the sign</param>
/// <param name="HouseNr">Roman number for the actual house</param>
/// <param name="Decanate">Glyph for the decanate</param>
public record PresentableBlaPosition(char PointTextGlyph, string PosText, char SignGlyph, string HouseNr, char Decanate);