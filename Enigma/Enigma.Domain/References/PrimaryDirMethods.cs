// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

public enum PrimaryDirMethods
{
    SemiArcMundane = 0, SemiArcZodiacal = 1 
}

public enum PrimaryDirCoordinateSystem
{
    Mundane = 0, Zodiacal = 1
}


public record PrimaryDirMethodDetails(PrimaryDirMethods DirMethod, PrimaryDirCoordinateSystem CoordSystem, string MethodName);


/// <summary>Extension class for PrimaryMethods.</summary>
public static class PrimaryDirMethodsExtensions
{
    /// <summary>Retrieve details for a primary method.</summary>
    /// <param name="dirMethod">The primary method.</param>
    /// <returns>Details for the primary method.</returns>
    public static PrimaryDirMethodDetails GetDetails(this PrimaryDirMethods dirMethod)
    {
        return dirMethod switch
        {
            PrimaryDirMethods.SemiArcMundane => new PrimaryDirMethodDetails(dirMethod, PrimaryDirCoordinateSystem.Mundane,
                "Semi-arc mundane"),
            PrimaryDirMethods.SemiArcZodiacal => new PrimaryDirMethodDetails(dirMethod, PrimaryDirCoordinateSystem.Zodiacal,
                "Semi-arc zodiacal"),
            _ => throw new ArgumentException("Primary method unknown : " + dirMethod)
        };
    }

    /// <summary>Retrieve details for items in the enum PrimaryMethods.</summary>
    /// <returns>All details.</returns>
    public static List<PrimaryDirMethodDetails> AllDetails()
    {
        return (from PrimaryDirMethods currentMethod in Enum.GetValues(typeof(PrimaryDirMethods))
            select currentMethod.GetDetails()).ToList();
    }

    /// <summary>Find primary method for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The method for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PrimaryDirMethods MethodForIndex(int index)
    {
        foreach (PrimaryDirMethods currentMethod in Enum.GetValues(typeof(PrimaryDirMethods)))
        {
            if ((int)currentMethod == index) return currentMethod;
        }
        Log.Error("PrimaryMethods.PointForIndex(): Could not find method for index : {Index}", index);
        throw new ArgumentException("Wrong index for PrimaryMethods");
    }
    
}
