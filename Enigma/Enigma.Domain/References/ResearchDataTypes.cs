// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Types of external data that can be usd for import and be used in a research project. </summary>
public enum ResearchDataTypes
{
    StandardEnigma = 0, PlanetDance = 1
    
   
}

/// <summary>Details for a ResearchDataType.</summary>
/// <param name="DataType">The type of data.</param>
/// <param name="Name">Name of the data type.</param>
public record ResearchDataTypesDetails(ResearchDataTypes DataType, string Name);



/// <summary>Extension class for the enum ResearchDataTypes.</summary>
public static class ResearchDataTypesExtensions
{
    /// <summary>Retrieve details for data type.</summary>
    /// <param name="dataType">The data type.</param>
    /// <returns>Details for the data type.</returns>
    public static ResearchDataTypesDetails GetDetails(this ResearchDataTypes dataType)
    {
        return dataType switch
        {
            ResearchDataTypes.StandardEnigma => new ResearchDataTypesDetails(dataType, "Enigma standard format"),
            ResearchDataTypes.PlanetDance => new ResearchDataTypesDetails(dataType, "PlanetDance: exported data"),
            _ => throw new ArgumentException("Research data type unknown : " + dataType)
        };
    }

    /// <summary>Retrieve details for items in the enum ResearchDataTypes.</summary>
    /// <returns>All details.</returns>
    public static List<ResearchDataTypesDetails> AllDetails()
    {
        return (from ResearchDataTypes currentDataType in Enum.GetValues(typeof(ResearchDataTypes)) 
            select currentDataType.GetDetails()).ToList();
    }


    /// <summary>Find research data type for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The data type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ResearchDataTypes DataTypeForIndex(int index)
    {
        foreach (ResearchDataTypes currentDataType in Enum.GetValues(typeof(ResearchDataTypes)))
        {
            if ((int)currentDataType == index) return currentDataType;
        }
        Log.Error("ResearchDataTypes.DataTypeForIndex(): Could not find data type for index : {Index}", index);
        throw new ArgumentException("Wrong index for ResearchDataTypes");
    }

}
