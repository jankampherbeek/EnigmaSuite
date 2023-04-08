// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Persistency;
using Serilog;

namespace Enigma.Api.Persistency;

/// <inheritdoc/>
public class ChartDataPersistencyApi : IChartDataPersistencyApi
{
    private readonly IChartDataDao _chartDataDao;

    public ChartDataPersistencyApi(IChartDataDao chartDataDao)
    {
        _chartDataDao = chartDataDao;
    }

    /// <inheritdoc/>
    public int NumberOfRecords()
    {
        Log.Information("ChartDataPersistencyApi.NumberOfRecords() requested.");
        return _chartDataDao.CountRecords();
    }

    /// <inheritdoc/>
    public int HighestIndex()
    {
        Log.Information("ChartDataPersistencyApi.HighestIndex() requested.");
        return _chartDataDao.HighestIndex();
    }

    /// <inheritdoc/>
    public PersistableChartData? ReadChartData(int index)
    {
        Log.Information("ChartDataPersistencyApi.ReadChartData() requested for index : " + index.ToString());
        return _chartDataDao.ReadChartData(index);
    }

    /// <inheritdoc/>
    public List<PersistableChartData> SearchChartData(string partOfName)
    {
        Guard.Against.NullOrEmpty(partOfName);
        Log.Information("ChartDataPersistencyApi.SearchChartData() requested with argument : " + partOfName);
        return _chartDataDao.SearchChartData(partOfName);
    }

    /// <inheritdoc/>
    public List<PersistableChartData> ReadAllChartData()
    {
        Log.Information("ChartDataPersistencyApi.readAllChartData() requested.");
        return _chartDataDao.ReadAllChartData();
    }

    /// <inheritdoc/>
    public int AddChartData(PersistableChartData chartData)
    {
        Guard.Against.Null(chartData);
        Log.Information("ChartDataPersistencyApi.AddChartData() requested using name: " + chartData.Name);
        return _chartDataDao.AddChartData(chartData);
    }

    /// <inheritdoc/>
    public bool DeleteChartData(int index)
    {
        Log.Information("ChartDataPersistencyApi.DeleteChartData() requested for index: " + index.ToString());
        return _chartDataDao.DeleteChartData(index);
    }


}