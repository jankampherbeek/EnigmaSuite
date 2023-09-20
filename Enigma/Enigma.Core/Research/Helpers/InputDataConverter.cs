// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Persistables;

namespace Enigma.Core.Research.Helpers;



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