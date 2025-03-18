// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.References;

public enum ApogeeTypes
{
    Interpolated = 0,
    Corrected = 1,
    Duval = 2
}

/// <summary>Details for an Apogee type</summary>
/// <param name="Type">Type from enum 'ApogeeTypes'.</param>
/// <param name="RbKey">Key to name for this aspect in resource bundle.</param>
public record ApogeeTypeDetails(ApogeeTypes Type, string RbKey);

public static class ApogeeTypesExtensions
{
    
      /// <summary>Retrieve details for apogee type.</summary>
    /// <param name="apogeeType">The apogee type.</param>
    /// <returns>Details for the apogee type.</returns>
    public static ApogeeTypeDetails GetDetails(this ApogeeTypes apogeeType)
    {
        return apogeeType switch
        {
            ApogeeTypes.Corrected => new ApogeeTypeDetails(apogeeType, "ref.apogeetype.corrected"),
            ApogeeTypes.Duval => new ApogeeTypeDetails(apogeeType, "ref.apogeetype.duval"),
            ApogeeTypes.Interpolated => new ApogeeTypeDetails(apogeeType, "ref.apogeetype.interpolated"),
            _ => throw new ArgumentException("ApogeeType unknown : " + apogeeType)
        };
    }
     
      
    /// <summary>Retrieve details for items in the enum AspectTypes.</summary>
    /// <returns>All details.</returns>
    public static IEnumerable<ApogeeTypeDetails> AllDetails()
    {
        return (from ApogeeTypes currentApogeeType in Enum.GetValues(typeof(ApogeeTypes)) select currentApogeeType.GetDetails()).ToList();
    }
    
    
    /// <summary>Find apogee type for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The apogee type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ApogeeTypes ApogeeTypeForIndex(int index)
    {
        foreach (ApogeeTypes currApogee in Enum.GetValues(typeof(ApogeeTypes)))
        {
            if ((int)currApogee == index) return currApogee;
        }
        throw new ArgumentException("Could not find apogee type");
    }
}