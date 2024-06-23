// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Charts.Prog.PrimDir;

/// <summary>Handler for the calculation of primary directions.</summary>
public interface IProgPrimDirHandler
{
    /// <summary>Perform the calculation for an incoming request.</summary>
    /// <param name="request">The request.</param>
    /// <returns>Response with the results.</returns>
    public PrimDirResponse HandleRequest(PrimDirRequest request);
}


// ========================== Implementation =============================================================

/// <inheritdoc/>
public class ProgPrimDirHandler(): IProgPrimDirHandler
{

    
    
    /// <inheritdoc/>
    public PrimDirResponse HandleRequest(PrimDirRequest request)
    {
        var speculum = new Speculum(request);
        return null;
        /*switch (request.Method)
        {
            case PrimDirMethods.Placidus:
                return HandlePlacidus(request);
            case PrimDirMethods.PlacidusPole:
                return HandlePlacidusUnderThePole(request);
            case PrimDirMethods.Regiomontanus:
                return HandleRegiomontanus(request);
            case PrimDirMethods.Topocentric:
                return HandleTopoCentric(request);
            default:
                throw new ArgumentException("Unknown method for primary directions: " + request.Method);
        }*/
    }

 
    
    
}