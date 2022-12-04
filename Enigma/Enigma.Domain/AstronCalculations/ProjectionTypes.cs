// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.AstronCalculations;

/// <summary>Projection of the positions of celestial bodies.</summary>
/// <remarks>
/// TwoDimensional: the default type of projection to a 2-dimensional chart.
/// ObliqueLongitude: a correction for the mundane position, also called 'true location', as used by the School of Ram.
/// </remarks>
public enum ProjectionTypes
{
    TwoDimensional = 0, ObliqueLongitude = 1
}


public static class ProjectionTypesExtensions
{
    /// <summary>Retrieve details for projection type.</summary>
    /// <param name="projType">The projection type, is automatically filled.</param>
    /// <returns>Details for the projection type.</returns>
    public static ProjectionTypeDetails GetDetails(this ProjectionTypes projType)
    {
      return projType switch
        {
            ProjectionTypes.ObliqueLongitude => new ProjectionTypeDetails(projType, "ref.enum.projectiontype.obliquelongitude"),
            ProjectionTypes.TwoDimensional => new ProjectionTypeDetails(projType, "ref.enum.projectiontype.twodimensional"),

            _ => throw new ArgumentException("ProjectionType unknown : " + projType.ToString())
        }; 

    }


    /// <summary>Retrieve details for items in the enum ProjectionTypes.</summary>
    /// <param name="projectionType">The projection type, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<ProjectionTypeDetails> AllDetails(this ProjectionTypes projectionType)
    {
        var allDetails = new List<ProjectionTypeDetails>();
        foreach (ProjectionTypes currentProjType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            allDetails.Add(currentProjType.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find projection type for an index.</summary>
    /// <param name="projType">Any projection type, automatically filled.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The projection type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ProjectionTypes ProjectionTypeForIndex(this ProjectionTypes projType, int index)
    {
        foreach (ProjectionTypes projectionType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            if ((int)projectionType == index) return projectionType;
        }
        throw new ArgumentException("Could not find ProjectionType for index : " + index);
    }
}


/// <summary>Details for a Projection Type.</summary>
/// <param name="ProjectionType">Instance of the enum ProjectionTypes.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record ProjectionTypeDetails(ProjectionTypes ProjectionType, string TextId);
