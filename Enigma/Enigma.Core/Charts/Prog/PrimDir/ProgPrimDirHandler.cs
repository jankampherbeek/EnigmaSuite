// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers;
using Enigma.Domain.Charts.Prog.PrimDir;
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
    private IJulDayHandler _julDayHandler;

    public ProgPrimDirHandler(IJulDayCalc julDayCalc, IPrimDirDates primDirDates, IJulDayHandler julDayHandler)
    {
        _julDayCalc = julDayCalc;
        _primDirDates = primDirDates;
        _julDayHandler = julDayHandler;
    }
    
    
    /// <inheritdoc/>
    public PrimDirResponse HandleRequest(PrimDirRequest request)
    {
        List<PrimDirHit> hits = new();
        Calendars cal = request.StartDate.Calendar;
        var speculum = new Speculum(request);
        Tuple<double, double> jdStartend = DefineJdLimits(request);
        double jdStart = jdStartend.Item1;
        double jdEnd = jdStartend.Item2;
        double arc = 0.0;
        
        // TODO add supported aspects
        
        
        foreach (ChartPoints significator in request.Significators)
        {
            double jdForEvent;
            foreach (ChartPoints promissor in request.Promissors)
            {

               switch (request.Method)
               {
                   case PrimDirMethods.Placidus:
                       arc = PlacidusArc(significator, promissor, speculum);
                       break;
                   case PrimDirMethods.PlacidusPole:
                       arc = PlacidusPoleArc(significator, promissor, speculum);
                       break;
                   case PrimDirMethods.Regiomontanus:
                       arc = RegiomontanusArc(significator, promissor, speculum);
                       break;
                   case PrimDirMethods.Topocentric:
                       arc = TopocentricArc(significator, promissor, speculum);
                       break;
                   default:
                       throw new ArgumentException("Unknown method for primary directions: " + request.Method);
               }
                jdForEvent = _primDirDates.JdForEvent(request.Chart.InputtedChartData.FullDateTime.JulianDayForEt, arc, request.TimeKey);
                if (jdForEvent > jdStart && jdForEvent <= jdEnd)
                {
                    hits.Add(ConstructHit(jdForEvent, cal, significator, promissor, AspectTypes.Conjunction ));
                }
            }
        }
        hits.Sort((x, y) => x.Jd.CompareTo(y.Jd));
        bool errors = false;
        string resultTxt = "OK";
        return new PrimDirResponse(hits, errors, resultTxt);
    }

    private PrimDirHit ConstructHit(double jd, Calendars cal, ChartPoints sign, ChartPoints prom, AspectTypes aspect)
    {
        const string separator = "/";
        SimpleDateTime dateTime = _julDayHandler.CalcDateTime(jd, cal);
        string yearText = $"{dateTime.Year:D4}";
        if (dateTime.Year > 9999 || dateTime.Year < -9999)
        {
            yearText = $"{dateTime.Year:D5}";
        }
        string monthText = $"{dateTime.Month:D2}";
        string dayText = $"{dateTime.Day:D2}";
        string calendarText = cal == Calendars.Gregorian ? "G" : "J";
        string dateTxt = yearText + separator + monthText + separator + dayText + " " + calendarText;
        return new PrimDirHit(jd, dateTxt, sign, prom, aspect);
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
    
    private double RegiomontanusArc(ChartPoints significator, ChartPoints promissor, Speculum speculum)
    {
        var signPoint = (SpeculumPointReg)speculum.SpeculumPoints[significator];
        var promPoint = (SpeculumPointReg)speculum.SpeculumPoints[promissor];
        double oadPromPoleSign = promPoint.PointBase.Ra + signPoint.AdPoleReg;
        if (signPoint.PointBase.ChartLeft) oadPromPoleSign = promPoint.PointBase.Ra - signPoint.AdPoleReg;
        double arcDir = oadPromPoleSign - signPoint.OadPoleReg;
        return arcDir;
    }
    
    private double TopocentricArc(ChartPoints significator, ChartPoints promissor, Speculum speculum)
    {
        var fixPoint = (SpeculumPointTopoc)speculum.SpeculumPoints[significator];
        var movPoint = (SpeculumPointTopoc)speculum.SpeculumPoints[promissor];
        double movPoleTcRad = MathExtra.DegToRad(movPoint.PoleTc);
        double fixDeclRad = MathExtra.DegToRad(fixPoint.PointBase.Decl);
        double adPoleTc = MathExtra.RadToDeg(Math.Asin(Math.Tan(movPoleTcRad) * Math.Tan(fixDeclRad)));
        double fixOad = fixPoint.PointBase.Ra + adPoleTc;
        double arcDir = fixPoint.PointBase.Ra + adPoleTc - fixOad;
        return arcDir;
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