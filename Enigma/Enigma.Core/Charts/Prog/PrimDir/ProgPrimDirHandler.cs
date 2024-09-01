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
        double oppArc = 0.0;
        
        foreach (ChartPoints movPoint in request.Significators)
        {
            double jdForEvent;
            foreach (ChartPoints fixPoint in request.Promissors)
            {

               switch (request.Method)
               {
                   case PrimDirMethods.Placidus:
                       arc = PlacidusArc(movPoint, fixPoint, speculum, AspectTypes.Conjunction);
                       oppArc = PlacidusArc(movPoint, fixPoint, speculum, AspectTypes.Opposition);
                       break;
                   case PrimDirMethods.Regiomontanus:
                       arc = RegiomontanusArc(movPoint, fixPoint, speculum, AspectTypes.Conjunction);
                       oppArc = RegiomontanusArc(movPoint, fixPoint, speculum, AspectTypes.Opposition);
                       break;
                   case PrimDirMethods.Topocentric:
                       arc = TopocentricArc(movPoint, fixPoint, speculum, AspectTypes.Conjunction);
                       oppArc = TopocentricArc(movPoint, fixPoint, speculum, AspectTypes.Opposition);
                       break;
                   default:
                       throw new ArgumentException("Unknown method for primary directions: " + request.Method);
               }
                jdForEvent = _primDirDates.JdForEvent(request.Chart.InputtedChartData.FullDateTime.JulianDayForEt, arc, request.TimeKey);
                if (jdForEvent > jdStart && jdForEvent <= jdEnd)
                {
                    hits.Add(ConstructHit(jdForEvent, cal, movPoint, fixPoint, AspectTypes.Conjunction ));
                }
                jdForEvent = _primDirDates.JdForEvent(request.Chart.InputtedChartData.FullDateTime.JulianDayForEt, oppArc, request.TimeKey);
                if (jdForEvent > jdStart && jdForEvent <= jdEnd)
                {
                    hits.Add(ConstructHit(jdForEvent, cal, movPoint, fixPoint, AspectTypes.Opposition ));
                }

            }
        }
        
        hits.Sort((x, y) => x.Jd.CompareTo(y.Jd));
        bool errors = false;
        string resultTxt = "OK";
        return new PrimDirResponse(hits, errors, resultTxt);
    }

    private PrimDirHit ConstructHit(double jd, Calendars cal, ChartPoints movPoint, ChartPoints fixPoint, AspectTypes aspect)
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
        return new PrimDirHit(jd, dateTxt, fixPoint, movPoint, aspect);
    }

   
    private double PlacidusArc(ChartPoints movPoint, ChartPoints fixPoint, Speculum speculum, AspectTypes aspect)
    {
        var specMovPoint = (SpeculumPointPlac)speculum.SpeculumPoints[movPoint];
        var specFixPoint = (SpeculumPointPlac)speculum.SpeculumPoints[fixPoint];
        if (aspect == AspectTypes.Opposition)
        {
            specMovPoint = (SpeculumPointPlac)speculum.SpeculumOppPoints[movPoint];
            specFixPoint = (SpeculumPointPlac)speculum.SpeculumPoints[fixPoint];
        }
        int quadrCorr = 1;
        if (specFixPoint.PointBase.ChartLeft && specFixPoint.PointBase.ChartTop) quadrCorr = -1;    // Quadrant IV
        if (!specFixPoint.PointBase.ChartLeft && !specFixPoint.PointBase.ChartTop) quadrCorr = -1;  // Quadrant II
        int horCorr = 1;
        double r = speculum.Base.RaMc;
        if (!specFixPoint.IsTop)
        {
            horCorr = -1;
            r = speculum.Base.RaIc;
        }
        double raP = specMovPoint.PointBase.Ra;                     // RA promissor
        double adP = specMovPoint.Ad;                               // AD promisssor
        double mdS = specFixPoint.MerDist;                          // mer distance significator
        double saS = specFixPoint.SemiArc;                          // semiarc significator
        double arc = raP - r + quadrCorr * (90 + horCorr * adP) * mdS / saS;
        return RangeUtil.ValueToRange(arc, 0.0, 360.0);
    }
    
    
    private double RegiomontanusArc(ChartPoints movPoint, ChartPoints fixPoint, Speculum speculum, AspectTypes aspect)
    {
        var promPoint = (SpeculumPointReg)speculum.SpeculumPoints[movPoint];
        var signPoint = (SpeculumPointReg)speculum.SpeculumPoints[fixPoint];
        if (aspect == AspectTypes.Opposition)
        {
            signPoint = (SpeculumPointReg)speculum.SpeculumPoints[fixPoint];
            promPoint = (SpeculumPointReg)speculum.SpeculumOppPoints[movPoint];
        }
        double declPromRad = MathExtra.DegToRad(promPoint.PointBase.Decl);
        double poleSignRad = MathExtra.DegToRad(signPoint.PoleReg);
        double qp = MathExtra.RadToDeg(Math.Asin(Math.Tan(declPromRad) * Math.Tan(poleSignRad)));
        double wp = promPoint.PointBase.Ra - qp;
        if (!signPoint.PointBase.ChartLeft) wp = promPoint.PointBase.Ra + qp;        
        double arcDir = wp - signPoint.FactorW;
        return arcDir;
    }
    
    private double TopocentricArc(ChartPoints movPoint, ChartPoints fixPoint, Speculum speculum, AspectTypes aspect)
    {
        var specFixPoint = (SpeculumPointTopoc)speculum.SpeculumPoints[movPoint];
        var specMovPoint = (SpeculumPointTopoc)speculum.SpeculumPoints[fixPoint];
        if (aspect == AspectTypes.Opposition)
        {
            specFixPoint = (SpeculumPointTopoc)speculum.SpeculumPoints[fixPoint];
            specMovPoint = (SpeculumPointTopoc)speculum.SpeculumOppPoints[movPoint];
        }        
        double declPromRad = MathExtra.DegToRad(specMovPoint.PointBase.Decl);
        double polesignRad = MathExtra.DegToRad(specFixPoint.PoleTc);
        double qProm = MathExtra.RadToDeg(Math.Asin(Math.Tan(declPromRad) * Math.Tan(polesignRad)));
        double wProm = specMovPoint.PointBase.Ra - qProm;
        if (!specFixPoint.PointBase.ChartLeft) wProm = specMovPoint.PointBase.Ra - qProm;
        double arcDir = wProm - specFixPoint.FactorW; 
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