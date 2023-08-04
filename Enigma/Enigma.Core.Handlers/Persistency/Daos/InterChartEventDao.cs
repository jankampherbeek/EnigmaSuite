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
public sealed class InterChartEventDao : IInterChartEventDao
{
    private readonly string _dbFullPath = ApplicationSettings.Instance.LocationDatabase + EnigmaConstants.DATABASE_NAME;
    private const string Collection = "chartevents";

    /// <inheritdoc />
    public void Insert(int chartId, int eventId)
    {
        PerformInsert(chartId, eventId);
    }

    /// <inheritdoc />
    public List<InterChartEvent> ReadAll()
    {
        return ReadRecordsFromDatabase();
    }

    public List<InterChartEvent> Read(int chartId)
    {
        return ReadRecordsForChart(chartId);
    }

    public bool Delete(int chartId)
    {
        return PerformDelete(chartId);
    }


    private List<InterChartEvent> ReadRecordsFromDatabase()
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<InterChartEvent>? col = db.GetCollection<InterChartEvent>(Collection);
        List<InterChartEvent> records = col.FindAll().ToList();
        return records;
    }

    private List<InterChartEvent> ReadRecordsForChart(int chartId)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<InterChartEvent>? col = db.GetCollection<InterChartEvent>(Collection);
        List<InterChartEvent> records = col.Find(x => x.ChartId.Equals(chartId)).ToList();
        return records;
    }

    private void PerformInsert(int chartId, int eventId)
    {
        var chartEvent = new InterChartEvent(chartId, eventId);
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<InterChartEvent>? col = db.GetCollection<InterChartEvent>(Collection);
        try
        {
            col.Insert(chartEvent);
            Log.Information("Inserted chartEvent for chart {Chart} and event {Event}",
                chartEvent.ChartId, chartEvent.EventId);
        }
        catch (Exception e)
        {
            Log.Error(
                "ChartDataDao.PerformInsert: trying to insert chartEvent for chart {Chart} and event {Event} results in exception {Ex}",
                chartEvent.ChartId, chartEvent.EventId, e.Message);
        }
    }


    private bool PerformDelete(int index)
    {
        using var db = new LiteDatabase(_dbFullPath);
        ILiteCollection<InterChartEvent>? col = db.GetCollection<InterChartEvent>(Collection);
        bool result = col.Delete(index);
        Log.Information("Deleted eventChart for id {Id}, success {Success}", index, result);
        return result;
    }
}