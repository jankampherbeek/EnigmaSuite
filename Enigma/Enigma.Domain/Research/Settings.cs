// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Domain.Research;

/// <summary>Abstract parent record for research settings.</summary>
/// <param name="Ayanamsha">Ayanamsha, for tropical use Ayanamsha 'None'.</param>
/// <param name="ObserverPosition">Position of observer (helio-, geo- or topocentric).</param>
/// <param name="ProjectionType">Type of projection (standard or oblique longitude).</param>
public abstract record ResearchCalcSettings(Ayanamshas Ayanamsha,
    ObserverPositions ObserverPosition,
    ProjectionTypes ProjectionType);


/// <summary>Research settings for houses-related research.</summary>
/// <param name="Ayanamsha">Ayanamsha, for tropical use Ayanamsha 'None'.</param>
/// <param name="ObserverPosition">Position of observer (helio-, geo- or topocentric).</param>
/// <param name="ProjectionType">Type of projection (standard or oblique longitude).</param>
/// <param name="HouseSystem"></param>
public record MundaneSettings(Ayanamshas Ayanamsha,
    ObserverPositions ObserverPosition,
    ProjectionTypes ProjectionType,
    HouseSystems HouseSystem) :
    ResearchCalcSettings(Ayanamsha, ObserverPosition, ProjectionType);




