// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>
/// ViewModel for the Enneagram window
/// </summary>
public partial class EnneagramViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.ENNEAGRAM;
    private readonly EnneagramModel _model;

    // Chart point selection properties
    [ObservableProperty] private bool _isSunSelected = true;
    [ObservableProperty] private bool _isMoonSelected = true;
    [ObservableProperty] private bool _isMercurySelected = true;
    [ObservableProperty] private bool _isVenusSelected = true;
    [ObservableProperty] private bool _isMarsSelected = true;
    [ObservableProperty] private bool _isJupiterSelected = true;
    [ObservableProperty] private bool _isSaturnSelected = true;
    [ObservableProperty] private bool _isUranusSelected = true;
    [ObservableProperty] private bool _isNeptuneSelected = true;
    [ObservableProperty] private bool _isPlutoSelected = true;
    [ObservableProperty] private bool _isChironSelected = true;
    [ObservableProperty] private bool _isTrueNodeSelected = true;
    [ObservableProperty] private bool _isApogeeMeanSelected = true;

    // Options
    [ObservableProperty] private bool _includeHouses = true;
    [ObservableProperty] private bool _countPlutoTwice = false;

    // Results
    [ObservableProperty] private ObservableCollection<EnneagramTypeResult> _enneagramResults = new();

    // Chart information
    [ObservableProperty] private string _chartName = "No chart loaded";
    [ObservableProperty] private string _descriptionText = "Select chart points and options to calculate Enneagram strengths.";

    // Canvas properties for drawing
    [ObservableProperty] private double _canvasWidth = 400;
    [ObservableProperty] private double _canvasHeight = 400;
    [ObservableProperty] private List<EnneagramCircle> _enneagramCircles = new();
    [ObservableProperty] private List<EnneagramLine> _enneagramLines = new();
    [ObservableProperty] private bool _canvasNeedsRedraw = false;

    public EnneagramViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<EnneagramModel>();
        UpdateChartInfo();
        CalculateEnneagram();
    }

    /// <summary>
    /// Update chart information when chart changes
    /// </summary>
    partial void OnIsSunSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsMoonSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsMercurySelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsVenusSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsMarsSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsJupiterSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsSaturnSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsUranusSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsNeptuneSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsPlutoSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsChironSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsTrueNodeSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIsApogeeMeanSelectedChanged(bool value) => CalculateEnneagram();
    partial void OnIncludeHousesChanged(bool value) => CalculateEnneagram();
    partial void OnCountPlutoTwiceChanged(bool value) => CalculateEnneagram();

    /// <summary>
    /// Update chart information
    /// </summary>
    private void UpdateChartInfo()
    {
        ChartName = _model.GetCurrentChartName();
        if (string.IsNullOrEmpty(ChartName))
        {
            ChartName = "No chart loaded";
            DescriptionText = "Please load a chart first to calculate Enneagram strengths.";
        }
        else
        {
            DescriptionText = "Select chart points and options to calculate Enneagram strengths.";
        }
    }

    /// <summary>
    /// Calculate Enneagram strengths based on current selections
    /// </summary>
    private void CalculateEnneagram()
    {
        if (!_model.IsChartLoaded())
        {
            EnneagramResults.Clear();
            UpdateEnneagramDrawing();
            return;
        }

        var selectedPoints = GetSelectedChartPoints();
        if (!selectedPoints.Any())
        {
            EnneagramResults.Clear();
            UpdateEnneagramDrawing();
            return;
        }

        var strengths = _model.CalculateEnneagramStrengths(selectedPoints, IncludeHouses, CountPlutoTwice);
        
        EnneagramResults.Clear();
        foreach (var strength in strengths)
        {
            EnneagramResults.Add(new EnneagramTypeResult
            {
                Type = strength.Key,
                Name = _model.GetEnneagramTypeName(strength.Key),
                Strength = strength.Value
            });
        }

        UpdateEnneagramDrawing();
    }

    /// <summary>
    /// Get the list of selected chart points
    /// </summary>
    /// <returns>List of selected chart points</returns>
    private List<ChartPoints> GetSelectedChartPoints()
    {
        var points = new List<ChartPoints>();
        
        if (IsSunSelected) points.Add(ChartPoints.Sun);
        if (IsMoonSelected) points.Add(ChartPoints.Moon);
        if (IsMercurySelected) points.Add(ChartPoints.Mercury);
        if (IsVenusSelected) points.Add(ChartPoints.Venus);
        if (IsMarsSelected) points.Add(ChartPoints.Mars);
        if (IsJupiterSelected) points.Add(ChartPoints.Jupiter);
        if (IsSaturnSelected) points.Add(ChartPoints.Saturn);
        if (IsUranusSelected) points.Add(ChartPoints.Uranus);
        if (IsNeptuneSelected) points.Add(ChartPoints.Neptune);
        if (IsPlutoSelected) points.Add(ChartPoints.Pluto);
        if (IsChironSelected) points.Add(ChartPoints.Chiron);
        if (IsTrueNodeSelected) points.Add(ChartPoints.TrueNode);
        if (IsApogeeMeanSelected) points.Add(ChartPoints.ApogeeMean);

        return points;
    }

    /// <summary>
    /// Update the Enneagram drawing based on current results
    /// </summary>
    private void UpdateEnneagramDrawing()
    {
        if (!EnneagramResults.Any())
        {
            EnneagramCircles.Clear();
            EnneagramLines.Clear();
            return;
        }

        // Sort results by strength descending
        var sortedResults = EnneagramResults.OrderByDescending(r => r.Strength).ToList();
        var highestStrengthType = sortedResults[0].Type;
        var secondType = sortedResults.Count > 1 ? sortedResults[1].Type : -1;
        var thirdType = sortedResults.Count > 2 ? sortedResults[2].Type : -1;

        // Create circles for each Enneagram type
        EnneagramCircles.Clear();
        var centerX = CanvasWidth / 2;
        var centerY = CanvasHeight / 2;
        var radius = Math.Min(CanvasWidth, CanvasHeight) * 0.35;
        var circleRadius = 25;

        // Enneagram positions: 9 at top, then clockwise 1-8
        var positions = new[] { 9, 1, 2, 3, 4, 5, 6, 7, 8 };
        for (int i = 0; i < positions.Length; i++)
        {
            var type = positions[i];
            var angle = i * 40 - 90; // Start at top (-90 degrees)
            var radians = angle * Math.PI / 180;
            var x = centerX + radius * Math.Cos(radians);
            var y = centerY + radius * Math.Sin(radians);

            Color color = Colors.Blue;
            if (type == highestStrengthType)
                color = Colors.Red;
            else if (type == secondType || type == thirdType)
                color = Colors.Orange;

            EnneagramCircles.Add(new EnneagramCircle
            {
                Type = type,
                Name = _model.GetEnneagramTypeName(type),
                X = x,
                Y = y,
                Radius = circleRadius,
                IsHighest = type == highestStrengthType,
                Color = color,
                Tooltip = GetEnneagramTooltipText(type)
            });
        }

        // Create lines connecting the Enneagram
        EnneagramLines.Clear();
        
        // Lines: 3-6-9 triangle
        EnneagramLines.Add(new EnneagramLine { FromType = 3, ToType = 6 });
        EnneagramLines.Add(new EnneagramLine { FromType = 6, ToType = 9 });
        EnneagramLines.Add(new EnneagramLine { FromType = 9, ToType = 3 });
        
        // Lines: 1-4-2-8-5-7-1 path
        EnneagramLines.Add(new EnneagramLine { FromType = 1, ToType = 4 });
        EnneagramLines.Add(new EnneagramLine { FromType = 4, ToType = 2 });
        EnneagramLines.Add(new EnneagramLine { FromType = 2, ToType = 8 });
        EnneagramLines.Add(new EnneagramLine { FromType = 8, ToType = 5 });
        EnneagramLines.Add(new EnneagramLine { FromType = 5, ToType = 7 });
        EnneagramLines.Add(new EnneagramLine { FromType = 7, ToType = 1 });
        
        // Trigger canvas redraw
        CanvasNeedsRedraw = !CanvasNeedsRedraw;
    }

    /// <summary>
    /// Handle window resize
    /// </summary>
    /// <param name="width">New width</param>
    /// <param name="height">New height</param>
    public void OnWindowResize(double width, double height)
    {
        CanvasWidth = width;
        CanvasHeight = height;
        UpdateEnneagramDrawing();
    }

    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private static void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    /// <summary>
    /// Get the name for an Enneagram type
    /// </summary>
    /// <param name="type">Enneagram type (1-9)</param>
    /// <returns>Name of the Enneagram type</returns>
    public string GetEnneagramTypeName(int type)
    {
        return type switch
        {
            1 => "Perfectionist",
            2 => "Helper",
            3 => "Winner",
            4 => "Feeler",
            5 => "Observer",
            6 => "Loyalist",
            7 => "Optimist",
            8 => "Leader",
            9 => "Peacemaker",
            _ => $"Type {type}"
        };
    }

    /// <summary>
    /// Get tooltip text for an Enneagram type
    /// </summary>
    /// <param name="type">Enneagram type (1-9)</param>
    /// <returns>Tooltip text for the Enneagram type</returns>
    public string GetEnneagramTooltipText(int type)
    {
        return type switch
        {
            1 => "The perfectionist (world improver, idealist) is a (lower) gut type, focused on improving undesirable things in himself and others.\n\n" +
                 "Type 1 can get worked up over imperfections, but usually hides his anger. Yet that anger over perceived injustice is a driving " +
                 "force (frustration type). Repels negative impulses by acting well behaved and formal (reaction formation).\n\n" +
                 "Sets high standards for himself, but cannot take criticism from others well.\n\n" +
                 "Pitfalls: squeamishness, burnout because the bar is set high.\n" +
                 "Integration point is the cheerful seven.",
            2 => "The helper is a heart type, focused on helping others. Helpers want to be needed and use their networks to do so.\n\n" +
                 "They take pride in the role they play in other people's lives, they find it hard to distance themselves from it (Jewish mother). " +
                 "Use willpower to bend things to their will.\n\n" +
                 "Pitfalls: Manipulation of others, meddling, forgetting/suppressing own needs, division " +
                 "of the world in- and outgroups (whoever is not for me is against me).\n\n" +
                 "When unappreciated, twos resemble an unhealthy eight and seek revenge.\n" +
                 "Introspection (four-behavior) brings them closer to their own needs and feelings.",
            3 => "The winner (successful worker, doer) is a heart type, focused on appreciation from others for his achievements. I perform, therefore I am.\n\n" +
                 "Outward success earns them appreciation; failure is a disaster and makes them work even harder.\n\n" +
                 "Like the 2 and 4, type 3 is an image type, easily adapting to social environment (chameleon). " +
                 "Because of their acting skills, threes can mimic other types well without being those types with heart and soul.\n\n" +
                 "Pitfalls: Self-deception through identification with outward goals, image outweighs actual achievements (sprucing up resume), " +
                 "may have difficulty listening to body.\n\n" +
                 "Integration point is the more socially minded loyalist.\n" +
                 "Resting point is the nine (lazing around after burnout).",
            4 => "The tragic romantic (feeler) is a heart type, focused on others. The four wants to stand out from others by being real and unique.\n\n" +
                 "I am unique, therefore I am. Avoiding mundanity and superficiality, the feeler seeks refuge in art, eccentric clothes, origins, creativity " +
                 "and original thoughts and deep emotions.\n\n" +
                 "Pitfalls: Envy, pride, pessimism, division squared.\n\n" +
                 "The integration point of the four is the more objective type 1.",
            5 => "The Observer (Thinker, Observer) is a main type, observing the world from a distance. I think, therefore I am (Descartes).\n\n" +
                 "The observer has difficulty plunging into life; he wants to know all about it first.\n\n" +
                 "He avoids dependence on others or fate by gaining more and more knowledge.\n\n" +
                 "Pitfalls: Greed, retreating into an ivory tower.\n\n" +
                 "Integration point is the eight, who is not afraid of direct experiences in the here and now.\n" +
                 "The thinker sometimes releases the brakes and then behaves like an extroverted bon vivant.",
            6 => "The loyalist (questioner, devil's advocate) is a main type. Sixes avoid uncertainty and seek their support in groups.\n\n" +
                 "They have a love-hate relationship with authority. Loyalists also tend to score on the three and nine (the 3, 6 and 9 are attachment types).\n\n" +
                 "The restless contrafobic six, like the boss, seeks boundaries, but has a 6>3>9 pattern. The more timid phobic six has a 6>9>6 pattern.\n\n" +
                 "Integration point is the nine: Despite all uncertainties in life trusting that everything will work out.",
            7 => "The bon vivant (optimist, planner) is main type, who likes to escape into the future to avoid the real problems.\n\n" +
                 "Actively seeks pleasure, avoids pain and sorrow. Everything must be fun, the seven idealizes even the past.\n\n" +
                 "Pitfalls: hypomanic behavior, rationalization.",
            8 => "The boss (leader) is an (under)gut type, focused on power and control. He does not show his vulnerability.\n\n" +
                 "The boss says directly what he stands for ('sacred innocence').\n\n" +
                 "A dominant boss can easily overwhelm others without realizing it.\n\n" +
                 "Pitfalls: Excess (lust).\n\n" +
                 "Integration point is the type 2 (use power to protect others).\n" +
                 "Under pressure, they withdraw as the five.",
            9 => "The peacemaker (mediator) is a belly type, focused on inner peace (acadia). He avoids conflict, as a heart type, " +
                 "feels others well, can mediate well, but can easily forget his own interests.\n\n" +
                 "Has great difficulty with prioritizing.\n\n" +
                 "Pitfalls: Not seeing one's own needs, getting lost in trivialities, drudgery and numbness.\n\n" +
                 "His integration point is the type 3, which is much more focused.",
            _ => $"Text for Enneagram type {type} not found"
        };
    }

    
}

/// <summary>
/// Result for an Enneagram type
/// </summary>
public class EnneagramTypeResult
{
    public int Type { get; set; }
    public string Name { get; set; } = "";
    public double Strength { get; set; }
}

/// <summary>
/// Circle for Enneagram drawing
/// </summary>
public class EnneagramCircle
{
    public int Type { get; set; }
    public string Name { get; set; } = "";
    public double X { get; set; }
    public double Y { get; set; }
    public double Radius { get; set; }
    public bool IsHighest { get; set; }
    public Color Color { get; set; }
    public string Tooltip { get; set; } = "";
}

/// <summary>
/// Line for Enneagram drawing
/// </summary>
public class EnneagramLine
{
    public int FromType { get; set; }
    public int ToType { get; set; }
} 