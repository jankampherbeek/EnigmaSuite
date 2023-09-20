using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using LiteDB;
using Serilog;

namespace Enigma.Core.Persistency.Daos;

/// <inheritdoc/>
public sealed class InterChartPeriodDao: IInterChartPeriodDao
{
    private readonly string _dbFullPath = ApplicationSettings.LocationDatabase + EnigmaConstants.DATABASE_NAME;
    private const string COLLECTION = "chartperiods";

    /// <inheritdoc/>
    public void Insert(int chartId, int periodId)
    {
        PerformInsert(chartId, periodId);
    }

    /// <inheritdoc/>
    public List<InterChartPeriod> ReadAll()
    {
        return ReadRecordsFromDatabase();
    }

    /// <inheritdoc/>
    public IEnumerable<InterChartPeriod> Read(int chartId)
    {
        return ReadRecordsForChart(chartId);
    }

    /// <inheritdoc/>
    public bool Delete(int chartId)
    {
        return PerformDelete(chartId);
    }
    
    private List<InterChartPeriod> ReadRecordsFromDatabase()
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<InterChartPeriod>? col = db.GetCollection<InterChartPeriod>(COLLECTION);
        List<InterChartPeriod> records = col.FindAll().ToList();
        return records;
    }

    private IEnumerable<InterChartPeriod> ReadRecordsForChart(int chartId)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<InterChartPeriod>? col = db.GetCollection<InterChartPeriod>(COLLECTION);
        List<InterChartPeriod> records = col.Find(x => x.ChartId.Equals(chartId)).ToList();
        return records;
    }
    
    private void PerformInsert(int chartId, int periodId)
    {
        var chartPeriod = new InterChartPeriod(chartId, periodId);
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<InterChartPeriod>? col = db.GetCollection<InterChartPeriod>(COLLECTION);
        try
        {
            col.Insert(chartPeriod);
            Log.Information("Inserted chartPeriod for chart {Chart} and period {Period}",
                chartPeriod.ChartId, chartPeriod.PeriodId);
        }
        catch (Exception e)
        {
            Log.Error(
                "ChartDataDao.PerformInsert: trying to insert chartPeriod for chart {Chart} and period {Period} results in exception {Ex}",
                chartPeriod.ChartId, chartPeriod.PeriodId, e.Message);
        }
    }
    
    private bool PerformDelete(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<InterChartPeriod>? col = db.GetCollection<InterChartPeriod>(COLLECTION);
        bool result = col.Delete(index);
        Log.Information("Deleted periodChart for id {Id}, success {Success}", index, result);
        return result;
    }
}
