// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Cycles;

public class CyclesDefinition
{
    public string Name { get; }
    public string Description { get; }
    public long CycleType { get; }
    public List<ChartPoints> Points { get; }
    public List<Tuple<ChartPoints, ChartPoints>> PointPaires { get; }
    



}