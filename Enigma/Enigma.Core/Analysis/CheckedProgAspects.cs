// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Analysis;



/// <summary>Helper class for finding aspects for progressions.</summary>
public interface ICheckedProgAspects
{
    /// <summary>Check if a given distance is within orb for one or more aspects.</summary>
    /// <remarks>PRE: Distance positive and distance max 180.0, orb positive and max 30.0,
    /// supportedAspects contains minimal one aspect.</remarks>
    /// <param name="distance">The distance to check.</param>
    /// <param name="orb">The orb to apply.</param>
    /// <param name="supportedAspects">The aspects to check.</param>
    /// <returns>Zero, one or more aspects that are within orb for the given distance.</returns>
    Dictionary<AspectTypes, double> CheckAspects(double distance, double orb, List<AspectTypes> supportedAspects);
}



/// <inheritdoc/>
public class CheckedProgAspects: ICheckedProgAspects
{
    private const double Zero = 0.0;
    private const double MaxOrb = 30.0;
    private const double SemiCircle = 180.0;
    
    /// <inheritdoc/>
    public Dictionary<AspectTypes, double> CheckAspects(double distance, double orb, List<AspectTypes> supportedAspects)
    {
        if (distance < Zero || distance > SemiCircle || orb <= Zero || orb > MaxOrb || supportedAspects.Count == 0) 
            throw new ArgumentException("Wrong input for CheckedProgAspects.CheckAspects");
        Dictionary<AspectTypes, double> aspectsAndOrb = new();
        foreach (var aspectType in supportedAspects)
        {
            double aspectAngle = aspectType.GetDetails().Angle;
            double actualOrb = Math.Abs(aspectAngle - distance);
            if ( actualOrb <= orb) aspectsAndOrb.Add(aspectType, actualOrb);
        }
        return aspectsAndOrb;
    }
}