﻿// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.positions;
using E4C.Models.Domain;
using System.Collections.Generic;

namespace E4C.domain.shared.reqresp;

/// <summary>
/// Complete calculation results for a full chart.
/// </summary>
public record FullChartResponse : ValidatedResponse
{
    public int MyProperty { get; set; }
    public List<FullSolSysPointPos> SolarSystemPointPositions { get; }
    public FullMundanePositions? MundanePositions { get; }

    public FullChartResponse(List<FullSolSysPointPos> solarSystemPointPositions, FullMundanePositions? mundanePositions, bool success, string errorText) : base(success, errorText)
    {
        SolarSystemPointPositions = solarSystemPointPositions;
        MundanePositions = mundanePositions;
        Success = success;
        ErrorText = errorText;
    }


}
