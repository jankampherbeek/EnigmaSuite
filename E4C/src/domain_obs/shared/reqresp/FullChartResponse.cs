// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Shared.Domain;
using E4C.Models.Domain;
using E4C.Shared.ReqResp;
using System.Collections.Generic;

namespace E4C.domain.shared.reqresp;

/// <summary>
/// Complete calculation results for a full chart.
/// </summary>
public record FullChartResponse : ValidatedResponse
{
    public int MyProperty { get; set; }
    public List<FullSolSysPointPos> SolarSystemPointPositions { get; }
    public FullHousesPositions? MundanePositions { get; }

    public FullChartResponse(List<FullSolSysPointPos> solarSystemPointPositions, FullHousesPositions? mundanePositions, bool success, string errorText) : base(success, errorText)
    {
        SolarSystemPointPositions = solarSystemPointPositions;
        MundanePositions = mundanePositions;
        Success = success;
        ErrorText = errorText;
    }


}

