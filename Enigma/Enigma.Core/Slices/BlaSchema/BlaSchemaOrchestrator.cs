// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

// I will use the orchestrator without an API, and try to access the orchestrator directly for the Model.

/// <summary>
/// Orchestrator for BLA Schema
/// </summary>
public class BlaSchemaOrchestrator
{
    private ChartLongitudes _chart;
    private List<BlaPointDetails> _pointDetails;
    private List<BlaHouseDetails> _houseDetails;
    private Dictionary<int, int> _houseCounts;
    private Dictionary<int, int> _signCounts;
    private Dictionary<ChartPoints, int> _planetsInSigns;
    private Dictionary<ChartPoints, int> _planetsInDecanates;
    private Dictionary<ChartPoints, int> _planetsInHouses;
    private Dictionary<int, int> _signsOnCusps;
    
    public BlaSchemaOrchestrator(ChartLongitudes chart, bool useDecanates)
    {
        _chart = chart;
        _pointDetails = [];
        foreach (var point in _chart.Points)
        {
            _pointDetails.Add(BlaDetailsFactory.CreateBlaPointDetails(point.Key, _chart));
        }
        
        _planetsInDecanates = new Dictionary<ChartPoints, int>();

        if (useDecanates)
        {
            foreach (var point in _pointDetails)
            {
                _planetsInDecanates.Add(point.Point, point.Decanate);           
            }            
        }
        _houseDetails = [];
        for (var i = 1; i < 13; i++)
        {
            _houseDetails.Add(BlaDetailsFactory.CreateBlaHouseDetails(i, _chart));
        }        
        _houseCounts =  HousePositions.DefineHouseCounts(_pointDetails);    
        _signCounts = SignPositions.DefineSignCounts(_pointDetails);          
        _planetsInSigns = _pointDetails.ToDictionary(x => x.Point, x => x.Sign);
        _planetsInHouses = HousePositions.DefineHousePositions(_chart); 
        _signsOnCusps = SignsOnCusps. DefineSignsOnCusps(_chart.Cusps);           
    }

    public List<BlaPointDetails> GetPointDetails()
    {
        return _pointDetails;
    }

    public List<BlaHouseDetails> GetHouseDetails()
    {
        return _houseDetails;   
    }

    public Dictionary<int, int> GetSignCounts()
    {
        return _signCounts;   
    }
    public Dictionary<int, int> GetHouseCounts()
    {
        return _houseCounts;
    }

    public Dictionary<ChartPoints, int> GetPlanetsInSigns()
    {
        return _planetsInSigns;
    }
    
    public Dictionary<ChartPoints, int> GetPlanetsInHouses()
    {
        return _planetsInHouses;
    }

    public Dictionary<int, int> GetSignsOnCusps()
    {
        return _signsOnCusps;
    }
    
    public (Dictionary<int, BlaSignHouseCountLine>, Dictionary<int, BlaSignHouseCountLine>) GetCrossELementCounts()
    {
        return CrossElementCounts.CreateCrossesElementsCounts(_signCounts, _houseCounts, _houseDetails);
    }
    
    public Dictionary<int, int> GetQuadrantCounts()
    {
        return QuadrantPositions.DefineQuadrants(_houseDetails);
    }
    
    public List<BlaDispositorLine> GetDispositors(bool useDecanates)
    {
        return Dispositors.CreateDispositors(_chart, _signCounts, _houseCounts, _signsOnCusps, _planetsInSigns, 
            _planetsInHouses, _planetsInDecanates, useDecanates);  
    }


    /// <summary>
    /// Define details: rulers of asc, sisterRuler asc, clampedhouses, interceptedsigns, groundnote, mundanehouseasc, sistersignCusp
    /// </summary>
    /// <returns>The calculated details</returns>
    public BlaDetailsData GetDetails()
    {
        return BlaDetails.CreateDetails(_chart, _signsOnCusps, _planetsInHouses);
    }

    public BlaCyclesData GetCycles()
    {
        return BlaCycles.CreateCyclesData(_planetsInHouses, _signsOnCusps);
    }

    public BlaCyclesData GetShortenedCycles()
    {
        return BlaCycles.CreateShortenedCyclesData(_planetsInHouses, _signsOnCusps);
    }
    
    // Reinforcements
    
    
    public Dictionary<ChartPoints, int> GetPointsInOwnSign()
    {
        return ReinforcementCalc.FindPointsInOwnSign(_planetsInSigns);
    }

    public Dictionary<ChartPoints, int> GetPointsInOwnHouse()
    {
        return ReinforcementCalc.FindPointsInOwnHouse(_planetsInHouses, _signsOnCusps);
    }

    public Dictionary<ChartPoints, int> GetPointsInOwnMundaneHouse()
    {
        return ReinforcementCalc.FindPointsInMundaneHouses(_planetsInHouses);        
    }

    public Dictionary<ChartPoints, int> GetRulersInHouseAsSign()
    {
        return ReinforcementCalc.FindRulerInHouseAsSign(_signsOnCusps, _planetsInSigns);        
    }

    public List<FactorPairAnalogHouseSign> GetFactorPairsAnalogHouseSigns()
    {
        return ReinforcementCalc.FindFactorPairs(_planetsInSigns, _planetsInHouses);
    }

    public List<Reception> GetReceptionsInSigns()
    {
        return ReinforcementCalc.FindReceptionInSigns(_planetsInSigns);        
    }

    public List<Reception> GetReceptionsInHouses()
    {
        return ReinforcementCalc.FindReceptionInHouses(_planetsInHouses, _signsOnCusps);        
    }

    public List<Reception> GetReceptionsInMundaneHouses()
    {
        return ReinforcementCalc.FindReceptionInMundaneHouses(_planetsInHouses, _signsOnCusps);
    } 
}