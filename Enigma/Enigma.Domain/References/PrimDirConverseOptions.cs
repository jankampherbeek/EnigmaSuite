// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Converse options for primary directions.</summary>
public enum PrimDirConverseOptions
{
    None = 0, ConverseOriginal = 1, ConverseModern = 2
}

/// <summary>Details for converse options for primary directions.</summary>
/// <param name="RbKey">Key to name in resource bundle.</param>
public record PrimDirConverseOptionDetails(string RbKey);

/// <summary>Extension class for the enum PrimDirConverseOptions.</summary>
public static class PrimDirConverseOptionsExtensions
{
    /// <summary>Retrieve details for primary directions converse options.</summary>
    /// <param name="option">The option for converse directions.</param>
    /// <returns>Details for the option.</returns>
    public static PrimDirConverseOptionDetails GetDetails(this PrimDirConverseOptions option)
    {
        return option switch
        {
            PrimDirConverseOptions.None => new PrimDirConverseOptionDetails("ref.primdirconverseoption.none"),
            PrimDirConverseOptions.ConverseOriginal => new PrimDirConverseOptionDetails("ref.primdirconverseoption.original"),
            PrimDirConverseOptions.ConverseModern => new PrimDirConverseOptionDetails("ref.primdirconverseoption.modern"),
            _ => throw new ArgumentException("PrimDirConverseOption unknown : " + option)
        };
    }
    
    /// <summary>Retrieve details for items in the enum PrimDirConverseOptions.</summary>
    /// <returns>All details.</returns>
    public static List<PrimDirConverseOptionDetails> AllDetails()
    {
        return (from PrimDirConverseOptions currentOption in Enum.GetValues(typeof(PrimDirConverseOptions))
            select currentOption.GetDetails()).ToList();
    }

    /// <summary>Find converse option for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The option for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PrimDirConverseOptions PrimDirConverseOptionForIndex(int index)
    {
        foreach (PrimDirConverseOptions currentOption in Enum.GetValues(typeof(PrimDirConverseOptions)))
        {
            if ((int)currentOption == index) return currentOption;
        }
        Log.Error("PrimDirConverseOptions.PrimDirConverseOptionForIndex(): Could not find converse option for index : {Index}", index);
        throw new ArgumentException("Wrong index for PrimDirConverseOptions");
    }
    
}