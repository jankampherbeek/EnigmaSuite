﻿// Enigma Astrology Research.
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
public sealed class ChartDataDao : IChartDataDao
{
    private readonly string _dbFullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.DATABASE_NAME;
    private const string COLLECTION = "charts";
    
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
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(COLLECTION);
        col.EnsureIndex(x => x.Id);
        PersistableChartData? result = col.FindOne(x => x.Id.Equals(index));
        return result;
    }

    private List<PersistableChartData>? PerformSearch(string? partOfName)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(COLLECTION);
        List<PersistableChartData> records = col.Query()
            .Where(x => partOfName != null && x.Name.ToUpper().Contains(partOfName.ToUpper()))
            .OrderBy(x => x.Name)
            .Limit(100)
            .ToList();
        return records;
    }

    private int PerformInsert(PersistableChartData chartData)
    {
        int idNewChart = 0;
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(COLLECTION);
        try
        {
            col.Insert(chartData);
            idNewChart = HighestIndex();
            Log.Information("Inserted chart {Chart}", idNewChart);
        }
        catch (Exception e)
        {
            Log.Error(
                "ChartDataDao.PerformInsert: trying to insert chart results in exception {Ex}", e.Message);
            idNewChart = 0;
        }

        return idNewChart;
    }

    private bool PerformDelete(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(COLLECTION);
        bool result = col.Delete(index);
        Log.Information("Deleted chart {Chart}, success {Success}", index, result);
        return result;
    }

    private List<PersistableChartData> ReadRecordsFromDatabase()
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistableChartData>? col = db.GetCollection<PersistableChartData>(COLLECTION);
        List<PersistableChartData> records = col.FindAll().ToList();
        return records;
    }
}