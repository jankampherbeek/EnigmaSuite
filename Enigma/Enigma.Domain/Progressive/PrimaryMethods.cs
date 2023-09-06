// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Progressive;

public enum PrimaryMethods
{
    PlacidusMundane = 0, PlacidusEcliptical = 1, RegiomontanusMundane = 2, RegiomontanusEcliptical = 3 
}

public enum PrimaryCoordinateSystem
{
    Mundane = 0, Ecliptical = 1
}


public record PrimaryMethodDetails(PrimaryMethods Method, PrimaryCoordinateSystem CoordSystem, string MethodName);


/// <summary>Extension class for PrimaryMethods.</summary>
public static class PrimaryMethodsExtensions
{
    /// <summary>Retrieve details for a primary method.</summary>
    /// <param name="method">The primary method.</param>
    /// <returns>Details for the primary method.</returns>
    public static PrimaryMethodDetails GetDetails(this PrimaryMethods method)
    {
        return method switch
        {
            PrimaryMethods.PlacidusMundane => new PrimaryMethodDetails(method, PrimaryCoordinateSystem.Mundane,
                "Placidus - mundane"),
            PrimaryMethods.PlacidusEcliptical => new PrimaryMethodDetails(method, PrimaryCoordinateSystem.Ecliptical,
                "Placidus - ecliptical"),
            PrimaryMethods.RegiomontanusMundane => new PrimaryMethodDetails(method, PrimaryCoordinateSystem.Mundane,
                "Regiomontanus - mundane"),
            PrimaryMethods.RegiomontanusEcliptical => new PrimaryMethodDetails(method,
                PrimaryCoordinateSystem.Ecliptical, "Regiomontanus - ecliptical"),
            _ => throw new ArgumentException("Primary method unknown : " + method)
        };
    }

    /// <summary>Retrieve details for items in the enum PrimaryMethods.</summary>
    /// <returns>All details.</returns>
    public static List<PrimaryMethodDetails> AllDetails()
    {
        return (from PrimaryMethods currentMethod in Enum.GetValues(typeof(PrimaryMethods))
            select currentMethod.GetDetails()).ToList();
    }

    /// <summary>Find primary method for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The method for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static PrimaryMethods MethodForIndex(int index)
    {
        foreach (PrimaryMethods currentMethod in Enum.GetValues(typeof(PrimaryMethods)))
        {
            if ((int)currentMethod == index) return currentMethod;
        }
        Log.Error("PrimaryMethods.PointForIndex(): Could not find method for index : {Index}", index);
        throw new ArgumentException("Wrong index for PrimaryMethods");
    }
    
}
