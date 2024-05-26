// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Time keys for primary directions.</summary>
public enum PrimDirTimeKeys
{
   Naibod = 0, Ptolemy = 1, Brahe = 2, Placidus = 3, VanDam = 4 
}

/// <summary>Details for time keys for primary directions.</summary>
/// <param name="RbKey">Key to name in resource bundle.</param>
public record PrimDirTimeKeysDetails(string RbKey);

/// <summary>Extension class for the enum PrimDirTimeKeys.</summary>
public static class PrimDirTimeKeysExtensions
{
    /// <summary>Retrieve details for time keys for primary directions.</summary>
    /// <param name="timeKey">The time key.</param>
    /// <returns>Details for the time key.</returns>
    public static PrimDirTimeKeysDetails GetDetails(this PrimDirTimeKeys timeKey)
    {
        return timeKey switch
        {
            PrimDirTimeKeys.Naibod => new PrimDirTimeKeysDetails("ref_primdirtimekey_naibod"),
            PrimDirTimeKeys.Ptolemy => new PrimDirTimeKeysDetails("ref_primdirtimekey_ptolemy"),
            PrimDirTimeKeys.Brahe => new PrimDirTimeKeysDetails("ref_primdirtimekey_brahe"),
            PrimDirTimeKeys.Placidus => new PrimDirTimeKeysDetails("ref_primdirtimekey_placidus"),
            PrimDirTimeKeys.VanDam => new PrimDirTimeKeysDetails("ref_primdirtimekey_vandam"),
            _ => throw new ArgumentException("PrimDirTimeKEy unknown : " + timeKey)
        };
    }
    
    /// <summary>Retrieve details for items in the enum PrimDirTimeKeys.</summary>
    /// <returns>All details.</returns>
    public static List<PrimDirTimeKeysDetails> AllDetails()
    {
        return (from PrimDirTimeKeys currentKey in Enum.GetValues(typeof(PrimDirTimeKeys))
            select currentKey.GetDetails()).ToList();
    }

    /// <summary>Find primary directions time key for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The time key for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PrimDirTimeKeys PrimDirTimeKeyForIndex(int index)
    {
        foreach (PrimDirTimeKeys currentKey in Enum.GetValues(typeof(PrimDirTimeKeys)))
        {
            if ((int)currentKey == index) return currentKey;
        }
        Log.Error("PrimDirTimeKeys.PrimDirTimeKeyForIndex(): Could not find time key for index : {Index}", index);
        throw new ArgumentException("Wrong index for PrimDirTimeKEys");
    }
    
}