// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Domain.Persistables;

namespace Enigma.Core.Research;

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
    /// <param name="standardInput">The StandardInput to convert.</param>
    /// <returns>The Json result.</returns>
    public string MarshallStandardInput(StandardInput standardInput);


    /// <summary>Create StandardInput from Json.</summary>
    /// <param name="jsonString">The Json with the standard input data.</param>
    /// <returns>The resulting StandardInput.</returns>
    public StandardInput UnMarshallStandardInput(string jsonString);
}


/// <inheritdoc/>
public sealed class InputDataConverter : IInputDataConverter
{

    /// <inheritdoc/>
    public string MarshallInputItem(StandardInputItem inputItem)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(inputItem, options);
    }

    /// <inheritdoc/>
    public StandardInputItem UnMarshallInputItem(string jsonString)
    {
        return JsonSerializer.Deserialize<StandardInputItem>(jsonString)!;
    }

    public string MarshallStandardInput(StandardInput standardInput)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(standardInput, options);
    }

    public StandardInput UnMarshallStandardInput(string jsonString)
    {
        return JsonSerializer.Deserialize<StandardInput>(jsonString)!;
    }

}