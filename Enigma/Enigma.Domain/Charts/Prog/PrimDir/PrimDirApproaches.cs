// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Charts.Prog.PrimDir;

/// <summary>Approaches (in mundo or in zodiaco) for primary directions.</summary>
public enum PrimDirApproaches
{
    Mundane = 0, Zodiacal = 1
}

/// <summary>Details for approaches for primary directions.</summary>
/// <param name="RbKey">Key to name in resource bundle.</param>
public record PrimDirApproachesDetails(string RbKey);

/// <summary>Extension class for the enum PrimDirApproaches.</summary>
public static class PrimDirApproachesExtensions
{
    /// <summary>Retrieve details for approaches for primary directions.</summary>
    /// <param name="approach">The approach.</param>
    /// <returns>Details for the approach.</returns>
    public static PrimDirApproachesDetails GetDetails(this PrimDirApproaches approach)
    {
        return approach switch
        {
            PrimDirApproaches.Mundane => new PrimDirApproachesDetails("ref.primdirapproach.inmundo"),
            PrimDirApproaches.Zodiacal => new PrimDirApproachesDetails("ref.primdirapproach.inzodiaco"),
            _ => throw new ArgumentException("PrimDirApproaches unknown : " + approach)
        };
    }
    
    /// <summary>Retrieve details for items in the enum PrimDirApproaches.</summary>
    /// <returns>All details.</returns>
    public static List<PrimDirApproachesDetails> AllDetails()
    {
        return (from PrimDirApproaches currentApproach in Enum.GetValues(typeof(PrimDirApproaches))
            select currentApproach.GetDetails()).ToList();
    }

    /// <summary>Find approach for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The approach for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PrimDirApproaches PrimDirApproachForIndex(int index)
    {
        foreach (PrimDirApproaches currentApproach in Enum.GetValues(typeof(PrimDirApproaches)))
        {
            if ((int)currentApproach == index) return currentApproach;
        }
        Log.Error("PrimDirApproaches.PrimDirApproachForIndex(): Could not find approach for index : {Index}", index);
        throw new ArgumentException("Wrong index for PrimDirApproaches");
    }
    
}