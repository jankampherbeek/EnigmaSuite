// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Persistables;
using Enigma.Domain.Responses;

namespace Enigma.Api.Interfaces;

/// <summary>Api for conversions from Csv to Json.</summary>
public interface IDataHandlerApi
{
    /// <summary>Convert a datafile in standard csv-format to a Json file.</summary>
    /// <remarks>Locations for the files are retrieved from the application settings.</remarks>
    /// <param name="sourceFile">Path to the source file.</param>
    /// <param name="dataName">Name for the datafile.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage ConvertDataFile2Json(string sourceFile, string dataName);
}

/// <summary>Api for managing the file system.</summary>
public interface IDataFileManagementApi
{
    /// <summary>Check if a folder can be created.</summary>
    /// <param name="fullPath">The full path of the folder to check.</param>
    /// <returns>True if the folder does not yet exist, otherwise false.</returns>
    public bool FolderIsAvailable(string fullPath);

    /// <summary>Creates folders for research data and additionally subfodlers 'csv' and 'json'.</summary>
    /// <param name="fullPath">The path for the datafiles, not including the csv and json subfolders.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage CreateFoldersForData(string fullPath);

    /// <summary>Create a list of data names, based in folders in the file system.</summary>
    /// <returns>Dat names.</returns>
    public IEnumerable<string> GetDataNames();
}

/// <summary>API for simple read and write access to files.</summary>
public interface IFileAccessApi
{
    /// <summary>Write a file to disk.</summary>
    /// <param name="path">Full path for the file.</param>
    /// <param name="text">Content of the file.</param>
    /// <returns>True if no error occured, otherwise false.</returns>
    public bool WriteFile(string path, string text);

    /// <summary>Read a file from disk.</summary>
    /// <param name="path">Full path for the file.</param>
    /// <returns>Content of the file.</returns>
    public string ReadFile(string path);
}

/// <summary>AI for persistency ChartData.</summary>
public interface IChartDataPersistencyApi
{
    /// <summary>Calculate number of records in Json file.</summary>
    /// <returns>The number of records</returns>
    public int NumberOfRecords();

    /// <summary>Define the highest index that is currently in use.</summary>
    /// <returns>The highest index.</returns>
    public int HighestIndex();

    /// <summary>Read a specific record.</summary>
    /// <param name="index">The unique index for the record.</param>
    /// <returns>If found: the record. Otherwise: null.</returns>
    public PersistableChartData? ReadChartData(int index);

    /// <summary>Read records that correspond (partly) with a given searchterm for the name.</summary>
    /// <param name="partOfName">The search term.</param>
    /// <returns>List with zero or more results.</returns>
    public List<PersistableChartData>? SearchChartData(string? partOfName);

    /// <summary>Read all records.</summary>
    /// <returns>List with zero or more results.</returns>
    public List<PersistableChartData>? ReadAllChartData();

    /// <summary>Add a record.</summary>
    /// <param name="chartData">The record to insert.</param>
    /// <returns>The index for the inserted record, -1 if the record could not be inserted.</returns>
    public int AddChartData(PersistableChartData chartData);

    /// <summary>Dele a record.</summary>
    /// <param name="index">Id of the record to delete.</param>
    /// <returns>True if the record was deleted, false if the record does not exist.</returns>
    public bool DeleteChartData(int index);
}



/// <summary>AI for persistency Event data.</summary>
public interface IEventDataPersistencyApi
{
    /// <summary>Calculate number of records in database.</summary>
    /// <returns>The number of records</returns>
    public int NumberOfRecords();

    /// <summary>Calculate number of event records in database that have an intersection with a specific chart.</summary>
    /// <param name="chartId">Index for the chart.</param>
    /// <returns>The number of records.</returns>
    public int NumberOfRecords(int chartId);


    /// <summary>Define the highest index that is currently in use.</summary>
    /// <returns>The highest index.</returns>
    public int HighestIndex();

    /// <summary>Read a specific record.</summary>
    /// <param name="index">The unique index for the record.</param>
    /// <returns>If found: the record. Otherwise: null.</returns>
    public PersistableEventData? ReadEventData(int index);

    /// <summary>Read records that correspond (partly) with a given searchterm for the description of the event.</summary>
    /// <param name="partOfDescription">The search term.</param>
    /// <returns>List with zero or more results.</returns>
    public List<PersistableEventData> SearchEventData(string? partOfDescription);

    /// <summary>Read records of events that have an intersection with a given chart.</summary>
    /// <param name="chartId">Id of the chart.</param>
    /// <returns>List with zero or more results.</returns>
    public List<PersistableEventData> SearchEventData(int chartId);


    /// <summary>Read all records.</summary>
    /// <returns>List with zero or more results.</returns>
    public List<PersistableEventData> ReadAllEventData();

    /// <summary>Add a record.</summary>
    /// <param name="eventData">The record to insert.</param>
    /// <returns>The index for the inserted record, -1 if the record could not be inserted.</returns>
    public int AddEventData(PersistableEventData eventData);

    /// <summary>Add a record and an intersection.</summary>
    /// <param name="eventData">The record to insert.</param>
    /// <param name="chartId">Id of the chart that will be coupled to the event.</param>
    /// <returns>The index for the inserted record, -1 if the record could not be inserted.</returns>
    public int AddEventData(PersistableEventData eventData, int chartId);
    
    /// <summary>Dele a record for an event.</summary>
    /// <remarks>Also deletes entries for this event in the intersections.</remarks>
    /// <param name="index">Id of the record to delete.</param>
    /// <returns>True if the record was deleted, false if the record does not exist.</returns>
    public bool DeleteEventData(int index);
}




/// <summary>AI for persistency Period data.</summary>
public interface IPeriodDataPersistencyApi
{
    /// <summary>Calculate number of records in database.</summary>
    /// <returns>The number of records</returns>
    public int NumberOfRecords();

    /// <summary>Calculate number of period records in database that have an intersection with a specific chart.</summary>
    /// <param name="chartId">Index for the chart.</param>
    /// <returns>The number of records.</returns>
    public int NumberOfRecords(int chartId);


    /// <summary>Define the highest index that is currently in use.</summary>
    /// <returns>The highest index.</returns>
    public int HighestIndex();

    /// <summary>Read a specific record.</summary>
    /// <param name="index">The unique index for the record.</param>
    /// <returns>If found: the record. Otherwise: null.</returns>
    public PersistablePeriodData? ReadPeriodData(int index);

    /// <summary>Read records that correspond (partly) with a given searchterm for the description of the event.</summary>
    /// <param name="partOfDescription">The search term.</param>
    /// <returns>List with zero or more results.</returns>
    public List<PersistablePeriodData> SearchPeriodData(string? partOfDescription);

    /// <summary>Read records of events that have an intersection with a given chart.</summary>
    /// <param name="chartId">Id of the chart.</param>
    /// <returns>List with zero or more results.</returns>
    public List<PersistablePeriodData> SearchPeriodData(int chartId);


    /// <summary>Read all records.</summary>
    /// <returns>List with zero or more results.</returns>
    public List<PersistablePeriodData> ReadAllPeriodData();

    /// <summary>Add a record.</summary>
    /// <param name="periodData">The record to insert.</param>
    /// <returns>The index for the inserted record, -1 if the record could not be inserted.</returns>
    public int AddPeriodData(PersistablePeriodData periodData);
    
    /// <summary>Add a record and an intersection.</summary>
    /// <param name="periodData">The record to insert.</param>
    /// <param name="chartId">Id of the chart that will be coupled to the period.</param>
    /// <returns>The index for the inserted record, -1 if the record could not be inserted.</returns>
    public int AddPeriodData(PersistablePeriodData periodData, int chartId);
    

    /// <summary>Dele a record for a period.</summary>
    /// <remarks>Also deletes entries for this period in the intersections.</remarks>
    /// <param name="index">Id of the record to delete.</param>
    /// <returns>True if the record was deleted, false if the record does not exist.</returns>
    public bool DeletePeriodData(int index);
}




