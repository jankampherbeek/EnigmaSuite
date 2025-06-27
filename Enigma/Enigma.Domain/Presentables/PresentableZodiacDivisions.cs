// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

/// <summary>
/// Zodiac divisions to be shown in a datagrid
/// </summary>
/// <param name="Longitude">Text for longitude (full longitude, no signs)</param>
/// <param name="Planet">Glyph for planet</param>
/// <param name="Signs">Glyph for sign</param>
/// <param name="Decans">Glyph for decan</param>
/// <param name="Dodecatemoria">Glyph for dodecatemorium</param>
/// <param name="Bounds">Glyph for bound</param>
public record PresentableZodiacDivisions(string Longitude, string Planet, string Signs, string Decans, string Dodecatemoria, string Bounds);