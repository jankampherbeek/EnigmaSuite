// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Persistables;
using Serilog;

namespace Enigma.Api.Persistency;

/// <inheritdoc/>
public class PeriodDataPersistencyApi: IPeriodDataPersistencyApi
{
    private readonly IPeriodDataDao _periodDataDao;

    public PeriodDataPersistencyApi(IPeriodDataDao periodDataDao)
    {
        _periodDataDao = periodDataDao;
    }
    
    /// <inheritdoc/>
    public int NumberOfRecords()
    {
        Log.Information("PeriodDataPersistencyApi.NumberOfRecords() requested");
        return _periodDataDao.CountRecords();
    }

    /// <inheritdoc/>
    public int NumberOfRecords(int chartId)
    {
        Log.Information("PeriodDataPersistencyApi.NumberOfRecords() for chart with id {Id} requested", chartId);
        return _periodDataDao.CountRecords(chartId);
    }

    /// <inheritdoc/>
    public int HighestIndex()
    {
        Log.Information("PeriodDataPersistencyApi.HighestIndex() requested");
        return _periodDataDao.HighestIndex();
    }

    /// <inheritdoc/>
    public PersistablePeriodData? ReadPeriodData(int index)
    {
        Log.Information("PeriodDataPersistencyApi.ReadPeriodData() for id {Id} requested", index);
        return _periodDataDao.ReadPeriodData(index);
    }

    /// <inheritdoc/>
    public List<PersistablePeriodData> SearchPeriodData(string? partOfDescription)
    {
        Guard.Against.NullOrEmpty(partOfDescription);
        Log.Information("PeriodDataPersistencyApi.SearchPeriodData() for searchterm {Term} requested", 
            partOfDescription);
        return _periodDataDao.SearchPeriodData(partOfDescription);
    }

    /// <inheritdoc/>
    public List<PersistablePeriodData> SearchPeriodData(int chartId)
    {
        Log.Information("PeriodDataPersistencyApi.SearchPeriodData() for chartId {Id} requested", chartId);
        return _periodDataDao.SearchPeriodData(chartId);
    }

    /// <inheritdoc/>
    public List<PersistablePeriodData> ReadAllPeriodData()
    {
        Log.Information("PeriodDataPersistencyApi.ReadAllPeriodData() requested");
        return _periodDataDao.ReadAllPeriodData();
    }

    /// <inheritdoc/>
    public int AddPeriodData(PersistablePeriodData periodData)
    {
        Guard.Against.Null(periodData);
        Log.Information("PeriodDataPersistencyApi.AddPeriodData() for periodData with id {PeriodData} requested", periodData.Id);
        return _periodDataDao.AddPeriodData(periodData);
    }

    /// <inheritdoc/>
    public int AddPeriodData(PersistablePeriodData periodData, int chartId)
    {
        Guard.Against.Null(periodData);
        Log.Information(
            "PeriodDataPersistencyApi.AddPeriodData() for periodData with id {PeriodData} and chartId {ChartId} requested", 
            periodData.Id, chartId);
        return _periodDataDao.AddPeriodData(periodData, chartId);
    }

    /// <inheritdoc/>
    public bool DeletePeriodData(int index)
    {
        Log.Information("PeriodDataPersistencyApi.DeletePeriodData() for index {Index} requested", index);
        return _periodDataDao.DeletePeriodData(index);
    }
}
