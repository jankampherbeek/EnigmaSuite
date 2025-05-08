// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistables;
using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>Selection of points to use in research.</summary>
/// <param name="SelectedPoints">Selected chart points.</param>
/// <param name="IncludeCusps">True if all cusps are used, otherwise false.</param>
public record ResearchPointSelection(List<ChartPoints> SelectedPoints, bool IncludeCusps);


/// <summary>Positions and inputdata for a chart in a research project.</summary>
/// <param name="Positions">All relevant positions for celestial points.</param>
/// <param name="Obliquity">Obliquity.</param>
/// <param name="InputItem">Inputted data.</param>
public record CalculatedResearchChart(
    Dictionary<ChartPoints, FullPointPos> Positions, 
    double Obliquity, 
    StandardInputItem InputItem);


/// <summary>Positions and inputdata for a chart in a research project while using only one coordinate.</summary>
/// <param name="Positions">Positions for one coordinate</param>
/// <param name="Obliquity">Obliquity</param>
/// <param name="InputItem">Inputted data</param>
public record ResearchChartSingleCoord(
    Dictionary<ChartPoints, double> Positions,
    double Obliquity,
    StandardInputItem InputItem);

/// <summary>Definition of points that should be excluded when performing a research action.</summary>
/// <remarks>One of these records should be used to specify the exclusions when using a specific research method.</remarks>
/// <param name="ExcludedPoints">List of ChartPoints to exclude.</param>
/// <param name="ExcludeCusps">True if cusps should be excluded. Angles are defined as part of the excludedpoints.</param>
public record PointsToExclude(List<ChartPoints> ExcludedPoints, bool ExcludeCusps);


/// <summary>Data Transfer Object for DataFiles table.</summary>
public class DataFileDto
{
    /// <summary>Id of the data file</summary>
    public int Id { get; set; } = -1;
    /// <summary>Name of the data file.</summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>Location of the data file.</summary>
    public string Location { get; set; } = string.Empty;
}

/// <summary>Data Transfer Object for Projects table.</summary>
public class ProjectDto
{
    /// <summary>Id of the project</summary>
    public int Id { get; set; }
    
    /// <summary>Name of the project.</summary>
    public string Name { get; set; } = string.Empty;
        
    /// <summary>Description of the project.</summary>
    public string Description { get; set; } = string.Empty;
        
    /// <summary>Location of the project.</summary>
    public string Location { get; set; } = string.Empty;
        
    /// <summary>Multi-factor indicator.</summary>
    public int MultiFactor { get; set; }
        
    /// <summary>Creation date of the project.</summary>
    public DateTime Created { get; set; }
        
    /// <summary>ID of the associated data file.</summary>
    public int DataFile { get; set; }
    
    /// <summary>ID of the controlgroup type that is used</summary>
    public int ControlGroupType { get; set; }
}

/// <summary>DTO for calculated research positions</summary>
public class ResearchPosition
{
    /// <summary>Abbreviation of the chartpoint</summary>
    public string Abbrev { get; set; }
    /// <summary>Position of the chartpoint</summary>
    public double Position { get; set; }
}

/// <summary>DTO for a chart with calculated research positions</summary>
public class ResearchPositionsForChart
{
    /// <summary>Id for this chart</summary>
    public string Id { get; set; }
    /// <summary>List of positions for this chart</summary>
    public List<ResearchPosition> Positions { get; set; }
}