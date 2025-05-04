// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Data.SQLite;
using Dapper;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using LiteDB;
using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>Prepares the database, if necessary, for use by the application.</summary>
public interface IRdbmsPreparator
{
    /// <summary>Create a database if it does not exist, append a database if it is not up to date.</summary>
    /// <returns>True if no errors occured.</returns>
    public bool PreparaDatabase();
}

/// <inheritdoc/>
public class RdbmsPreparator: IRdbmsPreparator
{
    private const string DB_VERSION = "0.6.0";
    
    /// <inheritdoc/>
    public bool PreparaDatabase()
    {
        var noErrors = true;
        var fullPath = CreateFullPath();
        var oldPath = GetOldDatabasePath();

        Log.Information("Checking database at {FullPath}", fullPath);
        Log.Information("Old database path: {OldPath}", oldPath);

        if (!DbExists(fullPath))
        {
            // Only try to migrate if new database doesn't exist
            if (File.Exists(oldPath))
            {
                Log.Information("New database not found, attempting to migrate from old location");
                if (MigrateDatabase(oldPath, fullPath))
                {
                    Log.Information("Database successfully migrated from old location");
                }
                else
                {
                    Log.Error("Database migration failed");
                    noErrors = false;
                }
            }
            else
            {
                Log.Information("No database found, creating new one at {FullPath}", fullPath);
                if (CreateDatabase(fullPath))
                {
                    Log.Information("Database successfully created");
                    if (PopulateDatabaseInitial(fullPath))
                    {
                        Log.Information("Database successfully populated");
                    }
                    else noErrors = false;
                }
                else
                {
                    Log.Error("Database could not be created");
                    noErrors = false;
                }
            }
        }
        else
        {
            Log.Information("Database already exists at {FullPath}", fullPath);
        }

        // Verify database exists and is not empty
        if (File.Exists(fullPath) && new FileInfo(fullPath).Length > 0)
        {
            Log.Information("Database exists and is not empty at {FullPath}", fullPath);
            var latestVersion = LatestVersion(fullPath);
            Log.Information("Latest version of database is {Version}", latestVersion);
        }
        else
        {
            Log.Error("Database is missing or empty at {FullPath}", fullPath);
            noErrors = false;
        }

        if (noErrors) noErrors = UpdateDatabase(fullPath);
        return noErrors;
    }

    private static bool UpdateDatabase(string fullPath)
    {
        var noErrors = true;
        try
        {
            var currentVersion = LatestVersion(fullPath);
            Log.Information("Current database version: {CurrentVersion}, Target version: {TargetVersion}", 
                currentVersion, DB_VERSION);

            // Compare versions using string comparison
            if (string.Compare(currentVersion, DB_VERSION, StringComparison.Ordinal) >= 0)
            {
                Log.Information("Database is already at version {Version} or higher", DB_VERSION);
                return noErrors;
            }
            Log.Information("Starting database update to version {Version}", DB_VERSION);
            var sqlQuery = Construct_0_6_Query();
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            using var transaction = dbConnection.BeginTransaction();
            try
            {
                var affectedRows = dbConnection.Execute(sqlQuery, transaction: transaction);
                Log.Information("DDL executed, affected rows: {Rows}", affectedRows);
                // Update version
                const string setVersionQuery = "INSERT INTO DbVersions(description) VALUES(@version)";
                var versionParams = new { version = DB_VERSION };
                affectedRows = dbConnection.Execute(setVersionQuery, versionParams, transaction: transaction);
                Log.Information("Version update executed, affected rows: {Rows}", affectedRows);

                // Verify tables were created
                const string tablesQuery = "SELECT name FROM sqlite_master WHERE type='table'";
                var tables = dbConnection.Query<string>(tablesQuery, transaction: transaction).ToList();
                Log.Information("Tables after update: {Tables}", string.Join(", ", tables));
                transaction.Commit();
                Log.Information("Successfully committed database update to version {Version}", DB_VERSION);
                PopulateDatabase_0_6(fullPath);
                Log.Information("Populated version 0.6.0 of database");
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Log.Error("Error during database update, rolling back. Exception: {Msg}", e.Message);
                throw;
            }
        }
        catch (Exception e)
        {
            Log.Error("Exception while updating database to version {Version}: {Msg}", DB_VERSION, e.Message);
            Log.Error("Stack trace: {StackTrace}", e.StackTrace);
            noErrors = false;
        }
        return noErrors;
    }
    
    

    private static string CreateFullPath()
    {
        return Path.Combine(ApplicationSettings.LocationDatabase, EnigmaConstants.RDBMS_NAME);
    }

    private static string GetOldDatabasePath()
    {
        return Path.Combine(@"c:\enigma_ar\database", EnigmaConstants.RDBMS_NAME);
    }
    
    private static bool DbExists(string fullPath)
    {
        return File.Exists(fullPath);
    }

    private static bool MigrateDatabase(string oldPath, string newPath)
    {
        try
        {
            Log.Information("Attempting to migrate database from {OldPath} to {NewPath}", oldPath, newPath);
            if (!File.Exists(oldPath))
            {
                Log.Information("Old database not found at {OldPath}, skipping migration", oldPath);
                return false;
            }
            // Check if new database already exists and is not empty
            if (File.Exists(newPath) && new FileInfo(newPath).Length > 0)
            {
                Log.Information("New database already exists and is not empty, skipping migration");
                return true;
            }
            // Ensure the new directory exists
            var newDirectory = Path.GetDirectoryName(newPath);
            if (!string.IsNullOrEmpty(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);
            }
            // Delete empty database if it exists
            if (File.Exists(newPath) && new FileInfo(newPath).Length == 0)
            {
                Log.Information("Deleting empty database file at {NewPath}", newPath);
                File.Delete(newPath);
            }
            // Copy the database file
            File.Copy(oldPath, newPath, true);
            Log.Information("Successfully migrated database to new location");
            // Verify the migration
            if (File.Exists(newPath) && new FileInfo(newPath).Length > 0)
            {
                Log.Information("Migration verified: new database size is {Size} bytes", new FileInfo(newPath).Length);
                return true;
            }
            Log.Error("Migration failed: new database is empty");
            return false;
        }
        catch (Exception e)
        {
            Log.Error("Error during database migration. Exception: {Msg}", e.Message);
            Log.Error("Stack trace: {StackTrace}", e.StackTrace);
            return false;
        }
    }

    private static string LatestVersion(string fullPath)
    {
        try
        {
            Log.Information("Attempting to connect to database at: {FullPath}", fullPath);
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            // Try to get the latest version
            const string versionQuery = "SELECT description FROM DbVersions ORDER BY description DESC LIMIT 1";
            var version = dbConnection.Query<string>(versionQuery).FirstOrDefault();
            if (version == null)
            {
                Log.Warning("No version found in DbVersions table");
                return "0.0";
            }
            Log.Information("Latest version found: {Version}", version);
            return version;
        }
        catch (Exception e)
        {
            Log.Error("Error when reading latest version of database. Exception: {Msg}", e.Message);
            Log.Error("Stack trace: {StackTrace}", e.StackTrace);
            return "0.0";
        }
    }
    
    
    private static bool CreateDatabase(string fullPath)
    {
        var noErrors = true;
        try
        {
            Log.Information("Creating database at: {FullPath}", fullPath);
            // Ensure the directory exists
            var directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
                Log.Information("Created directory: {Directory}", directory);
            }
            // Create the database file
            SQLiteConnection.CreateFile(fullPath);
            Log.Information("Created database file");
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            // Create initial tables
            var sqlQuery = ConstructInitQuery();
            dbConnection.Execute(sqlQuery);
            Log.Information("Created database schema");
            // Insert initial version
            const string versionSql = "INSERT INTO DbVersions(description) VALUES(@version)";
            dbConnection.Execute(versionSql, new { version = "0.0" });
            Log.Information("Inserted initial version");
        } 
        catch (Exception e)
        {
            Log.Error("An error occurred while creating database. Exception: {Msg}", e.Message);
            Log.Error("Stack trace: {StackTrace}", e.StackTrace);
            noErrors = false;
        }
        return noErrors;
    }

    private static bool PopulateDatabaseInitial(string fullPath)
    {
        return PopulateDatabase(fullPath, ConstructPopulateQuery(), "0.6.0");
    }
    
    private static bool PopulateDatabase_0_6(string fullPath)
    {
        return PopulateDatabase(fullPath, ConstructPopulate_0_6_Query(), "0.6.0");
    }

    private static bool PopulateDatabase(string fullPath, string sqlQuery, string version)
    {
        var noErrors = true;
        try
        {
            var connectionString = $"Data Source={fullPath}";
            using var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            dbConnection.Execute(sqlQuery);
            var anonymousDbVersion = new{description = EnigmaConstants.ENIGMA_VERSION};
            const string versionSql = "insert into DbVersions(description) VALUES(@description);";
            dbConnection.Execute(versionSql, anonymousDbVersion);
        } 
        catch (Exception e)
        {
            Log.Error($"An error occurred while populating database for version {version}. Exception: {e.Message}");
            noErrors = false;
        }
        return noErrors;
    }

    private static string ConstructInitQuery()
    {
        return 
            """
            create TABLE ChartCategories(id integer primary key AUTOINCREMENT, name varchar(50) NOT NULL);
            create TABLE Ratings(id integer primary key AUTOINCREMENT, name varchar(50) NOT NULL);
            create TABLE Charts(id integer primary key AUTOINCREMENT, name varchar(100) NOT NULL,
                         description varchar(200), category integer,
                         FOREIGN KEY (category) REFERENCES ChartCategories(id));
            create TABLE DateLocations(id integer primary key AUTOINCREMENT, chartId integer NOT NULL, 
                         source varchar(200) NOT NULL, locationName varchar(100), ratingId integer, 
                         geoLong real NOT NULL, geoLat real NOT NULL, dateText varchar(50) NOT NULL, 
                         timeText varchar(50) NOT NULL, jdForEt real NOT NULL,
                         FOREIGN KEY (chartId) REFERENCES Charts(id),
                         FOREIGN KEY (ratingId) REFERENCES Ratings(id));
            create TABLE Events(id integer PRIMARY KEY AUTOINCREMENT, description varchar(200) NOT NULL,
                         locationName varchar(100), geoLong real, geoLat real, 
                         dateText varchar(50) NOT NULL, timeText varchar(50) NOT NULL, 
                         jdForEt real NOT NULL);
            create TABLE ChartsEvents(chartId integer NOT NULL, eventId integer NOT NULL,
                         PRIMARY KEY (chartId, eventId),
                         FOREIGN KEY (chartId) REFERENCES Charts(id),
                         FOREIGN KEY (eventId) REFERENCES Events(id));
            create TABLE DbVersions(id integer PRIMARY KEY AUTOINCREMENT, description varchar(30) NOT NULL);
            """
            ;
    }

    private static string Construct_0_6_Query()
    {
        return
            """
            create TABLE ControlGroupTypes(id integer primary key AUTOINCREMENT, name varchar(100) NOT NULL, rbkey varchar(50) NOT NULL);
            create TABLE DataFiles(id integer primary key AUTOINCREMENT, name varchar(50) NOT NULL,
                         location varchar(256) NOT NULL);
            create TABLE Projects(id integer primary key AUTOINCREMENT, name varchar(50) NOT NULL,
                         description varchar(200), location varchar(256) NOT NULL, multiFactor integer NOT NULL,
                         created varchar(30) NOT NULL, datafile integer NOT NULL, controlgrouptype integer NOT NULL,
                         FOREIGN KEY (dataFile) REFERENCES DataFiles(id),
                         FOREIGN KEY (controlgrouptype) REFERENCES ControlGroupTypes(id));
            create TABLE Settings(name varchar(30) primary key, value varchar(256) NOT NULL);
            """;
    }
                   
    private static string ConstructPopulateQuery()
    {
        return """
               insert into ChartCategories(name) VALUES('Female');
               insert into ChartCategories(name) VALUES('Male');
               insert into ChartCategories(name) VALUES('Event');
               insert into ChartCategories(name) VALUES('Horary');
               insert into ChartCategories(name) VALUES('Election');
               insert into ChartCategories(name) VALUES('Other');
               insert into ChartCategories(name) VALUES('Unknown');
               insert into Ratings(name) VALUES('Unknown');
               insert into Ratings(name) VALUES('AA - Accurate');
               insert into Ratings(name) VALUES('A - Quoted');
               insert into Ratings(name) VALUES('B - (Auto)biography');
               insert into Ratings(name) VALUES('C - Caution, no source');
               insert into Ratings(name) VALUES('DD - Dirty Data');
               insert into Ratings(name) VALUES('X - No time of birth');
               insert into Ratings(name) VALUES('XX - No date of birth');
               """;
    }
    
    private static string ConstructPopulate_0_6_Query()
    {
        return "insert into ControlGroupTypes(name, rbkey) VALUES('StandardShift','ref.controlgrouptype.standardshift');";
    }
}