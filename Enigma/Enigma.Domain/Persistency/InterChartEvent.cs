// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Persistency;

/// <summary>Intersection data between charts and events.</summary>
public sealed class InterChartEvent
{
    public int ChartId { get; }
    public int EventId { get; }

    public InterChartEvent(int chartId, int eventId)
    {
        ChartId = chartId;
        EventId = eventId;
    }

}
