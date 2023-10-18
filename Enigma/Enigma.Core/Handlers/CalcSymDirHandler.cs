// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Core.Handlers;

public sealed class CalcSymDirHandler: ICalcSymDirHandler
{
    private readonly IProgRealPointCalc _progRealPointCalc;

    public CalcSymDirHandler(IProgRealPointCalc progRealPointCalc)
    {
        _progRealPointCalc = progRealPointCalc;
    }
        
    
    public ProgRealPointsResponse CalculateSymDir(SymDirEventRequest request)
    {
        int resultCode = ResultCodes.OK;
        Dictionary<ChartPoints, ProgPositions> positions = new();
        try
        {
            double symbolicArc = DefineSymbolicArc(request);

            foreach (var point in request.ConfigSym.ProgPoints)
            {
                if (request.RadixPoints.ContainsKey(point.Key))      // ignore points in configsym that are not used in radix
                {
                    double radixPos = request.RadixPoints[point.Key];
                    double symPos = RangeUtil.ValueToRange(radixPos + symbolicArc, 0.0, 360.0);
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
        double timeSpanInDays = request.JdEvent - request.JdRadix;
        double timeSpanInYears = timeSpanInDays / EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
        SymbolicKeys timeKey = request.ConfigSym.TimeKey;
        switch (timeKey)
        {
            case SymbolicKeys.TrueSun:
            {
                double sunRadix = request.RadixPoints[ChartPoints.Sun];
                double secundaryJd = request.JdRadix + timeSpanInYears;
                Location dummyLocation = new("", 0.0, 0);
                Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints = new();
                progPoints.Add(ChartPoints.Sun, new ProgPointConfigSpecs(true, 'a'));
                ProgRealPointsResponse response = _progRealPointCalc.CalculateTransits(Ayanamshas.None, 
                    ObserverPositions.GeoCentric, dummyLocation, secundaryJd, progPoints );
                ProgPositions positions;
                if (response.Positions.TryGetValue(ChartPoints.Sun, out positions))
                {
                    double sunSec = positions.Longitude;
                    return sunSec - sunRadix;
                }
                else throw new InvalidDataException();
            }
            case SymbolicKeys.MeanSun:
            {
                double meanSpeedOfSun = 360.0 / EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
                return timeSpanInYears * meanSpeedOfSun;
            }
            case SymbolicKeys.OneDegree:
                return timeSpanInYears;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
    }
    
}