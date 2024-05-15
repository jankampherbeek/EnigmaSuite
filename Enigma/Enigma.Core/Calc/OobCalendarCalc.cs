// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Core.Calc;

/// <summary>Calculation of an Oob calendar, using secundary Julian Day Numbers.</summary>
public interface IOobCalendarCalc
{
    /// <summary>Calculate the Oob calendar.</summary>
    /// <param name="request">The request.</param>
    /// <returns>Calculated events, sorted by Julian Day.</returns>
    public IEnumerable<OobSecJdEvent> CreateOobCalendar(OobCalRequest request);
}


// =========================== Implementation ========================================================

/// <inheritdoc/>
public sealed class OobCalendarCalc : IOobCalendarCalc
{
    private readonly ISeFlags _seFlags;
    private readonly ICelPointsHandler _celPointsHandler;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly ICalculationPreferencesCreator _calcPrefsCreator;
    private List<OobSecJdEvent> _oobJdEvents; 
    private double _obliquity = 0.0;
    private int _flags = 0;
    private Location? _location;
    private AstroConfig? _config;
    private const int NUMBER_OF_DAYS = 120;       // secondary days, resp. 120 years in life
    private const double DRILL_DOWN_STEPS = 30.0; // numer of steps to drill down, used 2 times sot here are 900 steps.
    
    
    public OobCalendarCalc(ISeFlags seFlags, 
        ICelPointsHandler celPointsHandler, 
        IObliquityHandler obliquityHandler,
        ICalculationPreferencesCreator calcPrefsCreator)
    {
        _seFlags = seFlags;
        _celPointsHandler = celPointsHandler;
        _obliquityHandler = obliquityHandler;
        _calcPrefsCreator = calcPrefsCreator;
        
    }
    
    /// <inheritdoc/>
    public IEnumerable<OobSecJdEvent> CreateOobCalendar(OobCalRequest request)
    {
        _oobJdEvents = new List<OobSecJdEvent>();
        _config = request.Config ?? throw new EnigmaException("Configuration is null in OobCalHandler.");
        _flags = _seFlags.DefineFlags(CoordinateSystems.Equatorial,
            _config.ObserverPosition,
            _config.ZodiacType);
        Dictionary<ChartPoints, ChartPointConfigSpecs> relevantPoints = DefineRelevantChartPoints(_config.ChartPoints);
        CalculationPreferences prefs = _calcPrefsCreator.CreatePrefs(_config, CoordinateSystems.Equatorial);
        _location = request.Location ?? throw new EnigmaException("Location is null in OobCalHandler.");


 
        
        /*Tuple<ChartPoints, DeclCalcResult>? newPos;
        Tuple<ChartPoints, DeclCalcResult>? previousPos = null;*/

        double newPos = 0.0;
        double prevPos = 0.0;
        
        foreach (var pointSpec in relevantPoints)
        {
            double actualJd = request.JdStart;
            double prevJd = actualJd - 1;
            DefineInitialPositions(pointSpec.Key, actualJd, _location, prefs);
            
            
            for (int i = 0; i < NUMBER_OF_DAYS; i++) //  first range of steps
            {
                ObliquityRequest oblReq = new(actualJd, true);
                _obliquity = _obliquityHandler.CalcObliquity(oblReq); // recalculate obliquity once per day
                Tuple<double, double> declPosSpeed = CalcDeclForSinglePoint(pointSpec.Key, actualJd, prefs);
                bool isOob = Math.Abs(declPosSpeed.Item1) > _obliquity;

                DeclCalcResult calcResult = new(actualJd, _obliquity, declPosSpeed.Item1, declPosSpeed.Item2, isOob);

                if (i == 0)         // start date
                {
                    prevPos = calcResult.DeclPos;
                } 
                else
                {
                    newPos = calcResult.DeclPos;
                    bool newOob = Math.Abs(newPos) > _obliquity;
                    bool prevOob = Math.Abs(prevPos) > _obliquity;
                    if (newOob != prevOob || (Math.Abs(newPos) - _obliquity) < 0.5) FirstDrillDown(pointSpec.Key, prevJd, prefs);
                    prevPos = newPos;
                }

                prevJd = actualJd;
                actualJd++;
            }            
        }
        return _oobJdEvents;
    }

    private void FirstDrillDown(ChartPoints point, double startJd, CalculationPreferences prefs)
    {
        double prevDd1 = 0.0;
        //  Tuple<double, double>? newDd1 = new(0.0, 0.0);
        double newDd1 = 0.0; 
        double jdDrillDown1 = startJd;
        double prevJd = jdDrillDown1 - 1 / DRILL_DOWN_STEPS;
        for (int i = 0; i < DRILL_DOWN_STEPS; i++)
        {
            double ddPos1 = CalcDeclForSinglePoint(point, jdDrillDown1, prefs).Item1;
            if (i == 0) prevDd1 = ddPos1;
            else
            {
                newDd1 = ddPos1;
                bool newOobDd1 = Math.Abs(newDd1) > _obliquity;
                bool prevOobDd1 = Math.Abs(prevDd1) > _obliquity;
                if (newOobDd1 != prevOobDd1) FinalDrillDown(point, prevJd, prefs);
                prevDd1 = newDd1;
            }
            prevJd = jdDrillDown1;
            jdDrillDown1 += 1 / DRILL_DOWN_STEPS;
        }
    }

    private void FinalDrillDown(ChartPoints point, double startJd, CalculationPreferences prefs)
    {
        double jd = startJd;
        double prevDecl = 0.0;
        double newDecl;
        for (int i = 0; i < DRILL_DOWN_STEPS; i++)
        {
            newDecl = CalcDeclForSinglePoint(point, jd, prefs).Item1;
            
            if (i == 0) prevDecl = newDecl;
            else
            {
                bool newOob = Math.Abs(newDecl) > _obliquity;
                bool prevOob = Math.Abs(prevDecl) > _obliquity;
                if (newOob != prevOob)
                {
                    OobEventTypes eventType;
                    if (newDecl > 0.0) eventType = newOob ? OobEventTypes.StartOobNorth : OobEventTypes.EndOobNorth;                        
                    else eventType = newOob ? OobEventTypes.StartOobSouth : OobEventTypes.EndOobSouth;
                    OobSecJdEvent oobSecJdEvent = new(point, eventType, jd);
                    _oobJdEvents.Add(oobSecJdEvent);                
                } 
            }
            prevDecl = newDecl;
            jd += 1 / (DRILL_DOWN_STEPS * DRILL_DOWN_STEPS);
        }
    }

    private void DefineInitialPositions(ChartPoints point, double jd, Location location, CalculationPreferences calcPref )
    {
        FullPointPos pointPos = _celPointsHandler.CalcSinglePointWithSe(point, jd, location, calcPref);
        ObliquityRequest oblReq = new(jd, true);
        _obliquity = _obliquityHandler.CalcObliquity(oblReq);        
        double declination = pointPos.Equatorial.DeviationPosSpeed.Position;
        bool oob = (Math.Abs(declination)) - _obliquity > 0.0;
        bool north = declination > 0.0;
        OobEventTypes eventType;
        if (oob) eventType = north ? OobEventTypes.InitialOobNorth : OobEventTypes.InitialOobSouth;
        else eventType = north ? OobEventTypes.InitialInBoundsNorth : OobEventTypes.InitialInBoundsSouth;
        _oobJdEvents.Add(new OobSecJdEvent(point, eventType, jd));
    }

    /*private Dictionary<ChartPoints, FullPointPos> CalcPositions(double jd,
        CalculationPreferences prefs)
    {
        CelPointsRequest request = new(jd, _location!, prefs);
        return _allPosHandler.CalcFullChart(request);
    }*/

    private Tuple<double, double>? CalcDeclForSinglePoint(ChartPoints point, double jd, CalculationPreferences prefs)
    {
        CalculationPreferences pointPrefs =
            _calcPrefsCreator.CreatePrefsForSinglePoint(point, _config, CoordinateSystems.Equatorial);
        FullPointPos pointPos = _celPointsHandler.CalcSinglePointWithSe(point, jd, _location, prefs);
        var declPos = pointPos.Equatorial.DeviationPosSpeed.Position;
        var declSpeed = pointPos.Equatorial.DeviationPosSpeed.Speed;
        return  new Tuple<double, double>(declPos, declSpeed);
    }
    
    private Dictionary<ChartPoints, ChartPointConfigSpecs> DefineRelevantChartPoints(Dictionary<ChartPoints, ChartPointConfigSpecs> configPoints)
    {
        List<ChartPoints> pointsToIgnore = new()  // exclude all points with zero latitude and all hypothetical points.
        {
            ChartPoints.Sun,
            ChartPoints.Earth,
            ChartPoints.MeanNode,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeCorrected,
            ChartPoints.ApogeeDuval,
            ChartPoints.ApogeeInterpolated,
            ChartPoints.ApogeeMean,
            ChartPoints.PersephoneCarteret,
            ChartPoints.VulcanusCarteret,
            ChartPoints.PersephoneRam,
            ChartPoints.HermesRam,
            ChartPoints.DemeterRam,
            ChartPoints.Isis,
            ChartPoints.CupidoUra,
            ChartPoints.AdmetosUra,
            ChartPoints.ApollonUra,
            ChartPoints.HadesUra,
            ChartPoints.KronosUra,
            ChartPoints.PoseidonUra,
            ChartPoints.VulcanusUra,
            ChartPoints.ZeusUra
        };
        
        return (from pointSpec in configPoints 
            let point = pointSpec.Key 
            let pointCat = point.GetDetails().PointCat 
            where (pointCat == PointCats.Common) && pointSpec.Value.IsUsed  && !pointsToIgnore.Contains(point) 
            select pointSpec).
            ToDictionary(pointSpec => pointSpec.Key, 
                pointSpec => pointSpec.Value);
    }
}

public record DeclCalcResult(double Jd, double Obliquity, double DeclPos, double DeclSpeed, bool IsOob );
