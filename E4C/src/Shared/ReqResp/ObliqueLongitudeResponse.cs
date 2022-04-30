// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Shared.Domain;
using System.Collections.Generic;

namespace E4C.Shared.ReqResp;

public record ObliqueLongitudeResponse : ValidatedResponse
{
    public List<NamedEclipticLongitude> SolSysPointLongitudes { get; }

    public ObliqueLongitudeResponse(List<NamedEclipticLongitude> solSysPointLongitudes, bool success, string errorText) : base(success, errorText)
    {
        SolSysPointLongitudes = solSysPointLongitudes;
    }
}
