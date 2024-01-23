// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Data.SQLite;
using Dapper;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
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
    private const string DATA_SOURCE_PREFIX = "Data Source=";
    
    /// <inheritdoc/>
    public bool PreparaDatabase()
    {
        bool noErrors = true;
        string fullPath = CreateFullPath();
        if (!DbExists(fullPath))
        {
            Log.Information("Database not found at {FullPath}. Creating a new database", fullPath);
            if (CreateDatabase(fullPath))
            {
                Log.Information("Database successfully created");
                if (PopulateDatabase(fullPath))
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
        return noErrors;
    }


    private static string CreateFullPath()
    {
        return ApplicationSettings.LocationDatabase + EnigmaConstants.RDBMS_NAME;
    }
    
    private static bool DbExists(string fullPath)
    {
        return File.Exists(fullPath);
    }

    private static bool CreateDatabase(string fullPath)
    {
        bool noErrors = true;
        try
        {
            SQLiteConnection dbConnection = new(DATA_SOURCE_PREFIX + fullPath);
            string sqlQuery = ConstructInitQuery();
            using var cnn = dbConnection;
            cnn.Open();
            cnn.Execute(sqlQuery);            
        } catch (Exception e)
        {
            Log.Error("An error occurred while creating database. Exception: {Msg}", e.Message);
            noErrors = false;
        }
        return noErrors;
    }

    private static bool PopulateDatabase(string fullPath)
    {
        bool noErrors = true;
        try
        {
            SQLiteConnection dbConnection = new(DATA_SOURCE_PREFIX + fullPath);
            string sqlQuery = ConstructPopulateQuery();
            using var cnn = dbConnection;
            cnn.Open();
            cnn.Execute(sqlQuery);
            var anonymousDbVersion = new{description = EnigmaConstants.ENIGMA_VERSION};
            const string versionSql = "insert into DbVersions(description) VALUES(@description);";
            cnn.Execute(versionSql, anonymousDbVersion);
        } catch (Exception e)
        {
            Log.Error("An error occurred while populating database. Exception: {Msg}", e.Message);
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
    
}