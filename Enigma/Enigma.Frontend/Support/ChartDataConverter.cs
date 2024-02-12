// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;

namespace Enigma.Frontend.Ui.Support;

/// <summary>Conversion to and from ChartData/PersistebleChartData.</summary>
public interface IChartDataConverter
{
    /// <summary>Convert PersistableChartData to ChartData.</summary>
    /// <param name="persistableChartData"/>
    /// <returns>Resulting ChartData.</returns>
    public ChartData FromPersistableChartData(PersistableChartData persistableChartData);

    /// <summary>Convert ChartData to PersistableChartData.</summary>
    /// <param name="chartData"/>
    /// <returns>Resulting PersistableChartData.</returns>
    public PersistableChartData ToPersistableChartData(ChartData chartData);

}

/// <inheritdoc/>
public sealed class ChartDataConverter : IChartDataConverter
{
    private readonly ILocationConversion _locationConversion;

    public ChartDataConverter(ILocationConversion locationConversion)
    {
        _locationConversion = locationConversion;
    }

    /// <inheritdoc/>
    public ChartData FromPersistableChartData(PersistableChartData persistableChartData)
    {
        return HandleConversion(persistableChartData);
        
    }

    /// <inheritdoc/>
    public PersistableChartData ToPersistableChartData(ChartData chartData)
    {
        return HandleConversion(chartData);
    }
    
    private ChartData HandleConversion(PersistableChartData persistableChartData)
    {
        PersistableChartIdentification cIdent = persistableChartData.Identification;
        PersistableChartDateTimeLocation dtLoc = persistableChartData.DateTimeLocs[0];  
        string name = cIdent.Name;
        string description = cIdent.Description;
        string source = dtLoc.Source;               
        string locationName =dtLoc.LocationName;
        long chartCategory = cIdent.ChartCategoryId;
        long rating = dtLoc.RatingId;
        MetaData metaData = new(name, description, source, locationName, chartCategory, rating);
        string locationFullName = _locationConversion.CreateLocationDescription(locationName, dtLoc.GeoLat, dtLoc.GeoLong);
        Location location = new(locationFullName, dtLoc.GeoLong, dtLoc.GeoLat);
        FullDateTime fullDateTime = new(dtLoc.DateText, dtLoc.TimeText, dtLoc.JdForEt);
        return new ChartData(cIdent.Id, metaData, location, fullDateTime);
    }

    private static PersistableChartData HandleConversion(ChartData chartData)
    {
        var meta = chartData.MetaData;
        var dTime = chartData.FullDateTime;
        var loc = chartData.Location;
        PersistableChartIdentification cIdent = new()
        {
            Id = chartData.Id,
            Name = chartData.MetaData.Name,
            Description = chartData.MetaData.Description,
            ChartCategoryId = chartData.MetaData.ChartCategory
        };
        PersistableChartDateTimeLocation dtLoc = new()
        {
            Id = -1,
            ChartId = chartData.Id,
            Source = meta.Source,
            DateText = dTime.DateText,
            TimeText = dTime.TimeText,
            LocationName = loc.LocationFullName,
            RatingId = meta.RoddenRating,
            GeoLong = loc.GeoLong,
            GeoLat = loc.GeoLat,
            JdForEt = dTime.JulianDayForEt
        };
        List<PersistableChartDateTimeLocation> allDtLocs = new() { dtLoc };
        return new PersistableChartData(cIdent, allDtLocs);
    }
    
}