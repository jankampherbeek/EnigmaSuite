// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Interfaces;

public interface IAspectForDataGridFactory
{
    /// <summary>Builds a presentable aspect to be used in a grid.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Presentable aspects.</returns>
    List<PresentableAspects> CreateAspectForDataGrid(List<DefinedAspect> aspects);
}


public interface IAspectForWheelFactory
{
    /// <summary>Builds a drawable aspect between two celestial points, that can be used in a wheel.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Drawable aspects.</returns>
    List<DrawableCelPointAspect> CreateCelPointAspectForWheel(List<DefinedAspect> aspects);

    /// <summary>Builds a drawable aspect between a mundane point and a celestial point, that can be used in a wheel.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Drawable aspects.</returns>
    List<DrawableMundaneAspect> CreateMundaneAspectForWheel(List<DefinedAspect> aspects);
}

/// <summary>Prepare midpoint values to be shown in a datagrid.</summary>
public interface IMidpointForDataGridFactory
{
    /// <summary>Builds a presentable midpoint to be used in a grid.</summary>
    /// <param name="midpoints">Calculated midpoints.</param>
    /// <returns>Presentable midpoints.</returns>
    List<PresentableMidpoint> CreateMidpointsDataGrid(List<BaseMidpoint> midpoints);

    /// <summary>Builds a presentable occupied midpoint to be used in a grid.</summary>
    /// <param name="midpoints">Occupied midpoints.</param>
    /// <returns>Presentable occupied midpoints.</returns>
    List<PresentableOccupiedMidpoint> CreateMidpointsDataGrid(List<OccupiedMidpoint> midpoints);
}



public interface ICelPointForDataGridFactory
{
    List<PresentableCelPointPositions> CreateCelPointPosForDataGrid(List<FullChartPointPos> celPointPositions);
}


public interface IDataNameForDataGridFactory
{
    /// <summary>Builds a presentable data name to be used in a grid.</summary>
    /// <param name="dataNames">List with datanames.</param>
    /// <returns>Presentable data names.</returns>
    List<PresentableDataName> CreateDataNamesForDataGrid(List<string> fullPathDataNames);
}

/// <summary>Factory to create presentable harmonic positions.</summary>
public interface IHarmonicForDataGridFactory
{
    /// <summary>Create a presentabe list with combined radix and harmonic positions.</summary>
    /// <param name="harmonicPositions">List with all harmonic positions in the same sequence as the celestial points in chart, and followed by respectively Mc, Asc, Vertex and Eastpoint.</param>
    /// <param name="chart">Calculated chart.</param>
    /// <returns>The presentable positions.</returns>
    public List<PresentableHarmonic> CreateHarmonicForDataGrid(List<double> harmonicPositions, CalculatedChart chart);
}

public interface IHousePosForDataGridFactory
{
    List<PresentableHousePositions> CreateHousePosForDataGrid(FullHousesPositions fullHousesPositions);
}


public interface ISortedGraphicCelPointsFactory
{
    public List<GraphicCelPointPositions> CreateSortedList(List<FullChartPointPos> celPointPositions, double longitudeAsc, double minDistance);
}








