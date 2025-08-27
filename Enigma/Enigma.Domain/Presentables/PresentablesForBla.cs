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

/// <summary>
/// Presentable data for elements and crosses in a BLA Schema
/// </summary>
/// <param name="Name">Name of element or cross</param>
/// <param name="Sign">Count in signs</param>
/// <param name="House">Count in houses</param>
/// <param name="Spacer1">Empty column for spacing</param>
/// <param name="Sum">Som of count for houses and signs</param>
/// <param name="Spacer2">Empty column for spacing</param>
/// <param name="Hcusp">HCusp</param>
/// <param name="Spacer3">Empty column for spacing</param>
/// <param name="Total">Total</param>
public record PresentableCrossElementsCount(String Name, int Sign, int House, string Spacer1, int Sum, string Spacer2, int Hcusp, string Spacer3, int Total);