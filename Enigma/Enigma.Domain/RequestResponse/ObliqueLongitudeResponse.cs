﻿// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;

namespace Enigma.Domain.RequestResponse;

public record ObliqueLongitudeResponse : ValidatedResponse
{
    public List<NamedEclipticLongitude> SolSysPointLongitudes { get; }

    public ObliqueLongitudeResponse(List<NamedEclipticLongitude> solSysPointLongitudes, bool success, string errorText) : base(success, errorText)
    {
        SolSysPointLongitudes = solSysPointLongitudes;
    }
}