// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Data.SQLite;
using Dapper;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>DAO for controlgroup types</summary>
public interface IControlGroupTypeDao
{
    /// <summary>Get Id for a specific controlgroup type</summary>
    /// <param name="name">Name of the controlgroup type</param>
    /// <returns>Id for the controlgroup type</returns>
    public int ReadIdForControlGroupType(string name);

    /// <summary>Get resource bundle key for a specific controlgroup type</summary>
    /// <param name="id">Id of the controlgroup type</param>
    /// <returns>Resource bundle key for the controlgroup type</returns>
    public string ReadRbKeyForControlGroupType(int id);
}

/// <inheritdoc/>
public class ControlGroupTypeDao: IControlGroupTypeDao
{
    public int ReadIdForControlGroupType(string name)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            const string sql = "SELECT id FROM ControlGroupTypes WHERE name = @Name";
            var dp = new DynamicParameters();
            dp.Add("@Name", name);
            dbConnection.Open();
            var result = dbConnection.Query<int>(sql, dp).FirstOrDefault();
            return result;
        }
        catch (Exception e)
        {
            Log.Error("ControlGroupTypeDao.ReadIdForControlGroupType. Exception when reading id for name {Name}. Exception msg: {Msg}", name, e.Message);
            return -1;
        }
    }

    public string ReadRbKeyForControlGroupType(int id)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            const string sql = "SELECT rbkey FROM ControlGroupTypes WHERE id = @Id";
            var dp = new DynamicParameters();
            dp.Add("@Id", id);
            dbConnection.Open();
            var result = dbConnection.Query<string>(sql, dp).FirstOrDefault();
            return result ?? string.Empty;
        }
        catch (Exception e)
        {
            Log.Error("ControlGroupTypeDao.ReadRbKeyForControlGroupType. Exception when reading rbkey for name {Id}. Exception msg: {Msg}", id, e.Message);
            return string.Empty;
        }
    }
}