// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Progressive;

using Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;


/// <summary>Event of period for progressive analysis. </summary>
/// <param name="Id">Unique id.</param>
/// <param name="Description">Description of the event or period.</param>
/// <param name="LocationName">Name of the location.</param>
/// <param name="Location">Datails for the location.</param>
/// <param name="StartDateTime">Date and time of the event of for the start of the period.</param>
/// <param name="EndDateTime">Optional date and time for the end of the period.</param>
public record ProgEvent(int Id, string Description, string LocationName, Location Location, 
    FullDateTime StartDateTime, FullDateTime? EndDateTime = null)
{
    public int Id { get; set; } = Id;
}
