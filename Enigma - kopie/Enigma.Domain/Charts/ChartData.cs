// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.DateTime;
using Enigma.Domain.Locational;

namespace Enigma.Domain.Charts;

/// <summary>
/// Data for a chart.
/// </summary>
/// <remarks>
/// Data required for calculations and data to be shown to the user. Does not contain the astronomical positions.
/// </remarks>
public record ChartData
{
    public readonly int Id;
    public int TempId { get; set; }
    public readonly MetaData ChartMetaData;
    public readonly Location ChartLocation;
    public readonly FullDateTime ChartDateTime;

    /// <summary>
    /// Constructor for record ChartData.
    /// </summary>
    /// <param name="id">Unique id that also serves as a primary key in the database.</param>
    /// <param name="tempId">Temporary id, unique within the set of charts that are currently avaiable to the user.</param>
    /// <param name="metaData">Metadata for this chart.</param>
    /// <param name="location">Location related data.</param>
    /// <param name="fullDateTime">Date/time related data.</param>
    public ChartData(int id, int tempId, MetaData metaData, Location location, FullDateTime fullDateTime)
    {
        Id = id;
        TempId = tempId;
        ChartMetaData = metaData;
        ChartLocation = location;
        ChartDateTime = fullDateTime;
    }
}