// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Messages;
using Enigma.Domain.Persistency;

namespace Enigma.Persistency.Interfaces;


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
    /// <summary>Read all text from a file inton a string.</summary>
    /// <param name="location"></param>
    /// <returns>If the file is found and no exception occured: the content of the file. Otherwile: an empty string.</returns>
    public string ReadFile(string location);
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

/// <summary>Handles reading data from a csv file and writing it to a Json file.</summary>
public interface ICsvHandler
{
    /// <summary>Processes data in the 'standard' csv-format and writes it to Json.</summary>
    /// <remarks>Writes a meta-file with processing status and error lines. Only writes the Json file if no errors were found.</remarks>
    /// <param name="dataName">Name for dataset.</param>
    /// <param name="fullPathCsv">Full path of the csv file.</param>
    /// <param name="fullPathJson">Full path of the json file.</param>
    /// <returns>ResultMessage with an errorcode > zero if an error occurred. Message contains the error description or a report of a succesfull conversion.</returns>
    public ResultMessage ConvertStandardCsvToJson(string dataName, string fullPathCsv, string fullPathJson);
}


public interface IDataNameHandler
{
    List<string> GetExistingDataNames(string path);
}


