// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Configuration;


/// <inheritdoc/>
public sealed class ConfigParser : IConfigParser
{
    /// <inheritdoc/>
    public string MarshallDeltasForConfig(Dictionary<string, string> deltas)
    {
        var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
        return JsonSerializer.Serialize(deltas, options);
    }

    /// <inheritdoc/>
    public Dictionary<string, string> UnMarshallDeltasForConfig(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString)) return new Dictionary<string, string>();
        var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
        return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString, options)!;        
    }

}