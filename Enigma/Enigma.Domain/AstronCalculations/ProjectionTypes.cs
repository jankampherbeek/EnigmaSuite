// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Research.Domain;
using Enigma.Domain.Interfaces;

namespace Enigma.Domain.AstronCalculations;

/// <summary>Projection of the positions of celestial bodies.</summary>
/// <remarks>
/// twoDimensional: the default type of projection to a 2-dimensional chart.
/// obliqueLongitude: a correction for the mundane position, also called 'true location', as used by the School of Ram.
/// </remarks>
public enum ProjectionTypes
{
    twoDimensional = 0, obliqueLongitude = 1
}


public static class ProjectionTypesExtensions
{
    /// <summary>Retrieve text id for resourcebundle for projection type.</summary>
    /// <param name="projType">The projection type, is automatically filled.</param>
    /// <returns>Id for the resource bundle.</returns>
    public static string TextInRbId(this ProjectionTypes projType)
    {
        return projType switch
        {
            ProjectionTypes.twoDimensional => "ref.enum.projectiontype.twodimensional",
            ProjectionTypes.obliqueLongitude => "ref.enum.projectiontype.obliquelongitude",
            _ => throw new ArgumentException("ProjectionType unknown : " + projType.ToString())
        };
    }
}


/// <summary>Details for a Projection Type.</summary>
public record ProjectionTypeDetails
{
    readonly public ProjectionTypes ProjectionType;
    readonly public string TextId;

    /// <param name="projectionType">Instance of the enum ProjectionTypes.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public ProjectionTypeDetails(ProjectionTypes projectionType, string textId)
    {
        ProjectionType = projectionType;
        TextId = textId;
    }
}



/// <inheritdoc/>
/// Obsolete.
public class ProjectionTypeSpecifications : IProjectionTypeSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the projection type was not recognized.</exception>
    public ProjectionTypeDetails DetailsForProjectionType(ProjectionTypes projectionType)
    {
        return projectionType switch
        {
            ProjectionTypes.twoDimensional => new ProjectionTypeDetails(projectionType, "ref.enum.projectiontype.twodimensional"),
            ProjectionTypes.obliqueLongitude => new ProjectionTypeDetails(projectionType, "ref.enum.projectiontype.obliquelongitude"),
            _ => throw new ArgumentException("ProjectionType unknown : " + projectionType.ToString())
        };
    }

    public ProjectionTypes ProjectionTypeForIndex(int projectionTypeIndex)
    {
        foreach (ProjectionTypes projectionType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            if ((int)projectionType == projectionTypeIndex) return projectionType;
        }
        throw new ArgumentException("Could not find ProjectionType for index : " + projectionTypeIndex);
    }
    public List<ProjectionTypeDetails> AllProjectionTypeDetails()
    {
        var allDetails = new List<ProjectionTypeDetails>();
        foreach (ProjectionTypes projectionType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            allDetails.Add(DetailsForProjectionType(projectionType));
        }
        return allDetails;
    }

}
