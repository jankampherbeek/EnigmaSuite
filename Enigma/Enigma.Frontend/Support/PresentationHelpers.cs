// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.Support;

public class SelectableChartPointDetails
{
    public ChartPoints ChartPoint { get; init; }
    public char? Glyph { get; set; }
    public string? Name { get; set; }
    public bool Selected { get; set; }
}
