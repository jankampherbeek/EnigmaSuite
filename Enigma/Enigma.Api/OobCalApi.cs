// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Api;

/// <summary>API for the calculation of an Out of Bounds Calendar.</summary>
public interface IOobCalApi
{
    /// <summary>Take care of the calculation of an Out of Bounds calendar.</summary>
    /// <param name="request">Request.</param>
    /// <returns>Resulting OOB events.</returns>
    public List<OobCalEvent> CreateOobCalendar(OobCalRequest request);
}

// ====================== Implementation ==========================================================

/// <inheritdoc/>
public class OobCalApi: IOobCalApi
{
    private IOobCalHandler _handler;

    public OobCalApi(IOobCalHandler handler)
    {
        _handler = handler;
    }
    
    
    /// <inheritdoc/>
    public List<OobCalEvent> CreateOobCalendar(OobCalRequest request)
    {
        return _handler.CreateOobCalendar(request);
    }
}