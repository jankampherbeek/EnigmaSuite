// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>Configuration for progressive techniques.</summary>
public sealed class ConfigProg
{
    public ConfigProgTransits ConfigTransits { get; }
    public ConfigProgSecDir ConfigSecDir { get; }
    public ConfigProgSymDir ConfigSymDir { get; }
    public ConfigProgPrimDir ConfigPrimDir { get; }
    

    /// <summary>Configuration for progressive techniques.</summary>
    /// <param name="configTransits">Configuration for transits.</param>
    /// <param name="configSecDir">Configuration for secondary directions.</param>
    /// <param name="configSymDir">Configuration for symbolic directions.</param>
    /// <param name="configPrimDir">Configuration for primary directions.</param>
    public ConfigProg(
        ConfigProgTransits configTransits, 
        ConfigProgSecDir configSecDir, 
        ConfigProgSymDir configSymDir,
        ConfigProgPrimDir configPrimDir)
    {
        ConfigTransits = configTransits;
        ConfigSecDir = configSecDir;
        ConfigSymDir = configSymDir;
        ConfigPrimDir = configPrimDir;
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


/// <summary>Configuration for primary directions.</summary>
/// <param name="Method">Primary method (Placidus, Topocentric etc.).</param>
/// <param name="Approach">Approach (in mundo, in zodiaco).</param>
/// <param name="TimeKey">Time key.</param>
/// <param name="Significators">Significators.</param>
/// <param name="Promissors">Promissors.</param>
public record ConfigProgPrimDir(
    PrimDirMethods Method,
    PrimDirApproaches Approach,
    PrimDirTimeKeys TimeKey,
    Dictionary<ChartPoints, ProgPointConfigSpecs> Significators, 
    Dictionary<ChartPoints, ProgPointConfigSpecs> Promissors
    );



/// <summary>Configuration details for a progressive point.</summary>
/// <param name="IsUsed">True if selected, otherwise false.</param>
/// <param name="Glyph">Character for the glyph, space if no glyph is available.</param>
/// <param name="Glyph"></param>
public record ProgPointConfigSpecs(bool IsUsed, char Glyph);