// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>Configuration for progressive techniques.</summary>
public sealed class ConfigProg
{
    public ConfigProgTransits ConfigTransits { get; }
    public ConfigProgSecDir ConfigSecDir { get; }
    public ConfigProgSymDir ConfigSymDir { get; }

    public ConfigProg(ConfigProgTransits configTransits, ConfigProgSecDir configSecDir, ConfigProgSymDir configSymDir)
    {
        ConfigTransits = configTransits;
        ConfigSecDir = configSecDir;
        ConfigSymDir = configSymDir;
    }
    
}


/// <summary>Configuration for transits.</summary>
/// <param name="Orb">Orb between progressive and radix contacts.</param>
/// <param name="ProgPoints">Avaialable points.</param>
public record ConfigProgTransits(double Orb, Dictionary<ChartPoints, ProgPointConfigSpecs> ProgPoints);

/// <summary>Configuration for secondary progressions.</summary>
/// <param name="Orb">Orb between progressive and radix contacts.</param>
/// <param name="ProgPoints">Avaialable points.</param>
public record ConfigProgSecDir(double Orb, Dictionary<ChartPoints, ProgPointConfigSpecs> ProgPoints);

/// <summary>Configuration for symbolic directions.</summary>
/// <param name="Orb">Orb between progressive and radix contacts.</param>
/// <param name="TimeKey">Time key for symbolic directions.</param>
/// <param name="ProgPoints">Avaialable points.</param>
public record ConfigProgSymDir(double Orb, SymbolicKeys TimeKey,
    Dictionary<ChartPoints, ProgPointConfigSpecs> ProgPoints);

/// <summary>Configuration details for a progressive point.</summary>
/// <param name="IsUsed">True if selected, otherwise false.</param>
/// <param name="Glyph">Character for the glyph, space if no glyph is available.</param>
/// <param name="Glyph"></param>
public record ProgPointConfigSpecs(bool IsUsed, char Glyph);