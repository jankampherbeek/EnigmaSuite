// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Helpers.Interfaces;


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
