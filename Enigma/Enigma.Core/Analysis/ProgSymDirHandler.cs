// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Core.Analysis;

/// <summary>Handles calculation of symbolic directions.</summary>
public interface ICalcSymDirHandler
{
    /// <summary>Handles calculation of symbolic directions for a specific event.</summary>
    /// <param name="request">Request with config items, date/time etc.</param>
    /// <returns>Calculated positions.</returns>
    public ProgRealPointsResponse CalculateSymDir(SymDirEventRequest request);
}


public sealed class ProgSymDirHandler(IProgRealPointCalc progRealPointCalc) : ICalcSymDirHandler
{
    public ProgRealPointsResponse CalculateSymDir(SymDirEventRequest request)
    {
        var resultCode = ResultCodes.OK;
        Dictionary<ChartPoints, ProgPositions> positions = new();
        try
        {
            var symbolicArc = DefineSymbolicArc(request);

            foreach (var point in request.ConfigSym.ProgPoints)
            {
                if (request.RadixPoints.ContainsKey(point.Key))      // ignore points in configsym that are not used in radix
                {
                    var radixPos = request.RadixPoints[point.Key];
                    var symPos = RangeUtil.ValueToRange(radixPos + symbolicArc, 0.0, 360.0);
                    ProgPositions progPos = new(symPos, 0.0, 0.0, 0.0);
                    positions.Add(point.Key, progPos);                    
                }
            }
        }
        catch (Exception e)
        {
            resultCode = ResultCodes.GENERAL_ERROR;
            Log.Error("Error in CalcSymDirHandler.CalculateSymDir: {Msg}", e.Message);
        }

        return new ProgRealPointsResponse(positions, resultCode);
    }

    private double DefineSymbolicArc(SymDirEventRequest request)
    {
        var timeSpanInDays = request.JdEvent - request.JdRadix;
        var timeSpanInYears = timeSpanInDays / EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
        var timeKey = request.ConfigSym.TimeKey;
        switch (timeKey)
        {
            case SymbolicKeys.TrueSun:
            {
                var sunRadix = request.RadixPoints[ChartPoints.Sun];
                var secondaryJd = request.JdRadix + timeSpanInYears;
                Location? dummyLocation = new("", 0.0, 0);
                Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints = new();
                progPoints.Add(ChartPoints.Sun, new ProgPointConfigSpecs(true, 'a'));
                var response = progRealPointCalc.CalculateTransits(Ayanamshas.None, 
                    ObserverPositions.GeoCentric, dummyLocation, secondaryJd, progPoints );
                ProgPositions positions;
                if (response.Positions.TryGetValue(ChartPoints.Sun, out positions))
                {
                    var sunSec = positions.Longitude;
                    return sunSec - sunRadix;
                }
                else throw new InvalidDataException();
            }
            case SymbolicKeys.MeanSun:
            {
                var meanSpeedOfSun = 360.0 / EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
                return timeSpanInYears * meanSpeedOfSun;
            }
            case SymbolicKeys.OneDegree:
                return timeSpanInYears;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
    }
    
}