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
                       arc = PlacidusArc(significator, promissor, speculum);
                       jdForEvent = _primDirDates.JdForEvent(jdStart, arc, request.TimeKey);
                        // todo add to response
                       break;
                   case PrimDirMethods.PlacidusPole:
                       arc = PlacidusPoleArc(significator, promissor, speculum);
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

    private double PlacidusArc(ChartPoints significator, ChartPoints promissor, Speculum speculum)
    {
        var signPoint = (SpeculumPointPlac)speculum.SpeculumPoints[significator];
        var promPoint = (SpeculumPointPlac)speculum.SpeculumPoints[promissor];
        double signPropSaDist = signPoint.MerDist / signPoint.SemiArc;
        double promProjMerdist = signPropSaDist * promPoint.SemiArc;
        double dirArc = promProjMerdist - promPoint.MerDist;
        return dirArc;
    }
    
    private double PlacidusPoleArc(ChartPoints significator, ChartPoints promissor, Speculum speculum)
    {
        var signPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[significator];
        var promPoint = (SpeculumPointPlacPole)speculum.SpeculumPoints[promissor];
        double promOadUnderPoleOfSign =
            PrimDirCalcAssist.AdPromUnderElevPoleSign(signPoint.ElevPole, promPoint.PointBase.Decl);
        double dirArc = promOadUnderPoleOfSign - promPoint.AdPlacPole;
        return dirArc;
    }
    
    private double RegiomontanusArc(ChartPoints significator, ChartPoints promissor, double jdStart, double jdEnd, Speculum speculum, PrimDirRequest request)
    {
        SpeculumPointReg signPoint = (SpeculumPointReg)speculum.SpeculumPoints[significator];
        SpeculumPointReg promPoint = (SpeculumPointReg)speculum.SpeculumPoints[promissor];
        double oadPromPoleSign = promPoint.PointBase.Ra + signPoint.AdPoleReg;
        if (signPoint.PointBase.ChartLeft) oadPromPoleSign = promPoint.PointBase.Ra - signPoint.AdPoleReg;
        double arcDir = oadPromPoleSign - signPoint.OadPoleReg;
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