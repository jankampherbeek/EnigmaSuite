// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.positions;
using E4C.Models.Domain;
using System.Collections.Generic;

namespace E4C.domain.shared.reqresp;

public record SolSysPointsResponse(List<FullSolSysPointPos>? SolarSystemPointPositions, bool Success, string ErrorText) : ValidatedResponse(Success, ErrorText)
{
    public readonly List<FullSolSysPointPos>? SolarSystemPointPositions;
}

