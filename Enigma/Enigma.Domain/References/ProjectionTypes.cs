// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Projection of the positions of celestial bodies</summary>
/// <remarks>
/// TwoDimensional: the default type of projection to a 2-dimensional chart
/// ObliqueLongitude: a correction for the mundane position, also called 'true location', as used by the School of Ram
/// </remarks>
public enum ProjectionTypes
{
    TwoDimensional = 0, ObliqueLongitude = 1
}



/// <summary>Details for a Projection Type</summary>
/// <param name="ProjectionType">Instance of the enum ProjectionTypes.</param>
/// <param name="RbKey">Key to descriptive text in resource bundle.</param>
public record ProjectionTypeDetails(ProjectionTypes ProjectionType, string RbKey);


/// <summary>Extension class for enum ProjectionTypes</summary>
public static class ProjectionTypesExtensions
{
    /// <summary>Retrieve details for projection type</summary>
    /// <param name="projType">The projection type, is automatically filled</param>
    /// <returns>Details for the projection type</returns>
    public static ProjectionTypeDetails GetDetails(this ProjectionTypes projType)
    {
        return projType switch
        {
            ProjectionTypes.ObliqueLongitude => new ProjectionTypeDetails(projType, "ref.projectiontype.obliquelongitude"),
            ProjectionTypes.TwoDimensional => new ProjectionTypeDetails(projType, "ref.projectiontype.twodimensional"),
            _ => throw new ArgumentException("ProjectionType unknown : " + projType)
        };

    }

    /// <summary>Retrieve details for items in the enum ProjectionTypes</summary>
    /// <returns>All details</returns>
    public static List<ProjectionTypeDetails> AllDetails()
    {
        return (from ProjectionTypes currentProjType in Enum.GetValues(typeof(ProjectionTypes)) select currentProjType.GetDetails()).ToList();
    }


    /// <summary>Find projection type for an index</summary>
    /// <param name="index">Index to look for</param>
    /// <returns>The projection type for the index</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given</exception>
    public static ProjectionTypes ProjectionTypeForIndex(int index)
    {
        foreach (ProjectionTypes projectionType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            if ((int)projectionType == index) return projectionType;
        }
        Log.Error("ProjectionTypes.ProjectionTypeForIndex(): Could not find ProjectionType for index : {Index}", index);
        throw new ArgumentException("Instance for enum ProjectionType could not be found");
    }
}


