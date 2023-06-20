// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Progressive;

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;


/// <summary>Data for an event.</summary>
/// <remarks>Data required for calculations and data to be shown to the user.</remarks>
/// <param name="Id">Unique Id that also serves as a primary key in the database.</param>
/// <param name="Description">Description for this event.</param>
/// <param name="LocationName">Name/description for the location.</param>
/// <param name="Location">Location related data.</param>
/// <param name="FullDateTime">Date/time related data.</param>
public record ChartData
{
    public int Id { get; set; }
    public string Description { get; }
    public string LocationName { get; }
    public Location Location { get; }
    public FullDateTime FullDateTime { get; }

    public ChartData(int id, string description, string locationName, Location location, FullDateTime fullDateTime)
    {
        Id = id;
        Description = description;
        LocationName = locationName;    
        Location = Location;
        FullDateTime = fullDateTime;
    }


}
