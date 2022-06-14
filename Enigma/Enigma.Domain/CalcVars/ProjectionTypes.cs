// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.CalcVars;

/// <summary>Projection of the positions of celestial bodies.</summary>
/// <remarks>
/// twoDimensional: the default type of projection to a 2-dimensional chart.
/// obliqueLongitude: a correction for the mundane position, also called 'true location', as used by the School of Ram.
/// </remarks>
public enum ProjectionTypes
{
    twoDimensional = 0, obliqueLongitude = 1
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

/// <summary>Specification for a Projection Type.</summary>
public interface IProjectionTypeDetailSpecifications          // TODO rename to IProjectionTypeSpecifications
{
    /// <summary>Returns the details for a Projection Type.</summary>
    /// <param name="projectionType">Instance from the enum ProjectionTypes.</param>
    /// <returns>A record PRojectionTypeDetails with the specifications.</returns>
    public ProjectionTypeDetails DetailsForProjectionType(ProjectionTypes projectionType);

    /// <summary>Returns a value from the enum ProjectionTypes that corresponds with an index.</summary>
    /// <param name="projectionTypeIndex">The index for the requested item from ProjectionTypes. 
    /// Throws an exception if no ProjectionTypes for the given index does exist.</param>
    /// <returns>Instance from enum ProjectionTypes that corresponds with the given index.</returns>
    public ProjectionTypes ProjectionTypeForIndex(int projectionTypeIndex);
}

/// <inheritdoc/>
public class ProjectionTypeDetailSpecifications : IProjectionTypeDetailSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the projection type was not recognized.</exception>
    public ProjectionTypeDetails DetailsForProjectionType(ProjectionTypes projectionType)
    {
        return projectionType switch
        {
            ProjectionTypes.twoDimensional => new ProjectionTypeDetails(projectionType, ""),
            ProjectionTypes.obliqueLongitude => new ProjectionTypeDetails(projectionType, ""),
            _ => throw new ArgumentException("ProjectionType unknown : " + projectionType.ToString())
        };
    }

    public ProjectionTypes ProjectionTypeForIndex(int projectionTypeIndex)
    {
        foreach (ProjectionTypes projectionType in Enum.GetValues(typeof(ProjectionTypes)))
        {
            if ((int)projectionType == projectionTypeIndex) return projectionType;
        }
        throw new ArgumentException("Could not find ProjectionTypes for index : " + projectionTypeIndex);
    }
}
