// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Persistency;
using Enigma.Domain.Persistables;
using Serilog;

namespace Enigma.Api;




/// <summary>AI for persistency Event data.</summary>
public interface IEventDataPersistencyApi
{

    /// <summary>Read a specific record.</summary>
    /// <param name="index">The unique index for the record.</param>
    /// <returns>If found: the record. Otherwise: null.</returns>
    public PersistableEventData? ReadEventData(long index);

    /// <summary>Read records of events that have an intersection with a given chart.</summary>
    /// <param name="chartId">Id of the chart.</param>
    /// <returns>List with zero or more results.</returns>
    public List<PersistableEventData> SearchEventData(long chartId);

    /// <summary>Add a record and an intersection.</summary>
    /// <param name="eventData">The record to insert.</param>
    /// <param name="chartId">Id of the chart that will be coupled to the event.</param>
    /// <returns>The index for the inserted record, -1 if the record could not be inserted.</returns>
    public long AddEventData(PersistableEventData eventData, long chartId);
    
    /// <summary>Dele a record for an event.</summary>
    /// <remarks>Also deletes entries for this event in the intersections.</remarks>
    /// <param name="index">Id of the record to delete.</param>
    /// <returns>True if the record was deleted, false if the record does not exist.</returns>
    public bool DeleteEventData(long index);
}





/// <inheritdoc/>
public sealed class EventDataPersistencyApi : IEventDataPersistencyApi
{

    private readonly IEventDataDao _eventDataDao;

    public EventDataPersistencyApi(IEventDataDao eventDataDao)
    {
        _eventDataDao = eventDataDao;
    }

    /// <inheritdoc/>
    public PersistableEventData? ReadEventData(long index)
    {
        Log.Information("EventDataPersistencyApi.ReadEventData() for id {Id} requested", index);
        return _eventDataDao.ReadEventData(index);
    }

    /// <inheritdoc/>
    public List<PersistableEventData> SearchEventData(long chartId)
    {
        Log.Information("EventDataPersistencyApi.SearchEventData() for chartId {Id} requested", chartId);
        return _eventDataDao.SearchEventData(chartId);
    }

    /// <inheritdoc/>
    public long AddEventData(PersistableEventData eventData, long chartId)
    {
        Guard.Against.Null(eventData);
        Log.Information(
            "EventDataPersistencyApi.AddEventData() for eventData with id {EventData} and chartId {ChartId} requested", 
            eventData.Id, chartId);
        return _eventDataDao.AddEventData(eventData, chartId);
    }

    /// <inheritdoc/>
    public bool DeleteEventData(long index)
    {
        Log.Information("EventDataPersistencyApi.DeleteEventData() for index {Index} requested", index);
        return _eventDataDao.DeleteEventData(index);
    }
    
}
