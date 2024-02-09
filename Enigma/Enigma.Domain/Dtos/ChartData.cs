// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Data for a chart.</summary>
/// <remarks>Data required for calculations and data to be shown to the user. Does not contain the astronomical positions.</remarks>
/// <param name="Id">Unique Id that also serves as a primary key in the database.</param>
/// <param name="MetaData">Metadata for this chart.</param>
/// <param name="Location">Location related data.</param>
/// <param name="FullDateTime">Date/time related data.</param>
public record ChartData(long Id, MetaData MetaData, Location Location, FullDateTime FullDateTime)
{
    public long Id { get; set; } = Id;
}

