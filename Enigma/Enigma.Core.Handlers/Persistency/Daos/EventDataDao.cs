// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Persistency;
using Serilog;
using System.Text.Json;

namespace Enigma.Core.Handlers.Persistency.Daos;

/// <inheritdoc/>
public sealed class EventDataDao : IEventDataDao
{
    private readonly IInterChartEventDao _intersectionDao;

    readonly string dbEventsFullPath = ApplicationSettings.Instance.LocationDatabase + EnigmaConstants.DATABASE_NAME_EVENTS;

    EventDataDao(IInterChartEventDao intersectionDao)
    {
        _intersectionDao = intersectionDao;
    }


    /// <inheritdoc/>
    public int CountRecords()
    {
        return ReadRecordsFromJson().Count;
    }

    public int CountRecords(int chartId)
    {
        return ReadEventsForChartFromJsn(chartId).Count;
    }


    /// <inheritdoc/>
    public int HighestIndex()
    {
        return SearchHighestIndex();
    }

    /// <inheritdoc/>
    public PersistableEventData? ReadEventData(int index)
    {
        return PerformRead(index);
    }

    /// <inheritdoc/>
    public List<PersistableEventData> ReadAllEventData()
    {
        return ReadRecordsFromJson();
    }

    /// <inheritdoc/>
    public List<PersistableEventData> SearchEventData(int chartId)
    {
        return PerformSearch(chartId);
    }

    /// <inheritdoc/>
    public List<PersistableEventData> SearchEventData(string partOfDescription)
    {
        return PerformSearch(partOfDescription);
    }

    public int AddEventData(PersistableEventData eventData)
    {
        return PerformInsert(eventData);
    }

    public int AddEventData(PersistableEventData eventData, int idChart)
    {
        int idEvent = PerformInsert(eventData);
        _intersectionDao.Insert(idChart, idEvent);
        return idEvent;
    }


    public bool DeleteEventData(int index)
    {
        return PerformDelete(index);
    }

    private bool CheckDatabaseEvents()
    {
        return File.Exists(dbEventsFullPath);
    }



    private int SearchHighestIndex()
    {
        int index = 0;
        var records = ReadRecordsFromJson();
        foreach (var item in records)
        {
            if (item.Id > index) index = item.Id;
        }
        return index;
    }

    private PersistableEventData? PerformRead(int index)
    {
        var records = ReadRecordsFromJson();
        foreach (var record in records)
        {
            if (record.Id == index) return record;
        }
        return null;
    }

    private List<PersistableEventData> PerformSearch(string partOfdescription)
    {
        List<PersistableEventData> recordsFound = new();
        var records = ReadRecordsFromJson();
        foreach (var record in records)
        {
            if (record.Description.ToLower().Contains(partOfdescription.ToLower())) recordsFound.Add(record);
        }
        return recordsFound;
    }

    private List<PersistableEventData> PerformSearch(int chartId)
    {
        List<PersistableEventData> recordsFound = new();
        var records = ReadRecordsFromJson();
        var intersections = _intersectionDao.ReadAll(); 
        foreach (var record in records)
        {
            foreach (var intersection in intersections)
            {
                if (intersection.ChartId == chartId)
                {
                    var eventFound = PerformRead(intersection.EventId);
                    if (eventFound != null) records.Add(eventFound);
                }
            }
        }
        return recordsFound;
    }

    private int PerformInsert(PersistableEventData eventData)
    {
        List<PersistableEventData> recordsAsList = ReadRecordsFromJson();
        try
        {
            int newIndex = SearchHighestIndex() + 1;
            eventData.Id = newIndex;
            recordsAsList.Add(eventData);
            PersistableEventData[] extendedRecords = recordsAsList.ToArray();
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            var newJson = JsonSerializer.Serialize(extendedRecords, options);
            File.WriteAllText(dbEventsFullPath, newJson);
            return newIndex;
        }
        catch (Exception ex)
        {
            string errorTxt = "ChartEventDao.PerformInsert() using eventData " + eventData + "encountered an exception.";
            Log.Error(errorTxt, ex);
            throw new Exception(errorTxt, ex);
        };
    }


    private bool PerformDelete(int index)
    {
        bool success = false;
        List<PersistableEventData> newRecordSet = new();
        var records = ReadRecordsFromJson();
        foreach (var record in records)
        {
            if (record.Id == index)
            {
                success = true;
                PerformDeleteIntersection(index);       // also remove intersections for this event. 
            }
            else newRecordSet.Add(record);
        }
        try
        {
            PersistableEventData[] newRecords = newRecordSet.ToArray();
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            var newJson = JsonSerializer.Serialize(newRecords, options);
            File.WriteAllText(dbEventsFullPath, newJson);
        }
        catch (Exception ex)
        {
            string errorTxt = "EventDataDao.PerformDelete() using index " + index.ToString() + "encountered an exception.";
            Log.Error(errorTxt, ex);
            throw new Exception(errorTxt, ex);
        };
        return success;
    }

    private bool PerformDeleteIntersection(int eventIndex)
    {
        bool success = false;
        List<InterChartEvent> newRecordSet = new();
        var records = _intersectionDao.ReadAll();
        foreach (var record in records)
        {
            if (record.EventId == eventIndex)
            {
                success = true;
            }
            else newRecordSet.Add(record);
        }
        try
        {
            InterChartEvent[] newRecords = newRecordSet.ToArray();
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            var newJson = JsonSerializer.Serialize(newRecords, options);
            File.WriteAllText(dbEventsFullPath, newJson);
        }
        catch (Exception ex)
        {
            string errorTxt = "EventDataDao.PerformDeleteIntersection() using eventIndex " + eventIndex.ToString() + "encountered an exception.";
            Log.Error(errorTxt, ex);
            throw new Exception(errorTxt, ex);
        };
        return success;
    }


    private List<PersistableEventData> ReadRecordsFromJson()
    {
        List<PersistableEventData> records = new();
        if (CheckDatabaseEvents())
        {
            var json = File.ReadAllText(dbEventsFullPath);
            try
            {
                PersistableEventData[] persistableEventDatas = JsonSerializer.Deserialize<PersistableEventData[]>(json)!;
                records = persistableEventDatas.ToList();
            }
            catch (Exception ex)
            {
                string errorTxt = "EventDataDao.ReadRecordsFromJson() encountered an exception.";
                Log.Error(errorTxt, ex);
                throw new Exception(errorTxt, ex);
            };
        }
        return records;
    }



    private List<PersistableEventData> ReadEventsForChartFromJsn(int chartId)
    {
        List<PersistableEventData> allEvents = ReadRecordsFromJson();
        List<InterChartEvent> allIntersections = _intersectionDao.ReadAll();
        List<PersistableEventData> eventsForChart = new();
        foreach (var eventData in allEvents) {
            foreach (var intersection in allIntersections)
            {
                if (eventData.Id == intersection.EventId && chartId == intersection.ChartId)
                {
                    eventsForChart.Add(eventData);
                }
            }
        }
        return eventsForChart;
    }


}