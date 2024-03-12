// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Core.Handlers;

/// <summary>Handler for the calculation of longitude equivalents.</summary>
public interface ILongitudeEquivalentHandler
{
    /// <summary>Calculates longitude equivalents.</summary>
    /// <param name="request">Request with chartpoints, longitudes and declinations.</param>
    /// <returns>List of points with longitude equivalent and indication of oob.</returns>
    public List<Tuple<PositionedPoint, bool>> DefineEquivalents(LongitudeEquivalentRequest request);
}



/// <inheritdoc/>

public class LongitudeEquivalentHandler: ILongitudeEquivalentHandler
{
    private IObliquityHandler _obliquityHandler;
    private IDirectConversionCalc _directConversionCalc;

    public LongitudeEquivalentHandler(IObliquityHandler obliquityHandler, IDirectConversionCalc directConversionCalc)
    {
        _obliquityHandler = obliquityHandler;
        _directConversionCalc = directConversionCalc;
    }
    
    /// <inheritdoc/>
    public List<Tuple<PositionedPoint, bool>> DefineEquivalents(LongitudeEquivalentRequest request)
    {
        List<Tuple<PositionedPoint, bool>> longitudeEquivalents = new();
        ObliquityRequest obliquityRequest = new(request.Jd, true);
        double obliquity = _obliquityHandler.CalcObliquity(obliquityRequest);
        foreach (var ppLongDecl in request.PointsPosLongDecl)
        {
            double decl = ppLongDecl.Item3;
            bool oob = false;
            if (ppLongDecl.Item3 > obliquity)
            {
                double oobPart = decl - obliquity;
                decl-= oobPart;
                oob = true;
            }
            double longitudeEquivalent = _directConversionCalc.DeclinationToLongitude(obliquity, decl);
            // TODO correct for hemisphere
            if (longitudeEquivalent < 0.0) longitudeEquivalent += 180.0;
            double longitudeEquivalent2 = 180.0 - longitudeEquivalent;
            double diff = Math.Abs(longitudeEquivalent - ppLongDecl.Item2);
            double diff2 = Math.Abs(longitudeEquivalent2 - ppLongDecl.Item2);
            if (diff2 < diff) longitudeEquivalent = longitudeEquivalent2;
            longitudeEquivalents.Add(new Tuple<PositionedPoint, bool> 
                (new PositionedPoint(ppLongDecl.Item1, longitudeEquivalent), oob));
        }
        return longitudeEquivalents;
    }
    
}