﻿// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.CalcVars;

/// <summary>Supported points in the Solar System (Planets, lights, Plutoids etc.).</summary>
public enum MundanePoints
{
    Ascendant = 0, Mc = 1, EastPoint = 2, Vertex = 3
}

/// <summary>Details for a Mundane Point.</summary>
public record MundanePointDetails
{
    readonly public MundanePoints MundanePoint;
    readonly public string TextId;
    readonly public string TextIdAbbreviated;

    /// <param name="mundanePoint">The Mundane Point.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    /// <param name=textIdAbbreviated">Abbreviated version for textId.</param>
    public MundanePointDetails(MundanePoints mundanePoint, string textId, string textIdAbbreviated)
    {
        MundanePoint = mundanePoint;
        TextId = textId;
        TextIdAbbreviated = textIdAbbreviated;
    }
}

/// <summary>Specifications for a Mundane Point.</summary>
public interface IMundanePointSpecifications
{
    /// <summary>Returns the specifications for a given Mundane Point.</summary>
    /// <param name="point">The mundane point for which to find the details.</param>
    /// <returns>A record MundanePointDetails with the specifications.</returns>
    public MundanePointDetails DetailsForPoint(MundanePoints point);

    /// <summary>Returns the specifications of a Mundane Point for a given id.</summary>
    /// <param name="pointId">The id of the mundane point for which to find the details.</param>
    /// <returns>A record MundanePointDetails with the specifications.</returns>
    public MundanePointDetails DetailsForPoint(int pointId);
}





/// <inheritdoc/>
public class MundanePointSpecifications : IMundanePointSpecifications
{
    /// <inheritdoc/>
    public MundanePointDetails DetailsForPoint(MundanePoints point)
    {
        return point switch
        {
            MundanePoints.Ascendant => new MundanePointDetails(point, "ref.enum.mundanepoint.id.asc", "ref.enum.mundanepoint.idabbr.asc"),
            MundanePoints.Mc => new MundanePointDetails(point, "ref.enum.mundanepoint.id.mc", "ref.enum.mundanepoint.idabbr.mc"),
            MundanePoints.EastPoint => new MundanePointDetails(point, "ref.enum.mundanepoint.id.eastpoint", "ref.enum.mundanepoint.idabbr.eastpoint"),
            MundanePoints.Vertex => new MundanePointDetails(point, "ref.enum.mundanepoint.id.vertex", "ref.enum.mundanepoint.idabbr.vertex"),
            _ => throw new ArgumentException("MundanePoint unknown : " + point.ToString())
        };

    }

    /// <inheritdoc/>
    public MundanePointDetails DetailsForPoint(int pointId)
    {
        MundanePoints point = (MundanePoints) pointId;
        return DetailsForPoint(point);
    }
}