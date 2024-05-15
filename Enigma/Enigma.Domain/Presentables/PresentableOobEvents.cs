// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

/// <summary>Presentable OOB events to be shown in a datagrid for an OOB Calendar.</summary>
/// <param name="Year">Year.</param>
/// <param name="Month">Month.</param>
/// <param name="Day">Day.</param>
/// <param name="Point">Glyph for the chart point.</param>
/// <param name="TypeOfChange">Description of the status or the change.</param>
public record PresentableOobEvents(int Year, int Month, int Day, char Point, string TypeOfChange);
