// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Helpers.Conversions;
using Enigma.Domain.Persistency;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Research;
using Newtonsoft.Json;

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


/// <summary>Converts a ResearchProject to Json and vice versa.</summary>
public interface IResearchProjectParser
{
    /// <summary>Create Json from a ResearchProject.</summary>
    /// <param name="project">The project to convert to Json.</param>
    /// <returns>The Json result.</returns>
    public string Marshall(ResearchProject project);

    /// <summary>Create a ResearchProject from Json.</summary>
    /// <param name="jsonString">The Json with the project data.</param>
    /// <returns>The rtesulting Research Project.</returns>
    public ResearchProject UnMarshall(string jsonString);
}

/// <summary>Converts a StandardInputItem to Json and vice versa.</summary>
public interface IInputDataConverter
{
    /// <summary>Create Json from a StandardInputItem.</summary>
    /// <param name="inputItem">The StandardInputItem to convert.</param>
    /// <returns>The Json result.</returns>
    public string MarshallInputItem(StandardInputItem inputItem);

    /// <summary>Create a StandardInputItem from Json.</summary>
    /// <param name="jsonString">The Json with the standard input item data.</param>
    /// <returns>The resulting StandardInputItem.</returns>
    public StandardInputItem UnMarshallInputItem(string jsonString);


    /// <summary>Create Json from StandardInput.</summary>
    /// <param name="inputItem">The StandardInput to convert.</param>
    /// <returns>The Json result.</returns>
    public string MarshallStandardInput(StandardInput standardInput);


    /// <summary>Create StandardInput from Json.</summary>
    /// <param name="jsonString">The Json with the standard input data.</param>
    /// <returns>The resulting StandardInput.</returns>
    public StandardInput UnMarshallStandardInput(string jsonString);
}
