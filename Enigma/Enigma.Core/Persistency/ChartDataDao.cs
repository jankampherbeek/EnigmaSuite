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

/// <summary>DAO for chart data.</summary>
public interface IChartDataDao
{
   
    /// <summary>Insert a new record.</summary>
    /// <param name="chartData">The record to insert.</param>
    /// <remarks>The id of the record is overwritten with the first available new index.</remarks>
    /// <returns>The id for the inserted record or -1 if the insert could not be fullfilled.</returns>
    public long AddChartData(PersistableChartData chartData);
    
    /// <summary>Delete a record.</summary>
    /// <param name="index">The index of the record to delete.</param>
    /// <returns>True if the record was deleted, false if the record was not found.</returns>
    public bool DeleteChartData(long index);
    
    /// <summary>Read chartdata for a given index.</summary>
    /// <param name="index">The index to check.</param>
    /// <returns>If found: the record that corresponds to the given index, otherwise null.</returns>
    public PersistableChartData? ReadChartData(long index);
    
    /// <summary>Read all chart identifications.</summary>
    /// <returns>List with all records.</returns>
    public List<PersistableChartIdentification>? ReadAllChartIdentifications();    
    
    /// <summary>Search for records using a part of the name as searchargument.</summary>
    /// <remarks>Search is case-insensitive.</remarks>
    /// <param name="partOfName">The search argument.</param>
    /// <returns>List with zero or more records that are found.</returns>
    public List<PersistableChartIdentification>? SearchChartData(string? partOfName);
    
    /// <summary>Count all records.</summary>
    /// <returns>The total number of records.</returns>
    public int CountRecords();

    /// <summary>Return the index of the last record.</summary>
    /// <returns>The highest index. If table is empty: -1.</returns>
    public long HighestIndex();
}



/// <inheritdoc />
public sealed class ChartDataDao : IChartDataDao
{
    private readonly string _fullPath = "Data Source=" + ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
    
    /// <inheritdoc />
    public long AddChartData(PersistableChartData chartData)
    {
        return PerformInsert(chartData);
    }

    /// <inheritdoc />
    public bool DeleteChartData(long index)
    {
        return PerformDelete(index);
    }
    
    /// <inheritdoc />
    public PersistableChartData? ReadChartData(long index)
    {
        return PerformRead(index);
    }
    
    /// <inheritdoc />
    public List<PersistableChartIdentification> ReadAllChartIdentifications()
    {
        return PerformReadAllChartIdentifications();
    }
    
    /// <inheritdoc />
    public List<PersistableChartIdentification>? SearchChartData(string? partOfName)
    {
        return PerformSearch(partOfName);
    }
    
    /// <inheritdoc />
    public int CountRecords()
    {
        return PerformCount();
    }

    public long HighestIndex()
    {
        return PerformHighestIndex();
    }


    private long PerformInsert(PersistableChartData chartData)
    {
        long chartIndex = -1;
        PersistableChartIdentification chartIdent = chartData.Identification;
        PersistableChartDateTimeLocation dateTimeLoc = chartData.DateTimeLocs[0];     // todo 0.3 support multiple instances of dateTimeLoc
        try
        {
            chartIndex = PerformInsertChartIdentification(chartIdent);
            long dataIndex = PerformInsertChartDateTimeLocation(dateTimeLoc, chartIndex);
            Log.Information("Saved chart with index {IdChart} and data-time-location {IdData}", chartIndex, dataIndex);
        }
        catch (Exception e)
        {
            // TODO check if chart has been saved. If found, delete it.
            Log.Error("ChartDataDao.PerformInsert. Could not insert chart or date/location. Msg: {Msg}", e.Message);   
        }
        return chartIndex;
    }

    private long PerformInsertChartIdentification(PersistableChartIdentification chartIdent)
    {
        SQLiteConnection dbConnection = new(_fullPath);
        long newIndex = -1;
        const string sql = """
                           INSERT INTO Charts (name, description, category)
                           VALUES (@name, @description, @category);
                           """;
        var dpChart = new DynamicParameters();
        dpChart.Add("@name", chartIdent.Name, DbType.AnsiString, ParameterDirection.Input);
        dpChart.Add("@description", chartIdent.Description, DbType.AnsiString, ParameterDirection.Input);
        dpChart.Add("@category", chartIdent.ChartCategoryId, DbType.Int32, ParameterDirection.Input);
        using var cnn = dbConnection;
        cnn.Open();
        newIndex = cnn.Query<int>(sql + "select last_insert_rowid()", dpChart).First();
        return newIndex;
    }

    private long PerformInsertChartDateTimeLocation(PersistableChartDateTimeLocation dateTimeLoc, long chartId)
    {
        
        SQLiteConnection dbConnection = new(_fullPath);
        long newIndex = -1;
        const string sql = 
            """
            INSERT INTO DateLocations (chartId, source, locationName, ratingId, geoLong, geoLat, dateText, timeText, jdForEt)
            VALUES (@idChart, @source, @locationName, @rating, @geoLong, @geoLat, @dateText, @timeText, @julianDayEt);
            """;    
        var dpDateTimeLoc = new DynamicParameters();
        dpDateTimeLoc.Add("@idChart", chartId,  DbType.Int32, ParameterDirection.Input);
        dpDateTimeLoc.Add("@source", dateTimeLoc.Source,  DbType.AnsiString, ParameterDirection.Input);
        dpDateTimeLoc.Add("@locationName", dateTimeLoc.LocationName,  DbType.AnsiString, ParameterDirection.Input);
        dpDateTimeLoc.Add("@rating", dateTimeLoc.RatingId, DbType.Int32, ParameterDirection.Input);
        dpDateTimeLoc.Add("@geoLong", dateTimeLoc.GeoLong, DbType.Double, ParameterDirection.Input);
        dpDateTimeLoc.Add("@geoLat", dateTimeLoc.GeoLat, DbType.Double, ParameterDirection.Input);
        dpDateTimeLoc.Add("@dateText", dateTimeLoc.DateText,  DbType.AnsiString, ParameterDirection.Input);
        dpDateTimeLoc.Add("@timeText", dateTimeLoc.TimeText,  DbType.AnsiString, ParameterDirection.Input);
        dpDateTimeLoc.Add("@julianDayEt", dateTimeLoc.JdForEt, DbType.Double, ParameterDirection.Input);
        using var cnn = dbConnection;
        cnn.Open();
        newIndex = cnn.Query<int>(sql + "select last_insert_rowid()", dpDateTimeLoc).First();
        return newIndex;
    }
    
    
    private bool PerformDelete(long index)
    {
        bool result = false;
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            const string sql = """
                               DELETE FROM ChartsEvents WHERE ChartId = @Id; 
                               DELETE FROM DateLocations WHERE ChartId = @Id;
                               DELETE FROM Charts WHERE Id = @Id;
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
            Log.Error("ChartDataDao.PerformDelete. Exception when trying to delete chart with index {Index}", index);   
        }
        return result;
    }
    
    private PersistableChartData? PerformRead(long index)
    {
        PersistableChartData? result = null;
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            const string sqlChart = "SELECT * FROM CHARTS WHERE id = @Id";
            const string sqlData = "SELECT * FROM DATELOCATIONS WHERE chartId = @Id";
            var dp = new DynamicParameters();
            dp.Add("@Id", index);
            using var cnn = dbConnection;
            cnn.Open();
            var charts = cnn.Query<PersistableChartIdentification>(sqlChart, dp).ToList();
            var data = cnn.Query<PersistableChartDateTimeLocation>(sqlData, dp).ToList();
            
            result = new(charts[0], data);
        }
        catch (Exception e)
        {
            Log.Error("ChartDataDao.PerformRead. Exception when reading charts for index {Id}. Exception msg: {Msg}", index, e.Message);
        }

        return result;
    }
    
    private List<PersistableChartIdentification> PerformReadAllChartIdentifications()
    {
        List<PersistableChartIdentification> allChartIdentifications = new();
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            const string sql = "SELECT * FROM CHARTS";
            using var cnn = dbConnection;
            cnn.Open();
            allChartIdentifications = cnn.Query<PersistableChartIdentification>(sql).ToList();
        }
        catch (Exception e)
        {
            Log.Error("ChartDataDao.PerformReadAllChartIdentifications. Exception when reading all charts. Exception msg: {Msg}", e.Message);
        }
        return allChartIdentifications;
    }
    
    private List<PersistableChartIdentification>? PerformSearch(string? partOfName)
    {
        string searchNamePart = partOfName.ToUpper();
        List<PersistableChartIdentification> foundChartIdentifications = new();
        try
        {
            SQLiteConnection dbConnection = new(_fullPath);
            string sql = "";
            if (string.IsNullOrEmpty(searchNamePart)) sql = "SELECT * FROM CHARTS;";
            else sql = "SELECT * FROM CHARTS WHERE UPPER(NAME) LIKE '%" + searchNamePart + "%';";
            using var cnn = dbConnection;
            cnn.Open();
            foundChartIdentifications = cnn.Query<PersistableChartIdentification>(sql).ToList();
        }
        catch (Exception e)
        {
            Log.Error("ChartDataDao.PerformSearch. Exception when searching for charts with argument {Arg}. Exception msg: {Msg}", partOfName, e.Message);
        }
        return foundChartIdentifications;
    }
    
    private int PerformCount()
    {
        SQLiteConnection dbConnection = new(_fullPath);
        const string sql = "SELECT count(*) FROM CHARTS;";
        using var cnn = dbConnection;
        cnn.Open();
        var result = cnn.Query<int>(sql);
        return result.First();
    }

    private long PerformHighestIndex()
    {
        SQLiteConnection dbConnection = new(_fullPath);
        if (PerformCount() == 0)
        {
            return -1L;
        }
        const string sql = "SELECT max(id) FROM CHARTS;";
        using var cnn = dbConnection;
        cnn.Open();
        var result = cnn.Query<int>(sql);   
        return result.First();            
    }

}