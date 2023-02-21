// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
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
    public List<PresentableCommonPositions> CreateCelPointPosForDataGrid(Dictionary<ChartPoints, FullPointPos> commonPositions);
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
    public List<PresentableHousePositions> CreateHousePosForDataGrid(Dictionary<ChartPoints, FullPointPos> positions);
}


public interface ISortedGraphicCelPointsFactory
{
    public List<GraphicCelPointPositions> CreateSortedList(Dictionary<ChartPoints, FullPointPos> celPointPositions, double longitudeAsc, double minDistance);
}

/// <summary>Create presentable versions for chart data to be used in a datagrid.</summary>
public interface IChartDataForDataGridFactory
{
    /// <summary>Create list with PresentableChartData from list with CalculatedChart.</summary>
    /// <param name="charts">The cahrts to create presentable versions for.</param>
    /// <returns>The presentable versions of the charts data.</returns>
    public List<PresentableChartData> CreateChartDataForDataGrid(List<CalculatedChart> charts);

    /// <summary>Create list with PresentableChartData from list with PersistableChartData.</summary>
    /// <param name="charts">The cahrts to create presentable versions for.</param>
    /// <returns>The presentable versions of the charts data.</returns>
    public List<PresentableChartData> CreateChartDataForDataGrid(List<PersistableChartData> charts);
}

/// <summary>Textual conversions ofr location and coördinates.</summary>
public interface ILocationConversion
{
    /// <summary>Convert name and coördinatres to a string that presesents all infor for the location.</summary>
    /// <remarks>If no name is entered the name is replaced with a text that incidcates theo omission of the name.</remarks>
    /// <param name="locationName">Name for the location.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <param name="geoLong">Geographic longitude.</param>
    /// <returns>A text in the form: Enschede 52.21666666667 N / 6.9 E .</returns>
    public string CreateLocationDescription(string locationName, double geoLat, double geoLong);
}

/// <summary>Conversion to and from ChartData/PersistebleChartData.</summary>
public interface IChartDataConverter
{
    /// <summary>Convert PersistableChartData to ChartData.</summary>
    /// <param name="persistableChartData"/>
    /// <returns>Resulting ChartData.</returns>
    public ChartData FromPersistableChartData(PersistableChartData persistableChartData);

    /// <summary>Convert ChartData to PersistableChartData.</summary>
    /// <param name="chartData"/>
    /// <returns>Resulting PersistableChartData.</returns>
    public PersistableChartData ToPersistableChartData(ChartData chartData);

}

/// <summary>Calculation for a single chart.</summary>
public interface IChartCalculation
{

    /// <summary>Calculate the chart based on the current configuration.</summary>
    /// <param name="chartData">ChartData with all the required input.</param>
    /// <returns>The calculated chart.</returns>
    public CalculatedChart CalculateChart(ChartData chartData);
}


