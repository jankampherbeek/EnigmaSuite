// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Persistency;
using LiteDB;
using Serilog;

namespace Enigma.Core.Persistency.Daos;

/// <inheritdoc />
public class PeriodDataDao: IPeriodDataDao
{
    private const string COL_INTERSECTION = "chartperiods";
    private const string COL_PERIODS = "periods";
    private readonly string _dbFullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.DATABASE_NAME;
    private readonly IInterChartPeriodDao _interChartPeriodDao;

    public PeriodDataDao(IInterChartPeriodDao interChartPeriodDao)
    {
        _interChartPeriodDao = interChartPeriodDao;
    }
    
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
    public int AddPeriodData(PersistablePeriodData periodData, int idChart)
    {
        int idPeriod = PerformInsert(periodData);
        int newId = HighestIndex();
        _interChartPeriodDao.Insert(idChart, newId);
        return idPeriod;
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
        IEnumerable<InterChartPeriod> intersections = _interChartPeriodDao.Read(chartId);
        using var db = new LiteDatabase(_dbFullPath);
        var col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        col.EnsureIndex(x => x.Id);
        foreach (var intersection in intersections)
        {
            int periodId = intersection.PeriodId;
            var periodItems = col.Query()
                .Where(x => x.Id == periodId)
                .OrderBy(x => x.Id)
                .Select(x => new { x.Description, x.StartJulianDayEt, x.EndJulianDayEt, 
                    x.StartDateText, x.EndDateText, x.Id })
                .Limit(100)
                .ToList();
            
            
            //    public PersistablePeriodData(string description, double startJd, double endJd, string startDateText, string endDateText, int id = 0)
            foreach (var periodItem in periodItems)
            {
                PersistablePeriodData periodData = new(periodItem.Description, periodItem.StartJulianDayEt, 
                    periodItem.EndJulianDayEt, periodItem.StartDateText, periodItem.EndDateText, periodItem.Id);
                allRecords.Add(periodData);
            }
        }
        return allRecords;
    }

    private int PerformInsert(PersistablePeriodData periodData)
    {
        int idNewPeriod = 0;
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistablePeriodData>? col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        try
        {
            col.Insert(periodData);
            idNewPeriod = HighestIndex();
            Log.Information("Inserted period {IdPeriod}", idNewPeriod);
        }
        catch (Exception e)
        {
            Log.Error(
                "PeriodDataDao.PerformInsert: trying to insert period results in exception {Ex}", e.Message);
            idNewPeriod = 0;
        }
        return idNewPeriod;
    }


    private bool PerformDelete(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<PersistablePeriodData>? col = db.GetCollection<PersistablePeriodData>(COL_PERIODS);
        ILiteCollection<InterChartPeriod>? colIntersections = db.GetCollection<InterChartPeriod>(COL_INTERSECTION);
        bool result = col.Delete(index);
        int deletedCount = 0;
        if (result) deletedCount = colIntersections.DeleteMany(x => x.ChartId.Equals(index));
        Log.Information("Deleted period {Period} and related {ICount} intersections, success {Success}", index,
            deletedCount, result);
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
        List<InterChartPeriod> allIntersections = _interChartPeriodDao.ReadAll();

        return (from periodData in allPeriods
            from intersection in allIntersections
            where periodData.Id == intersection.PeriodId && chartId == intersection.ChartId
            select periodData).ToList();
    }
    
    
}