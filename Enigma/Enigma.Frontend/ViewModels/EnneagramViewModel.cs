// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>
/// ViewModel for the Enneagram window
/// </summary>
[SuppressMessage("ReSharper", "UnusedParameterInPartialMethod")]
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
    [ObservableProperty] private bool _countPlutoTwice;

    // Results
    [ObservableProperty] private ObservableCollection<EnneagramTypeResult> _enneagramResults = [];
    [ObservableProperty] private ObservableCollection<EnneagramDetailResult> _enneagramDetails = [];

    // Chart information
    [ObservableProperty] private string _chartName = "No chart loaded";
    [ObservableProperty] private string _descriptionText = "Select chart points and options to calculate Enneagram strengths.";

    // Canvas properties for drawing
    [ObservableProperty] private double _canvasWidth = 400;
    [ObservableProperty] private double _canvasHeight = 400;
    [ObservableProperty] private List<EnneagramCircle> _enneagramCircles = [];
    [ObservableProperty] private List<EnneagramLine> _enneagramLines = [];
    [ObservableProperty] private bool _canvasNeedsRedraw;

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
            EnneagramDetails.Clear();
            UpdateEnneagramDrawing();
            return;
        }

        var selectedPoints = GetSelectedChartPoints();
        if (!selectedPoints.Any())
        {
            EnneagramResults.Clear();
            EnneagramDetails.Clear();
            UpdateEnneagramDrawing();
            return;
        }

        var strengths = _model.CalculateEnneagramStrengths(selectedPoints, IncludeHouses, CountPlutoTwice);
        var details = _model.CalculateEnneagramDetails(selectedPoints, IncludeHouses, CountPlutoTwice);
        
        // Update strengths results
        EnneagramResults.Clear();
        foreach (var strength in strengths)
        {
            EnneagramResults.Add(new EnneagramTypeResult
            {
                Type = strength.Key,
                Name = EnneagramModel.GetEnneagramTypeName(strength.Key),
                Strength = strength.Value,
                TooltipText = GetEnneagramTooltipText(strength.Key)
            });
        }

        // Update details results
        EnneagramDetails.Clear();
        foreach (var detail in details)
        {
            EnneagramDetails.Add(new EnneagramDetailResult
            {
                ChartPointName = GetChartPointName(detail.Point),
                PositionType = detail.InSigns ? "Sign" : "House",
                Position = detail.PositionIndex,
                Type1Factor = detail.Factors[0],
                Type2Factor = detail.Factors[1],
                Type3Factor = detail.Factors[2],
                Type4Factor = detail.Factors[3],
                Type5Factor = detail.Factors[4],
                Type6Factor = detail.Factors[5],
                Type7Factor = detail.Factors[6],
                Type8Factor = detail.Factors[7],
                Type9Factor = detail.Factors[8]
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
                Name = EnneagramModel.GetEnneagramTypeName(type),
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
    /// Get the name for a chart point
    /// </summary>
    /// <param name="point">Chart point</param>
    /// <returns>Name of the chart point</returns>
    private static string GetChartPointName(ChartPoints point)
    {
        return point switch
        {
            ChartPoints.Sun => "Sun",
            ChartPoints.Moon => "Moon",
            ChartPoints.Mercury => "Mercury",
            ChartPoints.Venus => "Venus",
            ChartPoints.Mars => "Mars",
            ChartPoints.Jupiter => "Jupiter",
            ChartPoints.Saturn => "Saturn",
            ChartPoints.Uranus => "Uranus",
            ChartPoints.Neptune => "Neptune",
            ChartPoints.Pluto => "Pluto",
            ChartPoints.Chiron => "Chiron",
            ChartPoints.TrueNode => "True Node",
            ChartPoints.ApogeeMean => "Apogee Mean",
            ChartPoints.Ascendant => "Ascendant",
            ChartPoints.Mc => "MC",
            _ => point.ToString()
        };
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
    private static string GetEnneagramTooltipText(int type)
    {
        return type switch
        {
            1 => "The Perfectionist:\n\nPrincipled, purposeful, self-controlled, and perfectionistic. Type Ones are conscientious and ethical, with a strong sense of right and wrong. They are teachers, crusaders, and advocates for change: always striving to improve things, but afraid of making a mistake. Well-organized, orderly, and fastidious, they try to maintain high standards but can slip into being critical and perfectionistic. They typically have problems with repressed anger and impatience. At their best: wise, discerning, realistic, and noble. Can be morally heroic.",
            2 => "The Helper:\n\nGenerous, people-pleasing, and possessive. Type Twos are empathetic, sincere, and warm-hearted. They are friendly, generous, and self-sacrificing, but can also be sentimental, flattering, and people-pleasing. They are well-meaning and driven to be close to others, but can slip into doing things for others in order to be needed. They typically have problems with possessiveness and with recognizing their own needs. At their best: unselfish and altruistic, they have unconditional love for others.",
            3 => "The Winner:\n\nAdaptable, excelling, driven, and image-conscious. Type Threes are self-assured, attractive, and charming. Ambitious, competent, and energetic, they can also be status-conscious and highly driven for advancement. They are diplomatic and poised, but can also be overly concerned with their image and what others think of them. They typically have problems with workaholism and competitiveness. At their best: self-accepting, authentic, everything they seem to be—role models who inspire others.",
            4 => "The Feeler:\n\nExpressive, dramatic, self-absorbed, and temperamental. Type Fours are self-aware, sensitive, and reserved. They are emotionally honest, creative, and personal, but can also be moody and self-conscious. Withholding themselves from others due to feeling vulnerable and defective, they can also feel disdainful and exempt from ordinary ways of living. They typically have problems with self-indulgence and self-pity. At their best: inspired and highly creative, they can renew themselves and transform their experiences.",
            5 => "The Observer:\n\nPerceptive, innovative, secretive, and isolated. Type Fives are alert, insightful, and curious. They are able to concentrate and focus on developing complex ideas and skills. Independent, innovative, and inventive, they can also become preoccupied with their thoughts and imaginary constructs. They become detached, yet high-strung and intense. They typically have problems with eccentricity, nihilism, and isolation. At their best: visionary pioneers, often ahead of their time, and able to see the world in an entirely new way.",
            6 => "The Loyalist:\n\nEngaging, responsible, anxious, and suspicious. Type Sixes are reliable, hard-working, responsible, and trustworthy. Excellent 'troubleshooters,' they foresee problems and foster cooperation, but can also become defensive, evasive, and anxious—running on stress while complaining about it. They can be cautious and indecisive, but also reactive, defiant and rebellious. They typically have problems with self-doubt and suspicion. At their best: internally stable and self-reliant, courageously championing themselves and others.",
            7 => "The Optimist:\n\nBusy, fun-loving, and scattered. Type Sevens are extroverted, optimistic, versatile, and spontaneous. Playful, high-spirited, and practical, they can also misapply and scatter their many talents, becoming over-extended, scattered, and undisciplined. They constantly seek new and exciting experiences, but can become distracted and exhausted by staying on the go. They typically have problems with impatience and impulsiveness. At their best: they focus their talents on worthwhile goals, becoming appreciative, joyous, and satisfied.",
            8 => "The Leader:\n\nPowerful, dominating, self-confident, and confrontational. Type Eights are self-confident, strong, and assertive. Protective, resourceful, straight-talking, and decisive, but can also be ego-centric and domineering. Eights feel they must control their environment, especially people, sometimes becoming confrontational and intimidating. Eights typically have problems with their tempers and with allowing themselves to be vulnerable. At their best: self-mastering, they use their strength to improve others' lives, becoming heroic, magnanimous, and inspiring.",
            9 => "The Peacemaker:\n\nReceptive, reassuring, complacent, and resigned. Type Nines are accepting, trusting, and stable. They are usually creative, optimistic, and supportive, but can also be too willing to go along with others to keep the peace. They want everything to go smoothly and be without conflict, but they can also be complacent, simplifying problems and minimizing anything upsetting. They typically have problems with inertia and stubbornness. At their best: indomitable and all-embracing, they are able to bring people together and heal conflicts.",
            _ => $"Type {type}: Description not available."
        };
    }
}

/// <summary>
/// Result for an Enneagram type strength calculation
/// </summary>
public class EnneagramTypeResult
{
    public int Type { get; init; }
    public string Name { get; set; } = "";
    public double Strength { get; init; }
    public string TooltipText { get; set; } = "";
}

/// <summary>
/// Result for an Enneagram detail calculation
/// </summary>
public class EnneagramDetailResult
{
    public string ChartPointName { get; set; } = "";
    public string PositionType { get; set; } = ""; // "Sign" or "House"
    public int Position { get; set; } // Sign number (1-12) or House number (1-12)
    public double Type1Factor { get; set; }
    public double Type2Factor { get; set; }
    public double Type3Factor { get; set; }
    public double Type4Factor { get; set; }
    public double Type5Factor { get; set; }
    public double Type6Factor { get; set; }
    public double Type7Factor { get; set; }
    public double Type8Factor { get; set; }
    public double Type9Factor { get; set; }
}

/// <summary>
/// Circle for Enneagram drawing
/// </summary>
public class EnneagramCircle
{
    public int Type { get; init; }
    public string Name { get; init; } = "";
    public double X { get; init; }
    public double Y { get; init; }
    public double Radius { get; init; }
    public bool IsHighest { get; set; }
    public Color Color { get; init; }
    public string Tooltip { get; init; } = "";
}

/// <summary>
/// Line for Enneagram drawing
/// </summary>
public class EnneagramLine
{
    public int FromType { get; init; }
    public int ToType { get; init; }
} 