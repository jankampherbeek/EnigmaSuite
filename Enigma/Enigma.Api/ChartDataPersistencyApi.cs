// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Persistency;
using Enigma.Domain.Persistables;
using Serilog;

namespace Enigma.Api;

/// <summary>AI for persistency ChartData.</summary>
public interface IChartDataPersistencyApi
{
    /// <summary>Add a record.</summary>
    /// <param name="chartData">The record to insert.</param>
    /// <returns>The index for the inserted record, -1 if the record could not be inserted.</returns>
    public long AddChartData(PersistableChartData chartData);

    /// <summary>Dele a record.</summary>
    /// <param name="index">Id of the record to delete.</param>
    /// <returns>True if the record was deleted, false if the record does not exist.</returns>
    public bool DeleteChartData(long index);

    /// <summary>Read a specific record.</summary>
    /// <param name="index">The unique index for the record.</param>
    /// <returns>If found: the record. Otherwise: null.</returns>
    public PersistableChartData? ReadChartData(long index);    
    
    /// <summary>Read all records.</summary>
    /// <returns>List with zero or more results.</returns>
    public List<PersistableChartIdentification>? ReadAllChartData();
    
    /// <summary>Calculate number of records in Json file.</summary>
    /// <returns>The number of records</returns>
    public long NumberOfRecords();

    /// <summary>Define the highest index that is currently in use.</summary>
    /// <returns>The highest index.</returns>
    public long HighestIndex();



    /// <summary>Read records that correspond (partly) with a given searchterm for the name.</summary>
    /// <param name="partOfName">The search term.</param>
    /// <returns>List with zero or more results.</returns>
    public List<PersistableChartIdentification>? SearchChartData(string? partOfName);




}



/// <inheritdoc/>
public class ChartDataPersistencyApi : IChartDataPersistencyApi
{
    private readonly IChartDataDao _chartDataDao;

    public ChartDataPersistencyApi(IChartDataDao chartDataDao)
    {
        _chartDataDao = chartDataDao;
    }
    
    /// <inheritdoc/>
    public long AddChartData(PersistableChartData chartData)
    { 
        Log.Information("ChartDataPersistencyApi.AddChartData() requested using name: {Name}", chartData.Identification.Name);
        return _chartDataDao.AddChartData(chartData);
    }
    
    /// <inheritdoc/>
    public bool DeleteChartData(long index)
    {
        Log.Information("ChartDataPersistencyApi.DeleteChartData() requested for index: {Index}", index);
        return _chartDataDao.DeleteChartData(index);
    }
    
    /// <inheritdoc/>
    public PersistableChartData? ReadChartData(long index)
    {
        Log.Information("ChartDataPersistencyApi.ReadChartData() requested for index : {Index}", index);
        return _chartDataDao.ReadChartData(index);
    }
    
    
    /// <inheritdoc/>
    public List<PersistableChartIdentification>? ReadAllChartData()
    {
        Log.Information("ChartDataPersistencyApi.readAllChartData() requested");
        return _chartDataDao.ReadAllChartIdentifications();
    }
    
    
    
    /// <inheritdoc/>
    public long NumberOfRecords()
    {
        Log.Information("ChartDataPersistencyApi.NumberOfRecords() requested");
        return _chartDataDao.CountRecords();
    }

    /// <inheritdoc/>
    public long HighestIndex()
    {
        Log.Information("ChartDataPersistencyApi.HighestIndex() requested");
        return _chartDataDao.HighestIndex();
    }



    /// <inheritdoc/>
    public List<PersistableChartIdentification>? SearchChartData(string? partOfName)
    {
        Guard.Against.NullOrEmpty(partOfName);
        Log.Information("ChartDataPersistencyApi.SearchChartData() requested with argument {PartOfName}", partOfName);
        return _chartDataDao.SearchChartData(partOfName);
    }




    
}