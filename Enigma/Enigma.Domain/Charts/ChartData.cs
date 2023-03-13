// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;

namespace Enigma.Domain.Charts;

/// <summary>Data for a chart.</summary>
/// <remarks>Data required for calculations and data to be shown to the user. Does not contain the astronomical positions.</remarks>
/// <param name="Id">Unique Id that also serves as a primary key in the database.</param>
/// <param name="MetaData">Metadata for this chart.</param>
/// <param name="Location">Location related data.</param>
/// <param name="FullDateTime">Date/time related data.</param>
public record ChartData { 
    public int Id { get; set; }
    public MetaData MetaData { get; }
    public Location Location { get; }
    public FullDateTime FullDateTime { get; }

    public ChartData(int Id, MetaData MetaData, Location Location, FullDateTime FullDateTime)
    {
        this.Id = Id;
        this.MetaData = MetaData;
        this.Location = Location;
        this.FullDateTime = FullDateTime;
    }

    
}

