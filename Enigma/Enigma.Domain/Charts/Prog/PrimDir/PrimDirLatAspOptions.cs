// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Charts.Prog.PrimDir;

/// <summary>Options for latitude with aspects in primary directions.</summary>
public enum PrimDirLatAspOptions
{
    None = 0, Significator = 1, Promissor = 2, Bianchini = 3
}

/// <summary>Details for latitude options for aspects in primary directions.</summary>
/// <param name="RbKey">Key to name in resource bundle.</param>
public record PrimDirLatAspOptionDetails(string RbKey);

/// <summary>Extension class for the enum PrimDirLatAspOptions.</summary>
public static class PrimDirLatAspOptionsExtensions
{
    /// <summary>Retrieve details about options for latitude with aspects in primary directions.</summary>
    /// <param name="option">The option for latitude.</param>
    /// <returns>Details for the option.</returns>
    public static PrimDirLatAspOptionDetails GetDetails(this PrimDirLatAspOptions option)
    {
        return option switch
        {
            PrimDirLatAspOptions.None => new PrimDirLatAspOptionDetails("ref.primdirlataspoption.none"),
            PrimDirLatAspOptions.Significator => new PrimDirLatAspOptionDetails("ref.primdirlataspoption.significator"),
            PrimDirLatAspOptions.Promissor => new PrimDirLatAspOptionDetails("ref.primdirlataspoption.promissor"),
            PrimDirLatAspOptions.Bianchini => new PrimDirLatAspOptionDetails("ref.primdirlataspoption.bianchini"),
            _ => throw new ArgumentException("PrimDirLatAspOption unknown : " + option)
        };
    }
    
    /// <summary>Retrieve details for items in the enum PrimDirLatAspOptions.</summary>
    /// <returns>All details.</returns>
    public static List<PrimDirLatAspOptionDetails> AllDetails()
    {
        return (from PrimDirLatAspOptions currentOption in Enum.GetValues(typeof(PrimDirLatAspOptions))
            select currentOption.GetDetails()).ToList();
    }

    /// <summary>Find option for altitude for aspects in primary directions, for a given index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The option for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PrimDirLatAspOptions PrimDirLatAspOptionForIndex(int index)
    {
        foreach (PrimDirLatAspOptions currentOption in Enum.GetValues(typeof(PrimDirLatAspOptions)))
        {
            if ((int)currentOption == index) return currentOption;
        }
        Log.Error("PrimDirLatAspOptions.PrimDirLatAspOptionForIndex(): Could not find latitude option for index : {Index}", index);
        throw new ArgumentException("Wrong index for PrimDirLatAspOptions");
    }
    
}