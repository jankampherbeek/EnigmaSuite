﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Handlers.Calc.Helpers;


/// <inheritdoc/>
public sealed class CelPointSECalc : ICelPointSECalc
{
    private readonly ICalcUtFacade _calcUtFacade;
    private readonly IChartPointsMapping _mapping;

    public CelPointSECalc(ICalcUtFacade calcUtFacade, IChartPointsMapping chartPointsMapping)
    {
        _calcUtFacade = calcUtFacade;
        _mapping = chartPointsMapping;
    }

    /// <inheritdoc/>
    public PosSpeed[] CalculateCelPoint(ChartPoints celPoint, double jdnr, Location location, int flags)
    {
        int pointId = _mapping.SeIdForCelestialPoint(celPoint);
        double[] positions = _calcUtFacade.PositionFromSe(jdnr, pointId, flags);
        var mainPos = new PosSpeed(positions[0], positions[3]);
        var deviation = new PosSpeed(positions[1], positions[4]);
        var distance = new PosSpeed(positions[2], positions[5]);
        return new PosSpeed[] { mainPos, deviation, distance };
    }

}