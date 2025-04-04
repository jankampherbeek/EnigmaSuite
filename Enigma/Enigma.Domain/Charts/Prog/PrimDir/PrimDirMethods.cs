// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Charts.Prog.PrimDir;

/// <summary>Methods for primary directions.</summary>
public enum PrimDirMethods
{
    Placidus = 0, Regiomontanus = 1
}

/// <summary>Details for methods for primary directions.</summary>
/// <param name="RbKey">Key to name in resource bundle.</param>
public record PrimDirMethodDetails(string RbKey);

/// <summary>Extension class for the enum PrimDirMethods.</summary>
public static class PrimDirMethodsExtensions
{
    /// <summary>Retrieve details for primary directions methods.</summary>
    /// <param name="method">The primary directions method.</param>
    /// <returns>Details for the primary directions method.</returns>
    public static PrimDirMethodDetails GetDetails(this PrimDirMethods method)
    {
        return method switch
        {
            PrimDirMethods.Placidus => new PrimDirMethodDetails("ref.primdirmethod.placidus"),
            PrimDirMethods.Regiomontanus => new PrimDirMethodDetails("ref.primdirmethod.regiomontanus"),
            _ => throw new ArgumentException("PrimDirMethod unknown : " + method)
        };
    }
    
    /// <summary>Retrieve details for items in the enum PrimDirMethods.</summary>
    /// <returns>All details.</returns>
    public static List<PrimDirMethodDetails> AllDetails()
    {
        return (from PrimDirMethods currentMethod in Enum.GetValues(typeof(PrimDirMethods))
            select currentMethod.GetDetails()).ToList();
    }

    /// <summary>Find primary directions method for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The method for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PrimDirMethods PrimDirMethodForIndex(int index)
    {
        foreach (PrimDirMethods currentMethod in Enum.GetValues(typeof(PrimDirMethods)))
        {
            if ((int)currentMethod == index) return currentMethod;
        }
        Log.Error("PrimDirMethods.PrimDirMethodForIndex(): Could not find method for index : {Index}", index);
        throw new ArgumentException("Wrong index for PrimDirMethods");
    }
    
}