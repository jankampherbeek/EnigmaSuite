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
    public ConfigProgPrimDir ConfigPrimDir { get; }
    public ConfigProgSymDir ConfigSymDir { get; }
    public ConfigProgSolar ConfigSolar { get; }

    public ConfigProg(ConfigProgTransits configTransits, ConfigProgSecDir configSecDir, ConfigProgPrimDir configPrimDir,
        ConfigProgSymDir configSymDir, ConfigProgSolar configSolar)
    {
        ConfigTransits = configTransits;
        ConfigSecDir = configSecDir;
        ConfigPrimDir = configPrimDir;
        ConfigSymDir = configSymDir;
        ConfigSolar = configSolar;
    }
    
}


/// <summary>Configuration for transits.</summary>
/// <param name="Orb">Orb between progressive and radix contacts.</param>
/// <param name="ProgPoints">Avaialable points.</param>
public record ConfigProgTransits(double Orb, Dictionary<ChartPoints, ProgPointConfigSpecs> ProgPoints);

/// <summary>Configuration for secundary progressions.</summary>
/// <param name="Orb">Orb between progressive and radix contacts.</param>
/// <param name="ProgPoints">Avaialable points.</param>
public record ConfigProgSecDir(double Orb, Dictionary<ChartPoints, ProgPointConfigSpecs> ProgPoints);

/// <summary>Configuration for primary directions.</summary>
/// <param name="Orb">Orb between progressive and radix contacts.</param>
/// <param name="TimeKey">Time key for primary directions/</param>
/// <param name="DirMethod">Method for primary directions.</param>
/// <param name="IncludeConverse">True if converse directions should be included, otherwise false.</param>
/// <param name="Promissors">Available promissors.</param>
/// <param name="Significators">Available significators.</param>
public record ConfigProgPrimDir(double Orb, PrimaryKeys TimeKey, PrimaryDirMethods DirMethod, bool IncludeConverse,
    Dictionary<ChartPoints, ProgPointConfigSpecs> Promissors,
    Dictionary<ChartPoints, ProgPointConfigSpecs> Significators);

/// <summary>Configuration for symbolic directions.</summary>
/// <param name="Orb">Orb between progressive and radix contacts.</param>
/// <param name="TimeKey">Time key for symbolic directions.</param>
/// <param name="ProgPoints">Avaialable points.</param>
public record ConfigProgSymDir(double Orb, SymbolicKeys TimeKey,
    Dictionary<ChartPoints, ProgPointConfigSpecs> ProgPoints);

/// <summary>Configuration for a solar.</summary>
/// <param name="SolarMethod">Method for the solar.</param>
/// <param name="Relocate">True if relocation should be applied, otherwise false.</param>
public record ConfigProgSolar(SolarMethods SolarMethod, bool Relocate);

/// <summary>Configuration details for a progressive point.</summary>
/// <param name="IsUsed">True if selected, otherwise false.</param>
/// <param name="Glyph">Character for the glyph, space if no glyph is available.</param>
/// <param name="Glyph"></param>
public record ProgPointConfigSpecs(bool IsUsed, char Glyph);