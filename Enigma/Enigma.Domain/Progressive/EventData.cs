// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Progressive;

using Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;


public record EventData
{
    public int Id { get; set; }
    public string Description { get; }
    public string LocationName { get; }
    public Location Location { get; }
    public FullDateTime FullDateTime { get; }

    public EventData(int id, string description, string locationName, Location location, FullDateTime fullDateTime)
    {
        Id = id;
        Description = description;
        LocationName = locationName;    
        Location = location;
        FullDateTime = fullDateTime;
    }


}
