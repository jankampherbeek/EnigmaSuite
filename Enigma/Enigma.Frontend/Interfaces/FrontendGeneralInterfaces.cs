// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain;
using Enigma.Domain.Analysis;
using Enigma.Domain.Positional;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.Interfaces;

public interface IAspectForDataGridFactory
{
    /// <summary>Builds a presentable aspect to be used in a grid.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Presentable aspects.</returns>
    List<PresentableAspects> CreateAspectForDataGrid(List<EffectiveAspect> aspects);
}


public interface IAspectForWheelFactory
{
    /// <summary>Builds a drawable aspect between two solar system points, that can be used in a wheel.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Drawable aspects.</returns>
    List<DrawableSolSysPointAspect> CreateSolSysAspectForWheel(List<EffectiveAspect> aspects);

    /// <summary>Builds a drawable aspect between a mundane point and a solar system point, that can be used in a wheel.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Drawable aspects.</returns>
    List<DrawableMundaneAspect> CreateMundaneAspectForWheel(List<EffectiveAspect> aspects);
}


public interface ICelPointForDataGridFactory
{
    List<PresentableSolSysPointPositions> CreateCelPointPosForDataGrid(List<FullSolSysPointPos> fullSolSysPointPositions);
}


public interface IDataNameForDataGridFactory
{
    /// <summary>Builds a presentable data name to be used in a grid.</summary>
    /// <param name="dataNames">List with datanames.</param>
    /// <returns>Presentable data names.</returns>
    List<PresentableDataName> CreateDataNamesForDataGrid(List<string> fullPathDataNames);
}



public interface IHousePosForDataGridFactory
{
    List<PresentableHousePositions> CreateHousePosForDataGrid(FullHousesPositions fullHousesPositions);
}


public interface ISortedGraphicSolSysPointsFactory
{
    public List<GraphicSolSysPointPositions> CreateSortedList(List<FullSolSysPointPos> solSysPointPositions, double longitudeAsc, double minDistance);
}

public interface IRangeCheck
{
    public double InRange360(double angle);
}


/// <summary>Manage texts that are stored in an external dictionary file.</summary>
public interface IRosetta
{
    /// <summary>Retrieve text from a resource bundle.</summary>
    /// <param name="id">The id to search.</param>
    /// <returns>The text for the Id. Returns the string '-NOT FOUND-' if the text could not be found.</returns>
    public string TextForId(string id);
}

public interface ITextFileReaderFE   // todo, check if this has the same functionality as textfilereader in persistency
{
    public IEnumerable<string> ReadSeparatedLines(string fileName);
    public string ReadAllText(string fileName);
}

/// <summary>Collection of available charts for the UI.</summary>
public interface ICurrentCharts
{

    /// <summary>Id for the chart that is currently active.</summary>
    public int IdPrimaryChart { get; set; }

    /// <summary>Id for a chart that is compared with the primary chart.</summary>
    public int IdSecondaryChart { get; set; }
    public void AddChart(CalculatedChart newChart, bool isPrimary, bool isSecondary);
    public void RemoveChart(int chartId);

}




