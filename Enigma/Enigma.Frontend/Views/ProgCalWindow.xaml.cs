// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Enigma.Core.Slices.ProgCalendar;
using Enigma.Frontend.Ui.ViewModels;
using ScottPlot;
using ScottPlot.TickGenerators;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for Progressive Calendar</summary>
public partial class ProgCalWindow
{
    private ProgCalViewModel? _viewModel;

    public ProgCalWindow()
    {
        InitializeComponent();
        DataContextChanged += ProgCalWindow_DataContextChanged;

        if (DataContext is ProgCalViewModel existingViewModel)
        {
            AttachViewModel(existingViewModel);
            UpdatePeriodsPlot(existingViewModel.ProgCalPeriods);
        }
    }
    


    private void UseParallelsCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateParallels(true);
        }
    }
    
    private void UseParallelsCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateParallels(false);
        }
    }

    private void UseSecundaryCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateSecundary(true);
        }
    }
    
    private void UseSecundaryCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateSecundary(false);
        }
    }

    private void UseExtPeriodCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateExtPeriod(true);
        }
    }
    
    private void UseExtPeriodCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateExtPeriod(false);
        }
    }
    
    private void ProgCalWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is ProgCalViewModel oldViewModel)
        {
            oldViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }

        if (e.NewValue is ProgCalViewModel newViewModel)
        {
            AttachViewModel(newViewModel);
            UpdatePeriodsPlot(newViewModel.ProgCalPeriods);
        }
    }

    private void AttachViewModel(ProgCalViewModel viewModel)
    {
        _viewModel = viewModel;
        viewModel.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not ProgCalViewModel viewModel)
        {
            return;
        }

        if (e.PropertyName == nameof(ProgCalViewModel.ProgCalPeriods))
        {
            Dispatcher.Invoke(() => UpdatePeriodsPlot(viewModel.ProgCalPeriods));
        }
    }

    private void UpdatePeriodsPlot(IReadOnlyList<PresentableProgCalPeriod>? periods)
    {
        if (ProgCalPeriodsPlot is null)
        {
            return;
        }

        var plot = ProgCalPeriodsPlot.Plot;
        plot.Clear();

        if (periods is null || periods.Count == 0)
        {
            NoPeriodsText.Visibility = Visibility.Visible;
            ProgCalPeriodsPlot.Visibility = Visibility.Collapsed;
            if (ProgCalAxisPlot is not null)
            {
                ProgCalAxisPlot.Visibility = Visibility.Collapsed;
            }
            ProgCalPeriodsPlot.Refresh();
            return;
        }

        NoPeriodsText.Visibility = Visibility.Collapsed;
        ProgCalPeriodsPlot.Visibility = Visibility.Visible;
        if (ProgCalAxisPlot is not null)
        {
            ProgCalAxisPlot.Visibility = Visibility.Visible;
        }

        var orderedPeriods = periods
            .OrderBy(p => p.DateTimeStart)
            .ThenBy(p => p.DateTimeEnd)
            .ToList();

        var comboIndex = new Dictionary<string, int>();
        var yLabels = new List<string>();
        const double halfHeight = 0.24;
        const double spacing = 0.18;
        var minDate = double.MaxValue;
        var maxDate = double.MinValue;

        int GetRowIndex(PresentableProgCalPeriod period)
        {
            var glyphs = $"{period.ProgPointGlyph}{period.AspectGlyph}{period.RadixGlyph}";
            if (!comboIndex.TryGetValue(glyphs, out var idx))
            {
                idx = comboIndex.Count;
                comboIndex[glyphs] = idx;
                yLabels.Add(glyphs);
            }

            return idx;
        }

        // First pass: determine all row indices and date ranges
        var periodData = new List<(int RowIndex, double Start, double End)>();
        foreach (var period in orderedPeriods)
        {
            var y = GetRowIndex(period);
            var start = period.DateTimeStart.ToOADate();
            var end = period.DateTimeEnd.ToOADate();
            if (end < start)
            {
                (start, end) = (end, start);
            }

            if (Math.Abs(end - start) < double.Epsilon)
            {
                end = start + 1.0 / 24; // minimum width of one hour
            }

            minDate = Math.Min(minDate, start);
            maxDate = Math.Max(maxDate, end);
            periodData.Add((y, start, end));
        }

        // Create a mapping from original row index to reversed row index
        // Original row 0 (earliest) should map to row totalRows-1 (top)
        var rowMapping = new Dictionary<int, int>();
        for (var i = 0; i < yLabels.Count; i++)
        {
            rowMapping[i] = yLabels.Count - 1 - i;
        }

        // Ensure we have valid date range for background rectangles
        var finalMaxDate = maxDate;
        if (Math.Abs(finalMaxDate - minDate) < double.Epsilon)
        {
            finalMaxDate = minDate + 1.0 / 24.0;
        }

        // Add alternating background colors for each row to improve visual alignment
        // Draw these first so they appear behind the period rectangles
        var totalRows = yLabels.Count;
        if (totalRows > 0 && minDate < double.MaxValue && finalMaxDate > double.MinValue)
        {
            for (var row = 0; row < totalRows; row++)
            {
                var rowBottom = row - 0.5;
                var rowTop = row + 0.5;
                // Alternate between light blue and white (starting with light blue for row 0)
                var backgroundColor = row % 2 == 0 ? Color.FromHex("#ADD8E6") : Color.FromHex("#FFFFFF");
                var backgroundRect = plot.Add.Rectangle(minDate, finalMaxDate, rowBottom, rowTop);
                backgroundRect.FillStyle.Color = backgroundColor;
                backgroundRect.LineStyle.Width = 0;
            }
        }

        // Second pass: plot with reversed row indices so earliest periods appear at the top
        foreach (var (y, start, end) in periodData)
        {
            // Map to reversed row index: original row 0 (earliest) becomes row totalRows-1 (top)
            var yReversed = rowMapping[y];
            var top = yReversed + halfHeight - spacing;
            var bottom = yReversed - halfHeight + spacing;
            var rect = plot.Add.Rectangle(start, end, bottom, top);
            rect.FillStyle.Color = Color.FromHex("#3F51B5");
            rect.LineStyle.Width = 0;
        }

        // Reverse labels so they match the reversed row positions
        // yLabels[0] (earliest) should be at position totalRows-1 (top)
        yLabels.Reverse();
        var yTicks = Enumerable.Range(0, yLabels.Count).Select(v => (double)v).ToArray();
        const double rowPixelHeight = 18d;
        ProgCalPeriodsPlot.Height = Math.Max(rowPixelHeight * yLabels.Count, 200d);

        plot.Axes.Left.TickGenerator = new NumericManual(yTicks, yLabels.ToArray());
        plot.Axes.Left.TickLabelStyle.FontName = "EnigmaAstrologyBLA2";
        plot.Axes.Left.TickLabelStyle.FontSize = 24;
        plot.Axes.Left.Label.Text = string.Empty;

        // Normal Y limits: 0 at bottom, max at top
        plot.Axes.SetLimitsY(-0.5, yLabels.Count - 0.5);

        if (Math.Abs(maxDate - minDate) < double.Epsilon)
        {
            maxDate = minDate + 1.0 / 24.0;
        }

        var dateTicks = new DateTimeAutomatic();
        plot.Axes.Bottom.TickGenerator = dateTicks;
        if (minDate < maxDate)
        {
            plot.Axes.SetLimitsX(minDate, maxDate);
        }

        if (yLabels.Count > 0 && minDate < double.MaxValue && maxDate > double.MinValue)
        {
            var separatorEnd = Math.Abs(maxDate - minDate) < double.Epsilon ? maxDate + 1.0 / 24 : maxDate;
            // Y limits are inverted, so boundaries are from max to min
            for (var boundary = yLabels.Count - 0.5; boundary >= -0.5; boundary -= 1.0)
            {
                var separator = plot.Add.Line(minDate, boundary, separatorEnd, boundary);
                separator.LineWidth = 1;
                separator.Color = Color.FromHex("#E0E0E0");
            }

            var dayStart = Math.Floor(minDate);
            var dayEnd = Math.Ceiling(separatorEnd);
            // Y limits are inverted: max at bottom, min at top
            var yMin = -0.5;
            var yMax = yLabels.Count - 0.5;
            for (var day = dayStart; day <= dayEnd; day += 1.0)
            {
                var vertical = plot.Add.Line(day, yMin, day, yMax);
                vertical.LineWidth = 1;
                vertical.Color = Color.FromHex("#F0F0F0");
            }
        }

        plot.Axes.Bottom.IsVisible = false;
        plot.Axes.Top.IsVisible = false;

        plot.FigureBackground.Color = Color.FromHex("#FFFFFF");
        plot.DataBackground.Color = Color.FromHex("#FFFFFF");

        plot.Legend.IsVisible = false;

        UpdateAxisPlot(minDate, maxDate, dateTicks);

        ProgCalPeriodsPlot.Refresh();
    }

    private void UpdateAxisPlot(double minDate, double maxDate, ITickGenerator tickGenerator)
    {
        if (ProgCalAxisPlot is null)
        {
            return;
        }

        var axisPlot = ProgCalAxisPlot.Plot;
        axisPlot.Clear();

        axisPlot.Axes.Left.IsVisible = false;
        axisPlot.Axes.Top.IsVisible = false;
        axisPlot.Axes.Right.IsVisible = false;

        axisPlot.Axes.Bottom.IsVisible = true;
        axisPlot.Axes.Bottom.TickGenerator = tickGenerator;
        axisPlot.Axes.Bottom.TickLabelStyle.Rotation = 45;
        axisPlot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.LowerRight;
        axisPlot.Axes.Bottom.Label.Text = "Date";

        axisPlot.Axes.SetLimitsX(minDate, maxDate);
        axisPlot.Axes.SetLimitsY(-1, 1);

        var baseline = axisPlot.Add.Line(minDate, 0, maxDate, 0);
        baseline.LineWidth = 0;
        baseline.Color = Color.FromHex("#00000000");

        ProgCalAxisPlot.Refresh();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        if (_viewModel is not null)
        {
            _viewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }
    }
}

