// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Interfaces;


/// <summary>Handler for preparation of the file system.</summary>
public interface IDataFilePreparationHandler
{
    /// <summary>Checks if a folder name is available.</summary>
    /// <param name="fullPath">Full path of the folder to check.</param>
    /// <returns>True if the folder is available, otherwise false.</returns>
    public bool FolderNameAvailable(string fullPath);

    /// <summary>Create folders to save data, including the subfolders 'csv' and 'json'.</summary>
    /// <param name="fullPath">Full path of the data folder to create (without the subfolders for csv and json).</param>
    /// <returns>Resultmessage with a description of the action.</returns>
    public ResultMessage MakeFolderStructure(string fullPath);
}


/// <summary>Handler for data names.</summary>
public interface IDataNamesHandler
{
    /// <summary>Retrieve data names from data folders.</summary>
    /// <returns>Data names.</returns>
    public List<string> GetExistingDataNames();
}


/// <summary>Handles the import and conversion to Json of a csv datafile.</summary>
public interface IDataImportHandler
{
    /// <summary>Import a datafile in standard csv and convert it to Json.</summary>
    /// <param name="fullPathSource">Full path to the file to read.</param>
    /// <param name="dataName">Name for the data.</param>
    /// <returns>Resultmessage with a description of the action.</returns>
    public ResultMessage ImportStandardData(string fullPathSource, string dataName);
}

/// <summary>Handler for writing and reading files.</summary>
public interface IFilePersistencyHandler
{
    /// <summary>Reads a textfile.</summary>
    /// <param name="fullPath">Full path to the file to read.</param>
    /// <returns>The textual content of the file.</returns>
    public string ReadFile(string fullPath);

    /// <summary>Writes a textfile.</summary>
    /// <param name="fullPath">Full path to the file to write.</param>
    /// <param name="text">The text to write.</param>
    /// <returns>True if the file was successfully written, otherwise false.</returns>
    public bool WriteFile(string fullPath, string text);
}


/// <summary>Handle directories for storing datafiles.</summary>
public interface IDataFilePreparator
{
    /// <summary>Checks if new folder can be created.</summary>
    /// <param name="fullPath">Path to the folder for the data to import.</param>
    /// <returns>True if folder does not exist, otherwise false.</returns>
    bool FolderNameAvailable(string fullPath);

    /// <summary>Create folder structure for data files, including subfolders.</summary>
    /// <param name="fullPath">Path to the folder for the data.</param>
    /// <returns>ResultMessage, containing errorcode > zero if an error occurred. 
    /// In case of an error the errorText contains the path of the directory that could not be created.</returns>
    ResultMessage MakeFolderStructure(string fullPath);
}

/// <summary>
/// Read text from a file.
/// </summary>
public interface ITextFileReader
{
    /// <summary>Read all text from a file into a string.</summary>
    /// <param name="location">Full path to the text file </param>
    /// <returns>If the file is found and no exception occured: the content of the file. Otherwise: an empty string.</returns>
    public string ReadFile(string location);

    /// <summary>Read all lines from a text file into a list.</summary>
    /// <param name="location">Full path to the text file </param>
    /// <returns>If the file is found and no exception occured: all lines from the file. Otherwise: an empty list.</returns>
    public List<string> ReadAllLines(string location);
}

/// <summary>Write text to a file.</summary>
public interface ITextFileWriter
{
    /// <summary>Write a full string to a textfile. Overwrites any existing file with the same name (location).</summary>
    /// <param name="location">Full pathname of the file.</param>
    /// <param name="text">Content to write to the file.</param>
    /// <returns>True is the write was successful, otherwise false.</returns>
    public bool WriteFile(string location, string text);

    /// <summary>Writes multiple lines to a textfile. Overwrites any existing file with the same name (location).</summary>
    /// <param name="location">Full pathname of the file.</param>
    /// <param name="textLines">Content to write to the file.</param>
    /// <returns>True is the write was successful, otherwise false.</returns>
    public bool WriteFile(string location, List<string> textLines);
}

/// <summary>Retrieves information about folders.</summary>
public interface IFoldersInfo
{
    /// <summary>Return names of folders.</summary>
    /// <param name="path">Full path where to look for folders.</param>
    /// <param name="includeSubFolders">Indicates if subfolders should also be searched.</param>
    /// <returns>Folder names.</returns>
    public List<string> GetExistingFolderNames(string path, bool includeSubFolders);
}

/// <summary>Copy a file in the file system.</summary>
public interface IFileCopier
{
    /// <summary>Performs a copy.</summary>
    /// <param name="source">Full path for the original file.</param>
    /// <param name="destination">Full path for the new location of the file.</param>
    /// <returns>True if the copy was successful, otherwise false.</returns>
    public bool CopyFile(string source, string destination);
}

/// <summary>Reads data from a csv file, converts it, and writes the result to a Json file.</summary>
public interface ICsv2JsonConverter
{
    /// <summary>Processes data in the 'standard' csv-format and converts it to Json.</summary>
    /// <remarks>Creates a list of lines that could not be processed.</remarks>
    /// <param name="csvLines">The csv lines to convert.</param>
    /// <param name="dataName">Name for the data.</param>
    /// <returns>Tuple with three items: a boolean that indicates if the conversion was succesfull, a string with the json,  
    /// and a list with csv-lines that caused an error. It the first item is true, the list with error-lines should be empty.</returns>
    public Tuple<bool, string, List<string>> ConvertStandardDataCsvToJson(List<string> csvLines, string dataName);
}


/// <summary>Conversions to date for csv-data.</summary>
public interface IDateCheckedConversion
{
    public Tuple<PersistableDate, bool> StandardCsvToDate(string csvDate, string csvCalendar);
}

/// <summary>Conversions to time for csv-data.</summary>
public interface ITimeCheckedConversion
{
    public Tuple<PersistableTime, bool> StandardCsvToTime(string csvTime, string zoneOffset, string dst);
}

/// <summary>
/// Conversions for latitude and longitude for csv-data.
/// </summary>
public interface ILocationCheckedConversion
{
    /// <summary>Convert csv text for longitude into a double.</summary>
    /// <param name="csvLocation">Csv text: in the format dd:mm:ss:dir (122:34:56:E)</param>
    /// <returns>Calculated value and an indication of errors. If errors did occutr the value will be zero.</returns>
    public Tuple<double, bool> StandardCsvToLongitude(string csvLocation);

    /// <summary>Convert csv text for latitude into a double.</summary>
    /// <param name="csvLocation">Csv text: in the format dd:mm:ss:dir (12:34:56:N)</param>
    /// <returns>Calculated value and an indication of errors. If errors did occutr the value will be zero.</returns>
    public Tuple<double, bool> StandardCsvToLatitude(string csvLocation);
}


/// <summary>DAO for chart data.</summary>
public interface IChartDataDao
{
    /// <summary>Count all records.</summary>
    /// <returns>The total number of records.</returns>
    public int CountRecords();

    /// <summary>Define the highest index as used by the records.</summary>
    /// <returns>Value of the highest index.</returns>
    public int HighestIndex();

    /// <summary>Read chartdata for a given index.</summary>
    /// <param name="index">The index to check.</param>
    /// <returns>If found: the record that corresponds to the given index, otherwise null.</returns>
    public PersistableChartData? ReadChartData(int index);

    /// <summary>Search for records using a part of the name as searchargument.</summary>
    /// <remarks>Search is case-insensitive.</remarks>
    /// <param name="partOfName">The search argument.</param>
    /// <returns>List with zero or more records that are found.</returns>
    public List<PersistableChartData> SearchChartData(string partOfName);

    /// <summary>Read all chartdata.</summary>
    /// <returns>List with all records.</returns>
    public List<PersistableChartData> ReadAllChartData();

    /// <summary>Insert a new record.</summary>
    /// <param name="chartData">The record to insert.</param>
    /// <remarks>The id of the record is overwritten with the first available new index.</remarks>
    /// <returns>The id for the inserted record or -1 if the insert could not be fullfilled.</returns>
    public int AddChartData(PersistableChartData chartData);

    /// <summary>Delete a record.</summary>
    /// <param name="index">The index of the record to delete.</param>
    /// <returns>True if the record was deleted, false if the record was not found.</returns>
    public bool DeleteChartData(int index);

}
