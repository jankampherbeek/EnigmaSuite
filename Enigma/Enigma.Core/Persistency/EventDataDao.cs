// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using LiteDB;
using Serilog;

namespace Enigma.Core.Persistency;

/// <inheritdoc />
public sealed class EventDataDao : IEventDataDao
{
    private const string COL_INTERSECTION = "chartevents";
    private const string COL_EVENTS = "events";
    private readonly string _dbFullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.DATABASE_NAME;
    private readonly IInterChartEventDao _intersectionDao;

    public EventDataDao(IInterChartEventDao intersectionDao)
    {
        _intersectionDao = intersectionDao;
    }

    /// <inheritdoc />
    public int CountRecords()
    {
        return ReadRecordsFromDatabase().Count;
    }

    public int CountRecords(int chartId)
    {
        return ReadEventsForChartFromDatabase(chartId).Count;
    }


    /// <inheritdoc />
    public int HighestIndex()
    {
        return SearchHighestIndex();
    }

    /// <inheritdoc />
    public PersistableEventData? ReadEventData(int index)
    {
        return PerformRead(index);
    }

    /// <inheritdoc />
    public List<PersistableEventData> ReadAllEventData()
    {
        return ReadRecordsFromDatabase();
    }

    /// <inheritdoc />
    public List<PersistableEventData> SearchEventData(int chartId)
    {
        return PerformSearch(chartId);
    }

    /// <inheritdoc />
    public List<PersistableEventData> SearchEventData(string? partOfDescription)
    {
        return PerformSearch(partOfDescription);
    }

    /// <inheritdoc />
    public int AddEventData(PersistableEventData eventData)
    {
        return PerformInsert(eventData);
    }

    /// <inheritdoc />
    public int AddEventData(PersistableEventData eventData, int idChart)
    {
        int idEvent = PerformInsert(eventData);
        int newId = HighestIndex();
        _intersectionDao.Insert(idChart, newId);
        return idEvent;
    }

    /// <inheritdoc />
    public bool DeleteEventData(int index)
    {
        return PerformDelete(index);
    }

    private int SearchHighestIndex()
    {
        List<PersistableEventData> records = ReadRecordsFromDatabase();
        return records.Select(item => item.Id).Prepend(0).Max();
    }

    private PersistableEventData? PerformRead(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableEventData> col = db.GetCollection<PersistableEventData>(COL_EVENTS);
        col.EnsureIndex(x => x.Id);
        PersistableEventData? result = col.FindOne(x => x.Id.Equals(index));
        return result;
    }

    private List<PersistableEventData> PerformSearch(string? partOfDescription)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableEventData>? col = db.GetCollection<PersistableEventData>(COL_EVENTS);
        List<PersistableEventData> records = col.Query()
            .Where(x => partOfDescription != null && x.Description.ToUpper().Contains(partOfDescription.ToUpper()))
            .OrderBy(x => x.JulianDayEt)
            .Limit(100)
            .ToList();
        return records;
    }

    private List<PersistableEventData> PerformSearch(int chartId)
    {
        List<PersistableEventData> allRecords = new();
        IEnumerable<InterChartEvent> intersections = _intersectionDao.Read(chartId);
        using var db = new LiteDatabase(_dbFullPath);
        var col = db.GetCollection<PersistableEventData>(COL_EVENTS);
        col.EnsureIndex(x => x.Id);
        
        foreach (var intersection in intersections)
        {
            int eventId = intersection.EventId;
            var eventItems = col.Query()
                .Where(x => x.Id == eventId)
                .OrderBy(x => x.Id)
                .Select(x => new { x.Description, x.JulianDayEt, x.DateText, x.TimeText, 
                    x.LocationName, x.GeoLong, x.GeoLat, x.Id })
                .Limit(100)
                .ToList();
            foreach (var eventItem in eventItems)
            {
                PersistableEventData eventData = new(eventItem.Description, eventItem.JulianDayEt, eventItem.DateText,
                    eventItem.TimeText, eventItem.LocationName, eventItem.GeoLong, eventItem.GeoLat, eventItem.Id);
                allRecords.Add(eventData);
            }
        }
        return allRecords;
    }

    private int PerformInsert(PersistableEventData eventData)
    {
        using var db = new LiteDatabase(_dbFullPath);
        int idNewEvent = 0;
        ILiteCollection<PersistableEventData>? col = db.GetCollection<PersistableEventData>(COL_EVENTS);
        try
        {
            col.Insert(eventData);
            idNewEvent = HighestIndex();
            Log.Information("Inserted event {EventId}", idNewEvent);
        }
        catch (Exception e)
        {
            Log.Error(
                "EventDataDao.PerformInsert: trying to insert event with existing id results in exception {Ex}",
                e.Message);
            idNewEvent = 0;
        }
        return idNewEvent;
    }


    private bool PerformDelete(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableEventData>? col = db.GetCollection<PersistableEventData>(COL_EVENTS);
        ILiteCollection<InterChartEvent>? colIntersections = db.GetCollection<InterChartEvent>(COL_INTERSECTION);
        bool result = col.Delete(index);
        int deletedCount = 0;
        if (result) deletedCount = colIntersections.DeleteMany(x => x.ChartId.Equals(index));
        Log.Information("Deleted event {Event} and related {ICount} intersections, success {Success}", index,
            deletedCount, result);
        return result;
    }


    private List<PersistableEventData> ReadRecordsFromDatabase()
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableEventData>? col = db.GetCollection<PersistableEventData>(COL_EVENTS);
        List<PersistableEventData> records = col.FindAll().ToList();
        return records;
    }


    private List<PersistableEventData> ReadEventsForChartFromDatabase(int chartId)
    {
        List<PersistableEventData> allEvents = ReadRecordsFromDatabase();
        List<InterChartEvent> allIntersections = _intersectionDao.ReadAll();

        return (from eventData in allEvents
            from intersection in allIntersections
            where eventData.Id == intersection.EventId && chartId == intersection.ChartId
            select eventData).ToList();
    }
}