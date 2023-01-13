// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>Projection of the positions of celestial bodies.</summary>
/// <remarks>
/// TwoDimensional: the default type of projection to a 2-dimensional chart.
/// ObliqueLongitude: a correction for the mundane position, also called 'true location', as used by the School of Ram.
/// </remarks>
public enum ProjectionTypes
{
    TwoDimensional = 0, ObliqueLongitude = 1
}



/// <summary>Details for a Projection Type.</summary>
/// <param name="ProjectionType">Instance of the enum ProjectionTypes.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record ProjectionTypeDetails(ProjectionTypes ProjectionType, string TextId);


/// <summary>Extension class for enum ProjectionTypes.</summary>
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
    /// <returns>All details.</returns>
    public static List<ProjectionTypeDetails> AllDetails(this ProjectionTypes _)
    {
        var allDetails = new List<ProjectionTypeDetails>();
        foreach (ProjectionTypes currentProjType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            allDetails.Add(currentProjType.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find projection type for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The projection type for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ProjectionTypes ProjectionTypeForIndex(this ProjectionTypes _, int index)
    {
        foreach (ProjectionTypes projectionType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            if ((int)projectionType == index) return projectionType;
        }
        string errorText = "ProjectionTypes.ProjectionTypeForIndex(): Could not find ProjectionType for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }
}


