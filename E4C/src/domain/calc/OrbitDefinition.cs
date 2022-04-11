

// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.domain.calc;

/// <summary>
/// Data for an orbit-definition. Used for calcualtion of celestial (hypothetical) planets that are not supported by the SE.
/// </summary>
public record OrbitDefinition
{
    public readonly double[] MeanAnomaly;
    public readonly double[] Eccentricity;
    public readonly double SemiMajorAxis;
    public readonly double[] ArgumentPerihelion;
    public readonly double[] AscendingNode;
    public readonly double[] Inclination;

    /// <summary>
    /// Constructor for record ORbitDefinition.
    /// </summary>
    /// <param name="meanAnomaly">Array with three elements for the mean anomaly.</param>
    /// <param name="eccentricity">Array with three elements for the eccentricity.</param>
    /// <param name="semiMajorAxis">Value for the semi major axis.</param>
    /// <param name="argumentPerihelion">Array with three elements for the argument of the perihelion.</param>
    /// <param name="ascendingNode">Array with three elements for the ascending node.</param>
    /// <param name="inclination">Array with three elements for the inclination.</param>
    public OrbitDefinition(double[] meanAnomaly, double[] eccentricity, double semiMajorAxis, double[] argumentPerihelion, double[] ascendingNode, double[] inclination)
    {
        MeanAnomaly = meanAnomaly;
        Eccentricity = eccentricity;
        SemiMajorAxis = semiMajorAxis;
        ArgumentPerihelion = argumentPerihelion;
        AscendingNode = ascendingNode;
        Inclination = inclination;
    }

}