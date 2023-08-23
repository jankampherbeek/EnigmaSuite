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
public class PeriodDataDao: IPeriodDataDao
{
    
    // TODO add code to handle intersections
    
    private const string COL_PERIODS = "periods";
    private readonly string _dbFullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.DatabaseName;
    
    
    
    /// <inheritdoc />
    public int CountRecords()
    {
        return ReadRecordsFromDatabase().Count;
    }

    /// <inheritdoc />
    public int CountRecords(int chartId)
    {
        return ReadPeriodsForChartFromDatabase(chartId).Count;
    }

    /// <inheritdoc />
    public int HighestIndex()
    {
        return SearchHighestIndex();
    }

    /// <inheritdoc />
    public PersistablePeriodData? ReadPeriodData(int index)
    {
        return PerformRead(index);
    }

    /// <inheritdoc />
    public List<PersistablePeriodData> SearchPeriodData(int chartId)
    {
        return PerformSearch(chartId);
    }

    /// <inheritdoc />
    public List<PersistablePeriodData> SearchPeriodData(string? partOfDescription)
    {
        return PerformSearch(partOfDescription);
    }

    /// <inheritdoc />
    public List<PersistablePeriodData> ReadAllPeriodData()
    {
        return ReadRecordsFromDatabase();
    }

    /// <inheritdoc />
    public int AddPeriodData(PersistablePeriodData periodData)
    {
        return PerformInsert(periodData);
    }

    /// <inheritdoc />
    public bool DeletePeriodData(int index)
    {
        return PerformDelete(index);
    }
    
    
    private int SearchHighestIndex()
    {
        List<PersistablePeriodData> records = ReadRecordsFromDatabase();
        return records.Select(item => item.Id).Prepend(0).Max();
    }

    private PersistablePeriodData? PerformRead(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistablePeriodData> col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        col.EnsureIndex(x => x.Id);
        PersistablePeriodData? result = col.FindOne(x => x.Id.Equals(index));
        return result;
    }

    private List<PersistablePeriodData> PerformSearch(string? partOfDescription)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistablePeriodData>? col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        List<PersistablePeriodData> records = col.Query()
            .Where(x => partOfDescription != null && x.Description.ToUpper().Contains(partOfDescription.ToUpper()))
            .OrderBy(x => x.StartJulianDayEt)
            .Limit(100)
            .ToList();
        return records;
    }

    private List<PersistablePeriodData> PerformSearch(int chartId)
    {
        List<PersistablePeriodData> allRecords = new();
        // TODO apply intersections
        /*
        IEnumerable<InterChartPeriod> intersections = _intersectionDao.Read(chartId);
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistablePeriodData>? col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        foreach (List<PersistablePeriodData> records in intersections.Select(intersection => col.Query()
                     .Where(x => x.Id.Equals(intersection.ChartId))
                     .OrderBy(x => x.StartJulianDayEt)
                     .Limit(100)
                     .ToList()))
            allRecords.AddRange(records);
            */

        return allRecords;
    }

    private int PerformInsert(PersistablePeriodData periodData)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistablePeriodData>? col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        try
        {
            col.Insert(periodData);
            Log.Information("Inserted period {Period}", periodData.Id);
        }
        catch (Exception e)
        {
            Log.Error(
                "PeriodDataDao.PerformInsert: trying to insert period with existing id {Id} results in exception {Ex}",
                periodData.Id, e.Message);
            return 0;
        }
        return periodData.Id;
    }


    private bool PerformDelete(int index)
    {
        bool result = true;  // todo apply intersection    
        /*using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistablePeriodData>? col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        ILiteCollection<InterChartPeriod>? colIntersections = db.GetCollection<InterChartPeriod>(COL_INTERSECTION);
        bool result = col.Delete(index);
        int deletedCount = 0;
        if (result) deletedCount = colIntersections.DeleteMany(x => x.ChartId.Equals(index));
        Log.Information("Deleted period {Period} and related {ICount} intersections, success {Success}", index,
            deletedCount, result);*/
        return result;
    }


    private List<PersistablePeriodData> ReadRecordsFromDatabase()
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistablePeriodData>? col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        List<PersistablePeriodData> records = col.FindAll().ToList();
        return records;
    }


    private List<PersistablePeriodData> ReadPeriodsForChartFromDatabase(int chartId)
    {
        List<PersistablePeriodData> allPeriods = ReadRecordsFromDatabase();
        return allPeriods;    // TODO apply intersection
        /*List<InterChartPeriod> allIntersections = _intersectionDao.ReadAll();

        return (from periodData in allPeriods
            from intersection in allIntersections
            where periodData.Id == intersection.PeriodId && chartId == intersection.ChartId
            select periodData).ToList();*/
    }
    
    
}