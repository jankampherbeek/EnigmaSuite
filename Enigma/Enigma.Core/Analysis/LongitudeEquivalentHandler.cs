// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;

namespace Enigma.Core.Analysis;

/// <summary>Handler for the calculation of longitude equivalents.</summary>
public interface ILongitudeEquivalentHandler
{
    /// <summary>Calculates longitude equivalents.</summary>
    /// <param name="request">Request with chartpoints, longitudes and declinations.</param>
    /// <returns>List of points with longitude equivalent and indication of oob.</returns>
    public List<Tuple<PositionedPoint, bool>> DefineEquivalents(LongitudeEquivalentRequest request);
}


/// <inheritdoc/>
public sealed class LongitudeEquivalentHandler(
    IObliquityHandler obliquityHandler,
    IDirectConversionCalc directConversionCalc)
    : ILongitudeEquivalentHandler
{
    /// <inheritdoc/>
    public List<Tuple<PositionedPoint, bool>> DefineEquivalents(LongitudeEquivalentRequest request)
    {
        List<Tuple<PositionedPoint, bool>> longitudeEquivalents = [];
        ObliquityRequest obliquityRequest = new(request.Jd, true);
        var obliquity = obliquityHandler.CalcObliquity(obliquityRequest);
        foreach (var ppLongDecl in request.PointsPosLongDecl)
        {
            var radixDeclination = ppLongDecl.Item3;
            var declination = radixDeclination;
            var longitude = ppLongDecl.Item2;
            var oob = false;
            if (Math.Abs(radixDeclination) > obliquity)     // OOB
            {
                var oobPart = Math.Abs(radixDeclination) - obliquity;
                declination = radixDeclination > 0 ? obliquity - oobPart : oobPart - obliquity;
                oob = true;
            }
            var candidate1 = directConversionCalc.DeclinationToLongitude(obliquity, declination);
            if (candidate1 < 0.0) candidate1+= 360.0;
            var candidate2 = longitude < 180.0 ? 90.0 + (90.0 - candidate1) : 270.0 + (270.0 - candidate1);

            var diff1 = Math.Abs(candidate1 - longitude);
            var diff2 = Math.Abs(candidate2 - longitude);
            var longitudeEquivalent = diff1 < diff2 ? candidate1 : candidate2;

            longitudeEquivalents.Add(new Tuple<PositionedPoint, bool> 
                (new PositionedPoint(ppLongDecl.Item1, longitudeEquivalent), oob));
        }
        return longitudeEquivalents;
    }
    
}