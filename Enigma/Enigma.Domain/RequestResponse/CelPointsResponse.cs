// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;

namespace Enigma.Domain.RequestResponse;

public record CelPointsResponse : ValidatedResponse
{
    public List<FullCelPointPos> CelPointPositions { get; }

    public CelPointsResponse(List<FullCelPointPos> celPointPositions, bool success, string errorText) : base(success, errorText)
    {
        CelPointPositions = celPointPositions;
    }
}

