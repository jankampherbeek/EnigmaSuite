// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Core.Handlers;

/// <inheritdoc/>
public class ProgAspectsHandler: IProgAspectsHandler
{
    private readonly ICalculatedDistance _calculatedDistance;
    private readonly ICheckedProgAspects _checkedProgAspects;

    public ProgAspectsHandler(ICalculatedDistance calculatedDistance, ICheckedProgAspects checkedProgAspects)
    {
        _calculatedDistance = calculatedDistance;
        _checkedProgAspects = checkedProgAspects;
    }
    
    /// <inheritdoc/>
    public ProgAspectsResponse FindProgAspects(ProgAspectsRequest request)
    {
        int resultCode = 0;
        List<AspectTypes> aspectTypes = request.SupportedAspects;
        double orb = request.Orb;
        List<DefinedAspect> allFoundAspects = new();
        try
        {
            allFoundAspects.AddRange(from radixPoint in request.RadixPoints 
                from progPoint in request.ProgPoints 
                let distance = _calculatedDistance.ShortestDistance(radixPoint.Value, progPoint.Value) 
                let aspectsFound = _checkedProgAspects.CheckAspects(distance, orb, aspectTypes) 
                where aspectsFound.Count > 0 
                from aspectFound in aspectsFound 
                let aspectDetails = aspectFound.Key.GetDetails() 
                select new DefinedAspect(radixPoint.Key, progPoint.Key, aspectDetails, orb, aspectFound.Value));
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