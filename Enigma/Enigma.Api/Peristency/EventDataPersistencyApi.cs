// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Persistency;
using Serilog;


namespace Enigma.Api.Peristency;

/// <inheritdoc/>
public sealed class EventDataPersistencyApi : IEventDataPersistencyApi
{

    private readonly IEventDataDao _eventDataDao;

    public EventDataPersistencyApi(IEventDataDao eventDataDao)
    {
        _eventDataDao = eventDataDao;
    }

    /// <inheritdoc/>
    public int NumberOfRecords()
    {
        Log.Information("EventDataPersistencyApi.NumberOfRecords() requested.");
        return _eventDataDao.CountRecords();
    }

    /// <inheritdoc/>
    public int NumberOfRecords(int chartId)
    {
        Log.Information("EventDataPersistencyApi.NumberOfRecords() for chart with id " + chartId.ToString() + " requested.");
        return _eventDataDao.CountRecords(chartId);
    }

    /// <inheritdoc/>
    public int HighestIndex()
    {
        Log.Information("EventDataPersistencyApi.HighestIndex() requested.");
        return _eventDataDao.HighestIndex();
    }

    /// <inheritdoc/>
    public List<PersistableEventData> ReadAllEventData()
    {
        Log.Information("EventDataPersistencyApi.ReadAllEventData() requested.");
        return _eventDataDao.ReadAllEventData();
    }

    /// <inheritdoc/>
    public PersistableEventData? ReadEventData(int index)
    {
        Log.Information("EventDataPersistencyApi.ReadEventData() for id " + index.ToString() + " requested.");
        return _eventDataDao.ReadEventData(index);
    }

    /// <inheritdoc/>
    public List<PersistableEventData> SearchEventData(string partOfDescription)
    {
        Guard.Against.NullOrEmpty(partOfDescription);
        Log.Information("EventDataPersistencyApi.SearchEventData() for searchterm " + partOfDescription + " requested.");
        return _eventDataDao.SearchEventData(partOfDescription);
    }

    /// <inheritdoc/>
    public List<PersistableEventData> SearchEventData(int chartId)
    {
        Log.Information("EventDataPersistencyApi.SearchEventData() for chartId " + chartId.ToString() + " requested.");
        return _eventDataDao.SearchEventData(chartId);
    }

    /// <inheritdoc/>
    public int AddEventData(PersistableEventData eventData)
    {
        Guard.Against.Null(eventData);
        Log.Information("EventDataPersistencyApi.AddEventData() for eventData " + eventData + " requested.");
        return _eventDataDao.AddEventData(eventData);
    }

    /// <inheritdoc/>
    public bool DeleteEventData(int index)
    {
        Log.Information("EventDataPersistencyApi.DeleteEventData() for index " + index.ToString() + " requested.");
        return _eventDataDao.DeleteEventData(index);
    }






}
