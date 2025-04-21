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

/// <summary>DAO for settings in the database</summary>
public interface ISettingsDao
{
    /// <summary>Inserts a new setting into the database.</summary>
    /// <param name="setting">The setting to insert.</param>
    /// <returns>True if the insert was successful, false otherwise.</returns>
    public bool InsertSetting(SettingsDto setting);

    /// <summary>Updates an existing setting in the database.</summary>
    /// <param name="setting">The setting to update.</param>
    /// <returns>True if the update was successful, false otherwise.</returns>
    public bool UpdateSetting(SettingsDto setting);

    /// <summary>Checks if a setting exists in the database.</summary>
    /// <param name="name">Name of the setting to check.</param>
    /// <returns>True if the setting exists, false otherwise.</returns>
    public bool SettingExists(string name);

    /// <summary>Reads the value of a setting from the database.</summary>
    /// <param name="name">Name of the setting to read.</param>
    /// <returns>The value of the setting, or null if the setting doesn't exist.</returns>
    public string? ReadSetting(string name);
}

/// <inheritdoc/>
public class SettingsDao: ISettingsDao
{
    /// <inheritdoc/>
    public bool InsertSetting(SettingsDto setting)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            const string insertQuery = """
                                      INSERT INTO Settings(name, value)
                                      VALUES(@Name, @Value);
                                      """;

            var affectedRows = dbConnection.Execute(insertQuery, setting);
            var success = affectedRows > 0;
            if (success)
            {
                Log.Information("Inserted setting {Name} with value {Value}", 
                    setting.Name, setting.Value);
            }
            else
            {
                Log.Warning("Failed to insert setting {Name}", setting.Name);
            }
            return success;
        }
        catch (Exception e)
        {
            Log.Error("Error inserting setting {Name}. Exception: {Msg}", 
                setting.Name, e.Message);
            return false;
        }
    }

    /// <inheritdoc/>
    public bool UpdateSetting(SettingsDto setting)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            const string updateQuery = """
                                      UPDATE Settings
                                      SET value = @Value
                                      WHERE name = @Name;
                                      """;

            var affectedRows = dbConnection.Execute(updateQuery, setting);
            var success = affectedRows > 0;
            if (success)
            {
                Log.Information("Updated setting {Name} with value {Value}", 
                    setting.Name, setting.Value);
            }
            else
            {
                Log.Warning("No setting found with name {Name} to update", setting.Name);
            }
            return success;
        }
        catch (Exception e)
        {
            Log.Error("Error updating setting {Name}. Exception: {Msg}", 
                setting.Name, e.Message);
            return false;
        }
    }

    /// <inheritdoc/>
    public bool SettingExists(string name)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            const string query = "SELECT COUNT(*) FROM Settings WHERE name = @Name";
            var count = dbConnection.ExecuteScalar<int>(query, new { Name = name });
            return count > 0;
        }
        catch (Exception e)
        {
            Log.Error("Error checking if setting {Name} exists. Exception: {Msg}", 
                name, e.Message);
            return false;
        }
    }

    /// <inheritdoc/>
    public string? ReadSetting(string name)
    {
        try
        {
            var fullPath = Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            const string query = "SELECT value FROM Settings WHERE name = @Name";
            var value = dbConnection.QueryFirstOrDefault<string>(query, new { Name = name });
            
            if (value == null)
            {
                Log.Warning("No setting found with name {Name}", name);
            }
            else
            {
                Log.Information("Read setting {Name} with value {Value}", name, value);
            }
            
            return value;
        }
        catch (Exception e)
        {
            Log.Error("Error reading setting {Name}. Exception: {Msg}", 
                name, e.Message);
            return null;
        }
    }
} 