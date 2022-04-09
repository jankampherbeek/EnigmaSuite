// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.positions;
using E4C.Models.Domain;
using System.Collections.Generic;

namespace E4C.domain.shared.reqresp;

/// <summary>
/// Complete calculation results for a full chart.
/// </summary>
public record FullChartResponse(List<FullSolSysPointPos>? SolarSystemPointPositions, FullMundanePositions? MundanePositions, bool Success, string ErrorText) : ValidatedResponse(Success, ErrorText)
{
    public readonly List<FullSolSysPointPos>? SolarSystemPointPositions;
    public readonly FullMundanePositions? mundanePositions;

}

