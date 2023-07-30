// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using System.Text.Json;
using Enigma.Core.Handlers.Interfaces;

namespace Enigma.Core.Handlers.Configuration.Helpers;


/// <inheritdoc/>
public sealed class AstroConfigParser : IAstroConfigParser
{
    /// <inheritdoc/>
    public string Marshall(AstroConfig astroConfig)
    {
        var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
        return JsonSerializer.Serialize(astroConfig, options);
    }

    /// <inheritdoc/>
    public AstroConfig UnMarshall(string jsonString)
    {
        var options = new JsonSerializerOptions { IncludeFields = true };
        return JsonSerializer.Deserialize<AstroConfig>(jsonString, options)!;
    }

}