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
                       arc = RegiomontanusArc(jdStart, jdEnd, speculum, request);
                       break;
                   case PrimDirMethods.Topocentric:
                       arc = TopocentricArc(jdStart, jdEnd, speculum, request);
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
        double signSa = signPoint.PointSaBase.HorDist >= 0.0 ? signPoint.PointSaBase.SaD : signPoint.PointSaBase.SaN;
        double signMd = signPoint.PointSaBase.HorDist >= 0.0 ? signPoint.PointSaBase.MdU : signPoint.PointSaBase.MdL;
        double promSa = promPoint.PointSaBase.HorDist >= 0.0 ? promPoint.PointSaBase.SaD : promPoint.PointSaBase.SaN;
        double promMd = promPoint.PointSaBase.HorDist >= 0.0 ? promPoint.PointSaBase.MdU : promPoint.PointSaBase.MdL;
        double signPropSaDist = signMd / signSa;
        double promProjMerdist = signPropSaDist * promSa;
        double dirArc = promProjMerdist - promMd;
        return dirArc;
    }
    
    private double PlacidusPoleArc(ChartPoints significator, ChartPoints promissor, double jdStart, double jdEnd, Speculum speculum, PrimDirRequest request)
    {
        SpeculumPointPlacPole signPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[significator];
        SpeculumPointPlacPole promPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[promissor];
        double signSa = signPoint.PointSaBase.HorDist >= 0.0 ? signPoint.PointSaBase.SaD : signPoint.PointSaBase.SaN;
        double signMd = signPoint.PointSaBase.HorDist >= 0.0 ? signPoint.PointSaBase.MdU : signPoint.PointSaBase.MdL;
        double promSa = promPoint.PointSaBase.HorDist >= 0.0 ? promPoint.PointSaBase.SaD : promPoint.PointSaBase.SaN;
        double promMd = promPoint.PointSaBase.HorDist >= 0.0 ? promPoint.PointSaBase.MdU : promPoint.PointSaBase.MdL;
        double signAd = signPoint.PointSaBase.Ad;
        double promAd = promPoint.PointSaBase.Ad;
        double signAdPlacPole = (signMd / signSa) * signAd;
        double promAdPlacPole = (promMd / promSa) * promAd;
        double signAdPlacPoleRad = MathExtra.DegToRad(signAdPlacPole);
        double signDeclRad = MathExtra.DegToRad(signPoint.PointBase.Decl);
        double signElevPole = MathExtra.RadToDeg(Math.Atan2(Math.Sin(signAdPlacPoleRad), Math.Tan(signDeclRad)));
        double promAdPlacPoleRad = MathExtra.DegToRad(signAdPlacPole);
        double promDeclRad = MathExtra.DegToRad(signPoint.PointBase.Decl);
        double promElevPole = MathExtra.RadToDeg(Math.Atan2(Math.Sin(promAdPlacPoleRad), Math.Tan(promDeclRad)));

        double signOadUnderPole = signPoint.PointBase.Ra + signAdPlacPole;
        if (signPoint.PointBase.ChartLeft) signOadUnderPole = signPoint.PointBase.Ra - signAdPlacPole;
        double promOadUnderPole = promPoint.PointBase.Ra + promAdPlacPole;
        if (promPoint.PointBase.ChartLeft) promOadUnderPole = promPoint.PointBase.Ra - promAdPlacPole;

        double signElevPoleRad = MathExtra.DegToRad(signElevPole);
        double promAdUnderPoleOfSign = MathExtra.RadToDeg(Math.Asin(Math.Tan(promDeclRad) * Math.Tan(signElevPoleRad)));
        
        double dirArc = promOadUnderPole - signOadUnderPole;
        
        return 0.0;
    }
    
    private double RegiomontanusArc(double jdStart, double jdEnd, Speculum speculum, PrimDirRequest request)
    {
        return 0.0;
    }
    
    private double TopocentricArc(double jdStart, double jdEnd, Speculum speculum, PrimDirRequest request)
    {
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