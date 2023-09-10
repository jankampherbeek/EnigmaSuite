// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.References;

namespace Enigma.Core.Analysis;

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