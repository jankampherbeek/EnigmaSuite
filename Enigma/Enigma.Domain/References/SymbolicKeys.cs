// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Time keys for symbolic directions.</summary>
public enum SymbolicKeys
{
    OneDegree = 0,
    TrueSun = 1,
    MeanSun = 2
}


/// <summary>Details for a symbolic time key.</summary>
/// <param name="SymbolicKey">Time key.</param>
/// <param name="RbKey">Key to descriptive text in resource bundle.</param>
public record SymbolicKeyDetails(SymbolicKeys SymbolicKey, string RbKey);


/// <summary>Extension class for enum SymbolicKeys.</summary>
public static class SymbolicKeyExtensions
{
    /// <summary>Retrieve details for symbolic time key.</summary>
    /// <param name="symbolicKey">The symbolic time key.</param>
    /// <returns>Details for the symbolic time key.</returns>
    public static SymbolicKeyDetails GetDetails(this SymbolicKeys symbolicKey)
    {
        return symbolicKey switch
        {
            SymbolicKeys.OneDegree => new SymbolicKeyDetails(symbolicKey, "ref.symdirtimekey.onedegree"),
            SymbolicKeys.MeanSun => new SymbolicKeyDetails(symbolicKey, "ref.symdirtimekey.meansun"),
            SymbolicKeys.TrueSun => new SymbolicKeyDetails(symbolicKey, "ref.symdirtimekey.truesun"),
            _ => throw new ArgumentException("Symbolic time key unknown : " + symbolicKey)
        };
    }

    /// <summary>Retrieve details for items in the enum SymbolicKeys.</summary>
    /// <returns>All details.</returns>
    public static List<SymbolicKeyDetails> AllDetails()
    {
        return (from SymbolicKeys symKeyCurrent in Enum.GetValues(typeof(SymbolicKeys))
            select symKeyCurrent.GetDetails()).ToList();
    }

    /// <summary>Find symbolic time key for a given index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>The symbolic time key.</returns>
    /// <exception cref="ArgumentException">Thrown if the time key could not be found.</exception>
    public static SymbolicKeys SymbolicKeysForIndex(int index)
    {
        foreach (SymbolicKeys symKeyCurrent in Enum.GetValues(typeof(SymbolicKeys)))
        {
            if ((int)symKeyCurrent == index) return symKeyCurrent;
        }

        Log.Error("SymbolicKeys.SymbolicKeyForIndex(): Could not find Symbolic Key for index : {Index}", index);
        throw new ArgumentException("Wrong index for symbolic key");
    }
}
