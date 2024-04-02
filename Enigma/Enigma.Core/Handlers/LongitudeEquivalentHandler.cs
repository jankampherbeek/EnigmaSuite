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

//////////// Implementation ////////////

/// <inheritdoc/>

public sealed class LongitudeEquivalentHandler: ILongitudeEquivalentHandler
{
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IDirectConversionCalc _directConversionCalc;

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
            double radixDeclination = ppLongDecl.Item3;
            double declination = radixDeclination;
            double longitude = ppLongDecl.Item2;
            bool oob = false;
            if (Math.Abs(radixDeclination) > obliquity)     // OOB
            {
                double oobPart = Math.Abs(radixDeclination) - obliquity;
                declination = radixDeclination > 0 ? obliquity - oobPart : oobPart - obliquity;
                oob = true;
            }
            double candidate1 = _directConversionCalc.DeclinationToLongitude(obliquity, declination);
            if (candidate1 < 0.0) candidate1+= 360.0;
            double candidate2 = longitude < 180.0 ? 90.0 + (90.0 - candidate1) : 270.0 + (270.0 - candidate1);

            double diff1 = Math.Abs(candidate1 - longitude);
            double diff2 = Math.Abs(candidate2 - longitude);
            double longitudeEquivalent = diff1 < diff2 ? candidate1 : candidate2;

            longitudeEquivalents.Add(new Tuple<PositionedPoint, bool> 
                (new PositionedPoint(ppLongDecl.Item1, longitudeEquivalent), oob));
        }
        return longitudeEquivalents;
    }
    
}