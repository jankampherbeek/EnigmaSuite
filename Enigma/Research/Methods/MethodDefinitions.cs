// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Research.Interfaces;

namespace Enigma.Research.Methods;


public class CountSignsMethodDefinition : IMethodDefinitions
{
    public Ayanamshas Ayanamsha { get; }
    public ObserverPositions ObserverPosition { get; }
    public HouseSystems HouseSystem { get; }
    public ProjectionTypes ProjectionType { get; }

    public List<int> SelectedSigns { get; }
    public List<SolarSystemPoints> SelectedCelPoints { get; }

    public CountSignsMethodDefinition(Ayanamshas ayanamsha, ObserverPositions observerPosition, HouseSystems houseSystem,
        ProjectionTypes projectionType, List<int> selectedSigns, List<SolarSystemPoints> selectedCelPoints) {

        Ayanamsha = ayanamsha;
        ObserverPosition = observerPosition;
        HouseSystem = houseSystem;
        ProjectionType = projectionType;
        SelectedSigns = selectedSigns;
        SelectedCelPoints = selectedCelPoints;
    }


}