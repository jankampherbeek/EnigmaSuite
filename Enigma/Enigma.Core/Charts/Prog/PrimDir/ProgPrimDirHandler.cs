// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

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
public class ProgPrimDirHandler: IProgPrimDirHandler
{
    private IJulDayCalc _julDayCalc;
    private IPrimDirDates _primDirDates;

    public ProgPrimDirHandler(IJulDayCalc julDayCalc, IPrimDirDates primDirDates)
    {
        _julDayCalc = julDayCalc;
        _primDirDates = primDirDates;
    }
    
    
    /// <inheritdoc/>
    public PrimDirResponse HandleRequest(PrimDirRequest request)
    {
        var speculum = new Speculum(request);
        Tuple<double, double> jdStartend = DefineJdLimits(request);
        double jdStart = jdStartend.Item1;
        double jdEnd = jdStartend.Item2;
        double arc = 0.0;
        
        // TODO add supported aspects
        
        
        foreach (ChartPoints significator in request.Significators)
        {
            foreach (ChartPoints promissor in request.Promissors)
            {
                double jdForEvent;
                switch (request.Method)
               {
                   case PrimDirMethods.Placidus:
                       arc = PlacidusArc(significator, promissor, jdStart, jdEnd, speculum, request);
                       jdForEvent = _primDirDates.JdForEvent(jdStart, arc, request.TimeKey);
                        // todo add to response
                       break;
                   case PrimDirMethods.PlacidusPole:
                       arc = PlacidusPoleArc(significator, promissor, jdStart, jdEnd, speculum, request);
                       jdForEvent = _primDirDates.JdForEvent(jdStart, arc, request.TimeKey);
                       // todo add to response
                       break;
                   case PrimDirMethods.Regiomontanus:
                       arc = RegiomontanusArc(significator, promissor, jdStart, jdEnd, speculum, request);
                       jdForEvent = _primDirDates.JdForEvent(jdStart, arc, request.TimeKey);                       
                       break;
                   case PrimDirMethods.Topocentric:
                    //   arc = TopocentricArc(jdStart, jdEnd, speculum, request);
                       break;
                   default:
                       throw new ArgumentException("Unknown method for primary directions: " + request.Method);
               }
            }
        }
        

        return null;

    }

    private double PlacidusArc(ChartPoints significator, ChartPoints promissor, double jdStart, double jdEnd, Speculum speculum, PrimDirRequest request)
    {
        SpeculumPointPlac signPoint = (SpeculumPointPlac)speculum.SpeculumPoints[significator];
        SpeculumPointPlac promPoint = (SpeculumPointPlac)speculum.SpeculumPoints[promissor];
        double signSa = signPoint.PointBase.ChartTop ? signPoint.PointSaBase.SaD : signPoint.PointSaBase.SaN;
        double signMd = signPoint.PointSaBase.MerDist;
        double promSa = promPoint.PointBase.ChartTop ? promPoint.PointSaBase.SaD : promPoint.PointSaBase.SaN;
        double promMd = promPoint.PointSaBase.MerDist;
        double signPropSaDist = signMd / signSa;
        double promProjMerdist = signPropSaDist * promSa;
        double dirArc = promProjMerdist - promMd;
        return dirArc;
    }
    
    private double PlacidusPoleArc(ChartPoints significator, ChartPoints promissor, double jdStart, double jdEnd, Speculum speculum, PrimDirRequest request)
    {
        SpeculumPointPlacPole signPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[significator];
        SpeculumPointPlacPole promPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[promissor];
        double signSa = signPoint.PointBase.ChartTop ? signPoint.PointSaBase.SaD : signPoint.PointSaBase.SaN;
        double signMd = signPoint.PointSaBase.MerDist;
        double promSa = promPoint.PointBase.ChartTop ? promPoint.PointSaBase.SaD : promPoint.PointSaBase.SaN;
        double promMd = promPoint.PointSaBase.MerDist;
        double signAd = signPoint.PointSaBase.Ad;
        double promAd = promPoint.PointSaBase.Ad;
        double signAdPlacPole = (signMd / signSa) * signAd;
        double promAdPlacPole = (promMd / promSa) * promAd;
        double signElevPole = PrimDirCalcAssist.ElevationOfThePolePlac(signAdPlacPole, signPoint.PointBase.Decl);
  //      double promElevPole = PrimDirCalcAssist.ElevationOfThePolePlac(promAdPlacPole, promPoint.PointBase.Decl);
        double signOadUnderPole = PrimDirCalcAssist.ObliqueAscDesc(signPoint.PointBase.Ra, signAdPlacPole,
            signPoint.PointBase.ChartLeft, speculum.Base.GeoLat > 0.0);
        double promOadUnderPole = PrimDirCalcAssist.ObliqueAscDesc(promPoint.PointBase.Ra, promAdPlacPole,
            promPoint.PointBase.ChartLeft, speculum.Base.GeoLat > 0.0);
  //      double promAdUnderPoleOfSign =
  //          PrimDirCalcAssist.AdPromUnderElevPoleSign(signElevPole, promPoint.PointBase.Decl);
        double dirArc = promOadUnderPole - signOadUnderPole;
        return dirArc;
    }
    
    private double RegiomontanusArc(ChartPoints significator, ChartPoints promissor, double jdStart, double jdEnd, Speculum speculum, PrimDirRequest request)
    {
        SpeculumPointPlacPole signPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[significator];
        SpeculumPointPlacPole promPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[promissor];
        
        double signDeclRad = MathExtra.DegToRad(signPoint.PointBase.Decl);
        double signMdUpperRad = MathExtra.DegToRad(signPoint.PointSaBase.MerDist);
        double angleX = MathExtra.RadToDeg(Math.Atan2(Math.Tan(signDeclRad), double.Cos(signMdUpperRad)));
        double angleY = speculum.Base.GeoLat - angleX;
        double angleXRad = MathExtra.DegToRad(angleX);
        double angleYRad = MathExtra.DegToRad(angleY);
        double angleZRad = Math.Atan2(double.Cos(angleYRad), Math.Tan(signMdUpperRad) * Math.Cos(angleXRad));
        double geoLatRad = MathExtra.DegToRad(speculum.Base.GeoLat);
        double poleRegRad = Math.Asin(Math.Sin(geoLatRad) * Math.Cos(angleZRad));
        double adPoleReg = Math.Asin(Math.Tan(signDeclRad) * Math.Tan(poleRegRad));
        double oadPoleReg = signPoint.PointBase.Ra - adPoleReg;
        if (promPoint.PointBase.ChartLeft) oadPoleReg = signPoint.PointBase.Ra + adPoleReg;
        double promDeclRad = MathExtra.DegToRad(promPoint.PointBase.Decl);
        double adPromUnderPoleSign = Math.Asin(Math.Tan(promDeclRad) * Math.Tan(poleRegRad));

        double oadSignPoleProm = promPoint.PointBase.Ra + adPromUnderPoleSign;
        if (signPoint.PointBase.ChartLeft) oadSignPoleProm = promPoint.PointBase.Ra - adPromUnderPoleSign;

        double arcDir = oadSignPoleProm - oadPoleReg;
        
        return arcDir;
    }
    
    private double TopocentricArc(ChartPoints significator, ChartPoints promissor, double jdStart, double jdEvent, Speculum speculum, PrimDirRequest request)
    {
        SpeculumPointPlacPole signPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[significator];
        SpeculumPointPlacPole promPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[promissor];
        double geoLatRad = MathExtra.DegToRad(speculum.Base.GeoLat);
        double signDeclRad = MathExtra.DegToRad(signPoint.PointBase.Decl);
        double adPoleTc = MathExtra.RadToDeg(Math.Asin(Math.Tan(geoLatRad) * Math.Tan(signDeclRad)));
        
        
        
        double signOad = signPoint.PointBase.Ra + adPoleTc;
        if (signPoint.PointBase.ChartLeft) signOad = signPoint.PointBase.Ra - adPoleTc;
        double timeInYears = jdEvent - jdStart;
        double arcDir = 0.0;
        switch (request.TimeKey) // todo timekeys van Dam, Ptolemy and Brahe
        {
            case PrimDirTimeKeys.Ptolemy:
                arcDir = timeInYears;
                break;
            case PrimDirTimeKeys.Naibod:
                arcDir = timeInYears * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
                break;
           
        }
        
        
        
        
        
        return 0.0;
    }
    
    private Tuple<double, double> DefineJdLimits(PrimDirRequest request)
    {
        SimpleDateTime startDateTime = new(request.StartDate.Year, request.StartDate.Month,
            request.StartDate.Day, 0.0, request.StartDate.Calendar);
        SimpleDateTime endDateTime = new(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day, 0.0,
            request.EndDate.Calendar);
        return new Tuple<double, double>(_julDayCalc.CalcJulDayUt(startDateTime), _julDayCalc.CalcJulDayUt(endDateTime));
    }

}