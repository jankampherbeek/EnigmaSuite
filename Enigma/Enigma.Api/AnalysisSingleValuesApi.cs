// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Api;

/// <summary>API for the calculation of sts of single values for analysis.</summary>
public interface IAnalysisSingleValuesApi
{
    /// <summary>Calculate longitude equivalents.</summary>
    /// <param name="request">Request with chartpoints and positions (longitude, declination).</param>
    /// <returns>Points and longitude equivalent, and also an indiciation if the planet was OOB.</returns>
    public List<Tuple<PositionedPoint, bool>> CalculateLongitudeEquivalents(LongitudeEquivalentRequest request);
}


public class AnalysisSingleValuesApi : IAnalysisSingleValuesApi
{
    private ILongitudeEquivalentHandler _longitudeEquivalentHandler;

    public AnalysisSingleValuesApi(ILongitudeEquivalentHandler longitudeEquivalentHandler)
    {
        _longitudeEquivalentHandler = longitudeEquivalentHandler;
    }
    
    public List<Tuple<PositionedPoint, bool>> CalculateLongitudeEquivalents(LongitudeEquivalentRequest request)
    {
        return _longitudeEquivalentHandler.DefineEquivalents(request);
    }
}
