// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Persistency;
using LiteDB;
using Serilog;

namespace Enigma.Core.Handlers.Persistency.Daos;

/// <inheritdoc />
public sealed class ChartDataDao : IChartDataDao
{
    private readonly string _dbFullPath = ApplicationSettings.Instance.LocationDatabase + EnigmaConstants.DATABASE_NAME;
    private const string Collection = "charts";
    
    /// <inheritdoc />
    public int CountRecords()
    {
        List<PersistableChartData> records = ReadRecordsFromDatabase();
        return records.Count;
    }

    /// <inheritdoc />
    public int HighestIndex()
    {
        return SearchHighestIndex();
    }

    /// <inheritdoc />
    public PersistableChartData? ReadChartData(int index)
    {
        return PerformRead(index);
    }

    /// <inheritdoc />
    public List<PersistableChartData>? SearchChartData(string? partOfName)
    {
        return PerformSearch(partOfName);
    }

    /// <inheritdoc />
    public List<PersistableChartData> ReadAllChartData()
    {
        return ReadRecordsFromDatabase();
    }

    /// <inheritdoc />
    public int AddChartData(PersistableChartData chartData)
    {
        return PerformInsert(chartData);
    }

    /// <inheritdoc />
    public bool DeleteChartData(int index)
    {
        return PerformDelete(index);
    }

    private int SearchHighestIndex()
    {
        List<PersistableChartData> records = ReadRecordsFromDatabase();
        return records.Select(item => item.Id).Prepend(0).Max();
    }

    private PersistableChartData? PerformRead(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(Collection);
        col.EnsureIndex(x => x.Id);
        PersistableChartData? result = col.FindOne(x => x.Id.Equals(index));
        return result;
    }

    private List<PersistableChartData>? PerformSearch(string? partOfName)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(Collection);
        List<PersistableChartData> records = col.Query()
            .Where(x => partOfName != null && x.Name.ToUpper().Contains(partOfName.ToUpper()))
            .OrderBy(x => x.Name)
            .Limit(100)
            .ToList();
        return records;
    }

    private int PerformInsert(PersistableChartData chartData)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(Collection);
        try
        {
            col.Insert(chartData);
            Log.Information("Inserted chart {Chart}", chartData.Id);
        }
        catch (Exception e)
        {
            Log.Error(
                "ChartDataDao.PerformInsert: trying to insert chart with existing id {Id} results in exception {Ex}",
                chartData.Id, e.Message);
            return 0;
        }

        return chartData.Id;
    }

    private bool PerformDelete(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(Collection);
        bool result = col.Delete(index);
        Log.Information("Deleted chart {Chart}, success {Success}", index, result);
        return result;
    }

    private List<PersistableChartData> ReadRecordsFromDatabase()
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(Collection);
        List<PersistableChartData> records = col.FindAll().ToList();
        return records;
    }
}