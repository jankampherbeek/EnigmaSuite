// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Core.Analysis;


public interface IProgAspectsHandler
{
    public ProgAspectsResponse FindProgAspects(ProgAspectsRequest request);
}


/// <inheritdoc/>
public class ProgAspectsHandler(ICalculatedDistance calculatedDistance, ICheckedProgAspects checkedProgAspects)
    : IProgAspectsHandler
{
    /// <inheritdoc/>
    public ProgAspectsResponse FindProgAspects(ProgAspectsRequest request)
    {
        var resultCode = 0;
        var aspectTypes = request.SupportedAspects;
        var orb = request.Orb;
        List<DefinedAspect> allFoundAspects = new();
        try
        {
            allFoundAspects.AddRange(from radixPoint in request.RadixPoints 
                from progPoint in request.ProgPoints 
                let distance = calculatedDistance.ShortestDistance(radixPoint.Value, progPoint.Value) 
                let aspectsFound = checkedProgAspects.CheckAspects(distance, orb, aspectTypes) 
                where aspectsFound.Count > 0 
                from aspectFound in aspectsFound 
                let aspectDetails = aspectFound.Key.GetDetails() 
                select new DefinedAspect(progPoint.Key, radixPoint.Key, aspectDetails, orb, aspectFound.Value));
        }
        catch (ArgumentException e)
        {
            Log.Error("ArgumentException in ProgAspectsHandler.FindPRogAspects: {Msg}", e.Message);
            resultCode = ResultCodes.WRONG_ARGUMENTS;
        }
        catch (Exception e)
        {
            Log.Error("General Exception in ProgAspectsHandler.FindProgAspects: {Msg}", e.Message);
            resultCode = ResultCodes.GENERAL_ERROR;
        }
        return new ProgAspectsResponse(allFoundAspects, resultCode);
    }
}