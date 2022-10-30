// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.ReqResp;

namespace Enigma.Domain.RequestResponse;

/// <summary>
/// Complete calculation results for a full chart.
/// </summary>
public record ChartAllPositionsResponse : ValidatedResponse
{
    public int MyProperty { get; set; }
    public List<FullSolSysPointPos> SolarSystemPointPositions { get; }
    public FullHousesPositions? MundanePositions { get; }

    public ChartAllPositionsResponse(List<FullSolSysPointPos> solarSystemPointPositions, FullHousesPositions? mundanePositions, bool success, string errorText) : base(success, errorText)
    {
        SolarSystemPointPositions = solarSystemPointPositions;
        MundanePositions = mundanePositions;
        Success = success;
        ErrorText = errorText;
    }


}

