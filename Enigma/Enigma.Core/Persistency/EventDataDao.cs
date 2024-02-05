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


/// <summary>DAO for event data.</summary>
public interface IEventDataDao
{
 /// <summary>Insert a new record and an intersection.</summary>
    /// <param name="eventData">The record to insert.</param>
    /// <param name="chartId">The id of an existing saved chart.</param>
    /// <returns>The id for the inserted record or -1 if the insert could not be fullfilled.</returns>
    public long AddEventData(PersistableEventData eventData, long chartId);
    

    /// <summary>Delete a record for an event and any intersection records that refer to this event.</summary>
    /// <param name="index">The index of the record to delete.</param>
    /// <returns>True if the record was deleted, false if the record was not found.</returns>
    public bool DeleteEventData(long index);    

    /// <summary>Read event data for a given index.</summary>
    /// <param name="index">The index to check.</param>
    /// <returns>If found: the record that corresponds to the given index, otherwise null.</returns>
    public PersistableEventData? ReadEventData(long index);

    /// <summary>Read event data that are connected to a specific chart.</summary>
    /// <param name="chartId">Id of hte chart.</param>
    /// <returns>List with zero or more records that are found.</returns>
    public List<PersistableEventData> SearchEventData(long chartId);
}



/// <inheritdoc />
public sealed class EventDataDao : IEventDataDao
{
    private readonly string _fullPath = "Data Source=" + ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
    private readonly IChartsEventsDao _chartsEventsDao;
    
    public EventDataDao(IChartsEventsDao chartsEventsDao)
    {
        _chartsEventsDao = chartsEventsDao;
    }

    /// <inheritdoc />
    public long AddEventData(PersistableEventData eventData, long idChart)
    {
        long eventIndex = PerformInsert(eventData);
        _chartsEventsDao.Insert(idChart, eventIndex);
        return eventIndex;
    }

    /// <inheritdoc />
    public bool DeleteEventData(long index)
    {
        return PerformDelete(index);
    }
    
    /// <inheritdoc />
    public PersistableEventData? ReadEventData(long index)
    {
        return PerformRead(index);
    }

    /// <inheritdoc />
    public List<PersistableEventData> SearchEventData(long chartId)
    {
        return PerformSearch(chartId);
    }

    
    
    private long PerformInsert(PersistableEventData eventData)
    {
        SQLiteConnection dbConnection = new(_fullPath);
        long newIndex = -1;
        const string sql = """
                           INSERT INTO Events (description, locationName, geoLong, geoLat, dateText, timeText, jdForEt)
                           VALUES (@Description, @LocationName, @GeoLong, @GeoLat, @DateText, @TimeText, @JdForEt);
                           """;
        var dpChart = new DynamicParameters();
        dpChart.Add("@Description", eventData.Description, DbType.AnsiString, ParameterDirection.Input);
        dpChart.Add("@LocationName", eventData.LocationName, DbType.AnsiString, ParameterDirection.Input);
        dpChart.Add("@GeoLong", eventData.GeoLong, DbType.Double, ParameterDirection.Input);
        dpChart.Add("@GeoLat", eventData.GeoLat, DbType.Double, ParameterDirection.Input);
        dpChart.Add("@DateText", eventData.DateText, DbType.AnsiString, ParameterDirection.Input);
        dpChart.Add("@TimeText", eventData.TimeText, DbType.AnsiString, ParameterDirection.Input);
        dpChart.Add("@JdForEt", eventData.JdForEt, DbType.Double, ParameterDirection.Input);
        try
        {
            using var cnn = dbConnection;
            cnn.Open();
            newIndex = cnn.Query<int>(sql + "select last_insert_rowid()", dpChart).First();
        }
        catch (Exception e)
        {
            Log.Error(
                "EventDataDao.PerformInsert: trying to insert event with results in exception {Ex}",
                e.Message);
        }
        return newIndex;
    }

   private bool PerformDelete(long index)
    {
        bool result = false;
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            const string sql = """
                               DELETE FROM ChartsEvents WHERE EventId = @Id;
                               DELETE FROM Events WHERE Id = @Id;
                               """;
            var dp = new DynamicParameters();
            dp.Add("@Id", index);
            using var cnn = dbConnection;
            cnn.Open();
            cnn.Execute(sql, dp);
            result = true;
        }
        catch (Exception e)
        {
            Log.Error("EventDataDao.PerformDelete. Exception when trying to delete event with index {Index}. Exception msg: {Msg}", index, e.Message);   
        }
        return result;

    }
    
    private PersistableEventData? PerformRead(long index)
    {
        PersistableEventData? result = null;
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            const string sqlChart = "SELECT * FROM Events WHERE id = @Id;";
            var dp = new DynamicParameters();
            dp.Add("@Id", index);
            using var cnn = dbConnection;
            cnn.Open();
            var events = cnn.Query<PersistableEventData>(sqlChart, dp).ToList();
            result = events[0];
        }
        catch (Exception e)
        {
            Log.Error("EventDataDao.PerformRead. Exception when reading events for index {Id}. " +
                      "Exception msg: {Msg}", index, e.Message);
        }

        return result;
    }
    
    private List<PersistableEventData> PerformSearch(long chartId)
    {
        List<PersistableEventData> result = new();
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            const string sql = """
                               SELECT id, description, locationName, geoLong, geoLat, dateText, timeText, jdForEt 
                               FROM EVENTS WHERE id in (
                                  SELECT eventId from ChartsEvents WHERE chartId = @ChartId)
                               """;
            var dp = new DynamicParameters();
            dp.Add("@ChartId", chartId);
            using var cnn = dbConnection;
            cnn.Open();
            result = cnn.Query<PersistableEventData>(sql, dp).ToList();
        }
        catch (Exception e)
        {
            Log.Error("EventDataDao.PerformRead. Exception when reading events for chart index {Id}. " +
                      "Exception msg: {Msg}", chartId, e.Message);
        }
        return result;
    }
    
   
}