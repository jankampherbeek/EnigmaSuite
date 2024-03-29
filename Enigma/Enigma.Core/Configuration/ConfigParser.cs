﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;

namespace Enigma.Core.Configuration;

/// <summary>Parser for configurations.</summary>
public interface IConfigParser
{

    /// <summary>Parse deltas for config: from Dictionary to Json. Supports standard config and prog config.</summary>
    /// <param name="deltas">Deltas for the configuration.</param>
    /// <returns>The Json.</returns>
    public string MarshallDeltasForConfig(Dictionary<string, string> deltas);
    
    /// <summary>Parse deltas for config: from json to dictionary. Supports standard config and prog config.</summary>
    /// <param name="jsonString">The Json.</param>
    /// <returns>The deltas.</returns>
    public Dictionary<string, string> UnMarshallDeltasForConfig(string jsonString);

}

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