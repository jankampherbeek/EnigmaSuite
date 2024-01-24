// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Frontend.Ui.Interfaces;


namespace Enigma.Frontend.Ui.Support;

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
        PersistableChartDateTimeLocation dtLoc = persistableChartData.DateTimeLocs[0];  // TODO 0. 3 support multiple instances of DateTimeLoc 
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
        /*PersistableChartIdentification cIdent = new(chartData.Id, chartData.MetaData.Name, chartData.MetaData.Description,
            chartData.MetaData.ChartCategory);*/
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