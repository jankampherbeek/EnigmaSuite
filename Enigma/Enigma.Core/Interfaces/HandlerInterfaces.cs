// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Interfaces;

public interface IProgAspectsHandler
{
    public ProgAspectsResponse FindProgAspects(ProgAspectsRequest request);
}

/// <summary>Handles calculation of transits.</summary>
public interface ICalcTransitsHandler
{
    /// <summary>Handles calculation of transits for a specific event.</summary>
    /// <param name="request">Request with config items, date/time etc.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalculateTransits(TransitsEventRequest request);
}

/// <summary>Handles calculation of secondary directions.</summary>
public interface ICalcSecDirHandler
{
    /// <summary>Handles calculation of secondary directions for a specific event.</summary>
    /// <param name="request">Request with config items, date/time etc.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalculateSecDir(SecDirEventRequest request);
}

/// <summary>Handles calculation of symbolic directions.</summary>
public interface ICalcSymDirHandler
{
    /// <summary>Handles calculation of symbolic directions for a specific event.</summary>
    /// <param name="request">Request with config items, date/time etc.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalculateSymDir(SymDirEventRequest request);
}

