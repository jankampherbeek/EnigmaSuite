// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Time keys for primary directions.</summary>
public enum PrimaryKeys
{
    Ptolemy = 1,        // 1 degree longitude
    Naibod = 2,         // mean daily movement of Sun in longitude
    Brahe = 3,          // equatorial solar arc at birthday
    Placidus = 4,       // equatorial secundary solar arc 
    VanDam = 5          // ecliptical secundary solar arc
}

/// <summary>Details for a primary time key.</summary>
/// <param name="PrimaryKey">Time key.</param>
/// <param name="Text">Descriptive text.</param>
public record PrimaryKeyDetails(PrimaryKeys PrimaryKey, string Text);


/// <summary>Extension class for enum PrimaryKeys.</summary>
public static class PrimaryKeyExtensions
{
    /// <summary>Retrieve details for primary time key.</summary>
    /// <param name="primaryKey">The primary time key.</param>
    /// <returns>Details for the primary time key.</returns>
    public static PrimaryKeyDetails GetDetails(this PrimaryKeys primaryKey)
    {
        return primaryKey switch
        {
            PrimaryKeys.Ptolemy => new PrimaryKeyDetails(primaryKey, "Ptolemy"),
            PrimaryKeys.Naibod => new PrimaryKeyDetails(primaryKey, "Naibod"),
            PrimaryKeys.Brahe => new PrimaryKeyDetails(primaryKey, "Brahe"),
            PrimaryKeys.Placidus => new PrimaryKeyDetails(primaryKey, "Placidus"),
            PrimaryKeys.VanDam => new PrimaryKeyDetails(primaryKey, "Van Dam"),
            _ => throw new ArgumentException("Primary time key unknown : " + primaryKey)
        };
    }


    /// <summary>Retrieve details for items in the enum PrimaryKeys.</summary>
    /// <returns>All details.</returns>
    public static List<PrimaryKeyDetails> AllDetails()
    {
        return (from PrimaryKeys primKeyCurrent in Enum.GetValues(typeof(PrimaryKeys)) 
            select primKeyCurrent.GetDetails()).ToList();
    }

    /// <summary>Find primary time key for a given index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>The primary time key.</returns>
    /// <exception cref="ArgumentException">Thrown if the time key could not be found.</exception>
    public static PrimaryKeys PrimaryKeysForIndex(int index)
    {
        foreach (PrimaryKeys primKeyCurrent in Enum.GetValues(typeof(PrimaryKeys)))
        {
            if ((int)primKeyCurrent == index) return primKeyCurrent;
        }
        Log.Error("PrimaryKeys.PrimaryKeyForIndex(): Could not find Primary Key for index : {Index}", index);
        throw new ArgumentException("Wrong index for primary key");
    }

}
