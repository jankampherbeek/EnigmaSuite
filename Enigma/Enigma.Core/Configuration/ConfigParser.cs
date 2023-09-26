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
    public string MarshallConfig(AstroConfig astroConfig)
    {
        var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
        return JsonSerializer.Serialize(astroConfig, options);
    }

    /// <inheritdoc/>
    public string MarshallConfig(ConfigProg configProg)
    {
        var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
        return JsonSerializer.Serialize(configProg, options);
    }

    /// <inheritdoc/>
    public AstroConfig UnMarshallAstroConfig(string jsonString)
    {
        var options = new JsonSerializerOptions { IncludeFields = true };
        return JsonSerializer.Deserialize<AstroConfig>(jsonString, options)!;
    }

    /// <inheritdoc/>
    public ConfigProg UnMarshallConfigProg(string jsonString)
    {
        var options = new JsonSerializerOptions { IncludeFields = true };
        return JsonSerializer.Deserialize<ConfigProg>(jsonString, options)!;
    }
}