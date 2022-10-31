// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;

namespace Enigma.Domain.RequestResponse;

public record SolSysPointsResponse : ValidatedResponse
{
    public List<FullSolSysPointPos> SolarSystemPointPositions { get; }

    public SolSysPointsResponse(List<FullSolSysPointPos> solarSystemPointPositions, bool success, string errorText) : base(success, errorText)
    {
        SolarSystemPointPositions = solarSystemPointPositions;
    }
}

