// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Data;
using System.Data.SQLite;
using Dapper;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Serilog;

namespace Enigma.Core.Persistency;


/// <summary>DAO for intersection between chart and event.</summary>
/// <remarks>Should only accessed from other DAO's.</remarks>
public interface IChartsEventsDao
{
    /// <summary>Insert intersection.</summary>
    /// <param name="chartId">Id for the chart.</param>
    /// <param name="eventId">Id for the event.</param>
    public void Insert(long chartId, long eventId);

    /// <summary>Delete all intersections for a specific chart.</summary>
    /// <param name="chartId"></param>
    /// <returns>True if delete was successful.</returns>
    public bool Delete(long chartId);
    
    /// <summary>Read all interesections.</summary>
    /// <returns>List with all intersections.</returns>
    public List<InterChartEvent> ReadAll();

    /// <summary>Read all intersections for a specific chart.</summary>
    /// <param name="chartId"></param>
    /// <returns>Lidt with intersections for the chart..</returns>
    public IEnumerable<InterChartEvent> Read(long chartId);
}




/// <inheritdoc />
public sealed class ChartsEventsDao : IChartsEventsDao
{
    private readonly string _fullPath = "Data Source=" + ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;

    /// <inheritdoc />
    public void Insert(long chartId, long eventId)
    {
        PerformInsert(chartId, eventId);
    }

    /// <inheritdoc />
    public List<InterChartEvent> ReadAll()
    {
        return ReadAllRecords();
    }

    public IEnumerable<InterChartEvent> Read(long chartId)
    {
        return ReadRecordsForChart(chartId);
    }

    public bool Delete(long chartId)
    {
        return PerformDelete(chartId);
    }

    private void PerformInsert(long chartId, long eventId)
    {
        SQLiteConnection dbConnection = new(_fullPath);
        long newIndex = -1;
        const string sql = "INSERT INTO Chartsevents (chartId, eventId) VALUES (@chart, @event);";
        var dp = new DynamicParameters();
        dp.Add("@chart", chartId, DbType.Int64, ParameterDirection.Input);
        dp.Add("@event", eventId, DbType.Int64, ParameterDirection.Input);
        try
        {
            using var cnn = dbConnection;
            cnn.Open();
            cnn.Query(sql, dp);
        }
        catch (Exception e)
        {
            Log.Error(
                "ChartsEventsDao.PerformInsert: trying to insert chartEvent for chart {Chart} and " +
                "event {Event} results in exception {Ex}", chartId, eventId, e.Message);
        }
    }
    
  
    
    private bool PerformDelete(long chartIndex)
    {
        SQLiteConnection dbConnection = new(_fullPath);
        bool result = false;
        const string sql = "DELETE FROM ChartsEvents WHERE chartId = @chartId;";
        var dp = new DynamicParameters();
        dp.Add("@chartId", chartIndex, DbType.Int64, ParameterDirection.Input);
        try
        {
            using var cnn = dbConnection;
            cnn.Open();
            cnn.Execute(sql, dp);
            result = true;
        }
        catch (Exception e)
        {
            Log.Error("ChartsEventsDao.PerformDelete: delete events for chartid {ChartId} results in " +
                      "Exception: {Msg}", chartIndex, e.Message);
        }
        return result;
    }
    
    
    private List<InterChartEvent>? ReadRecordsForChart(long index)
    {
        List<InterChartEvent> records  = new();
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            const string sqlChart = "SELECT * FROM CHARTSEVENTS WHERE chartId = @Id";
            var dp = new DynamicParameters();
            dp.Add("@Id", index);
            using var cnn = dbConnection;
            cnn.Open();
            records = cnn.Query<InterChartEvent>(sqlChart, dp).ToList();
        }
        catch (Exception e)
        {
            Log.Error("ChartsEventsDao.ReadRecordsForCharts. Exception when reading events for index {Id}. " +
                      "Exception msg: {Msg}", index, e.Message);
        }
        return records;
    }



    private List<InterChartEvent> ReadAllRecords()
    {
        List<InterChartEvent> records = new();
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            const string sqlChart = "SELECT * FROM CHARTSEVENTS;";
            using var cnn = dbConnection;
            cnn.Open();
            records = cnn.Query<InterChartEvent>(sqlChart).ToList();
        }
        catch (Exception e)
        {
            Log.Error("ChartsEventsDao.ReadALlRecords. Exception when reading all events. " +
                      "Exception msg: {Msg}", e.Message);
        }

        return records;
    }

}