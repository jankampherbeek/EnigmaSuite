// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Configuration;
using Enigma.Api.Slices;
using Enigma.Core.Slices.ZodiacDivisions;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support.Conversions;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>
/// Model for zodiac divisions (terms, decans etc.)
/// </summary>
public class ZodiacDivisionsModel
{
    private readonly ZodiacDivisionsService _zdService;
    private readonly IConfigurationApi _cfgApi;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly char[] _planetGlyphs = new char[] { 'a', 'b', 'c', 'd', 'f', 'g', 'h' }; // Sun, Moon, Merc., Venus, Mars, Jup., Sat.
    private readonly char[] _signGlyphs = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=' }; // Aries .. Pisces

    public ZodiacDivisionsModel(ZodiacDivisionsService zdService, IConfigurationApi cfgApi, IDoubleToDmsConversions doubleToDmsConversions)
    {
        _zdService = zdService;
        _cfgApi = cfgApi;
        _doubleToDmsConversions = doubleToDmsConversions;
    }

    public bool UseDecansPlanet { get; set; }
    public bool UseDodecatsOrignal { get; set; }
    public bool UseBoundsEgyptian { get; set; }
    
    /// <summary>
    /// Get metadata information including chart name and configuration values
    /// </summary>
    /// <returns>Combined metadata text</returns>
    public string GetMetadataText()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        var currentConfig = _cfgApi.GetCurrentConfiguration();
        
        if (currentChart == null)
        {
            return "No chart loaded";
        }
        
        var chartName = currentChart.InputtedChartData.MetaData.Name;
        var ayanamshaText = GetAyanamshaText(currentConfig.Ayanamsha);
        var observerPositionText = GetObserverPositionText(currentConfig.ObserverPosition);
        var projectionTypeText = GetProjectionTypeText(currentConfig.ProjectionType);
        
        return $"Chart: {chartName}, Ayanamsha: {ayanamshaText}, Observer: {observerPositionText}, Projection: {projectionTypeText}";
    }
    
    private string GetAyanamshaText(Ayanamshas ayanamsha)
    {
        var details = ayanamsha.GetDetails();
        return details.RbKey.Replace("ref.ayanamsha.", "").Replace(".", " ").ToUpperInvariant();
    }
    
    private string GetObserverPositionText(ObserverPositions observerPosition)
    {
        var details = observerPosition.GetDetails();
        return details.RbKey.Replace("ref.observerpos.", "").Replace(".", " ").ToUpperInvariant();
    }
    
    private string GetProjectionTypeText(ProjectionTypes projectionType)
    {
        var details = projectionType.GetDetails();
        return details.RbKey.Replace("ref.projectiontype.", "").Replace(".", " ").ToUpperInvariant();
    }
    
    /// <summary>
    /// Define the indexes 
    /// </summary>
    /// <remarks>
    /// Prompt: define the chartpoints that will be used by checking currentConfig.ChartPoints that have the value 'IsUsed'
    /// in the value of Chartpoint. Define all methods that will be used by checking the enum ZodiacDivisionMethods.
    /// Always use Signs. If useDecansPlanet is true select DecansPlanet,otherwise DecansSign. If useDodecatsOriginal
    /// is true select DodecatsOriginal, otherwise select DodecatsPaulus, if UseboundsEgyptian is true select
    /// BoundsEgyptian, otherwise select BoundsPtolemy. Define a 2d array of chars. Each row should contain 5 columns,
    /// the glyph of the planet from currentConfig (see Glyph in the value), and 4 rows with the glyphs of the indexes
    /// which can be found via the method GlyphForIndex. The last 4 rows should contain the chars for the glyphs of the
    /// selected methods: signs, decans, dodecatemoria, bounds (in that sequence). You can use the methode DefineSingleIndex
    /// to calculate the required index and GlyphForIndex to get the glyphs. 
 
    /// </remarks>
    /// <returns></returns>
    public List<KeyValuePair<ChartPoints, string[]>> DefineAllTextsForGrid()
    {
        var allTexts = new List<KeyValuePair<ChartPoints, string[]>>();
        
        var currentConfig = _cfgApi.GetCurrentConfiguration();
        var currentChart = _dataVaultCharts.GetCurrentChart();
        var usedChartPoints = currentConfig.ChartPoints.Where(cp => cp.Value.IsUsed).ToList();
        
        // Get used chart points that actually exist in the current chart
        var availableChartPoints = new List<KeyValuePair<ChartPoints, ChartPointConfigSpecs>>();
        foreach (var chartPoint in usedChartPoints)
        {
            if (currentChart.Positions.ContainsKey(chartPoint.Key))
            {
                availableChartPoints.Add(chartPoint);
            }
        }
        
        // Define methods to use 
        var signsMethod = ZodiacDivisionMethods.Signs; // Always use Signs
        var decansMethod = UseDecansPlanet ? ZodiacDivisionMethods.DecansPlanet : ZodiacDivisionMethods.DecansSign;
        var dodecatsMethod = UseDodecatsOrignal ? ZodiacDivisionMethods.DodecatsOriginal : ZodiacDivisionMethods.DodecatsPaulus;
        var boundsMethod = UseBoundsEgyptian ? ZodiacDivisionMethods.BoundsEgyptian : ZodiacDivisionMethods.BoundsPtolemy;
        
        
        // Process each available chart point
        for (int i = 0; i < availableChartPoints.Count; i++)
        {
            var chartPoint = availableChartPoints[i];
            var longitude = currentChart.Positions[chartPoint.Key].Ecliptical.MainPosSpeed.Position;
            var textLine = new string[6];
            // Column 0: Planet glyph
            textLine[0] = chartPoint.Value.Glyph.ToString();
            // Column 1: Longitude
            textLine[1] = _doubleToDmsConversions.ConvertDoubleToDmsInSignNoGlyph(longitude);
            // Column 2: Signs 
            var signsIndex = DefineSingleIndex(longitude, signsMethod);
            textLine[2] = GlyphForIndex(signsIndex, signsMethod).ToString();
            // Column 3: Decans index
            var decansIndex = DefineSingleIndex(longitude, decansMethod);
            textLine[3] = GlyphForIndex(decansIndex, decansMethod).ToString();
            // Column 4: Dodecatemoria index
            var dodecatsIndex = DefineSingleIndex(longitude, dodecatsMethod);
            textLine[4] = GlyphForIndex(dodecatsIndex, dodecatsMethod).ToString();
            // Column 5: Bounds index
            var boundsIndex = DefineSingleIndex(longitude, boundsMethod);
            textLine[5] = GlyphForIndex(boundsIndex, boundsMethod).ToString();
            allTexts.Add(new KeyValuePair<ChartPoints, string[]>(chartPoint.Key, textLine));
        }
        return allTexts;
    }

    // Define index for zodiac division for a single longitude and one method
    private int DefineSingleIndex(double longitude, ZodiacDivisionMethods method)
    {
        int index;
        try
        {
            index = _zdService.FindIndexForDivision(longitude, method);
        }
        catch (Exception e)
        {
            Log.Error($"Could not define zodiac division ofr longitude {longitude} and method {method}. Original exception: {e.Message}");
            throw;
        }
        return index;
    }

    // Define the glyph for an index, either a sign or a planet, based on the method
    private char GlyphForIndex(int index, ZodiacDivisionMethods method)
    {
        // check for sign based methods
        if (method == ZodiacDivisionMethods.Signs || method == ZodiacDivisionMethods.DecansSign ||
            method == ZodiacDivisionMethods.DodecatsOriginal || method == ZodiacDivisionMethods.DodecatsPaulus)
        {
            return _signGlyphs[index];
        }
        // not sign based so return index for planets
        return _planetGlyphs[index];
    }
} 