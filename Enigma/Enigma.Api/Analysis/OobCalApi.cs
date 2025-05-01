// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Api.Analysis;

/// <summary>API for the calculation of an Out of Bounds Calendar.</summary>
public interface IOobCalApi
{
    /// <summary>Take care of the calculation of an Out of Bounds calendar.</summary>
    /// <param name="request">Request.</param>
    /// <returns>Resulting OOB events.</returns>
    public List<OobCalEvent> CreateOobCalendar(OobCalRequest request);
}

/// <inheritdoc/>
public sealed class OobCalApi(IOobCalHandler handler) : IOobCalApi
{
    /// <inheritdoc/>
    public List<OobCalEvent> CreateOobCalendar(OobCalRequest request)
    {
        return handler.CreateOobCalendar(request);
    }
}