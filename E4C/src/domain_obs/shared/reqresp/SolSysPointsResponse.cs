// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.Shared.ReqResp;
using System.Collections.Generic;

namespace E4C.domain.shared.reqresp;

public record SolSysPointsResponse: ValidatedResponse
{
    public List<FullSolSysPointPos> SolarSystemPointPositions { get; }

    public SolSysPointsResponse(List<FullSolSysPointPos> solarSystemPointPositions, bool success, string errorText): base(success, errorText)
    {
        SolarSystemPointPositions = solarSystemPointPositions;
    }
}

