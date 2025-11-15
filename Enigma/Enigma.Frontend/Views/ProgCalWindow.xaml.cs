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
    
    private void UseAspectsCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateAspects(true);
        }
    }
    
    private void UseAspectsCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateAspects(false);
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

    
    private void UseRetroDirectCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateRetroDirect(true);
        }
    }
    
    private void UseRetroDirectCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateRetroDirect(false);
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
            ProgCalPeriodsPlot.Refresh();
            return;
        }

        NoPeriodsText.Visibility = Visibility.Collapsed;
        ProgCalPeriodsPlot.Visibility = Visibility.Visible;

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

            var top = y + halfHeight - spacing;
            var bottom = y - halfHeight + spacing;
            var rect = plot.Add.Rectangle(start, end, bottom, top);
            rect.FillStyle.Color = Color.FromHex("#3F51B5");
            rect.LineStyle.Width = 0;
        }

        var yTicks = Enumerable.Range(0, yLabels.Count).Select(v => (double)v).ToArray();
        const double rowPixelHeight = 18d;
        ProgCalPeriodsPlot.Height = Math.Max(rowPixelHeight * yLabels.Count, 200d);

        plot.Axes.Left.TickGenerator = new NumericManual(yTicks, yLabels.ToArray());
        plot.Axes.Left.TickLabelStyle.FontName = "EnigmaAstrologyBLA2";
        plot.Axes.Left.TickLabelStyle.FontSize = 24;
        plot.Axes.Left.Label.Text = string.Empty;

        plot.Axes.SetLimitsY(-0.5, yLabels.Count - 0.5);

        plot.Axes.Bottom.TickGenerator = new DateTimeAutomatic();
        if (minDate < maxDate)
        {
            plot.Axes.SetLimitsX(minDate, maxDate);
        }

        if (yLabels.Count > 0 && minDate < double.MaxValue && maxDate > double.MinValue)
        {
            var separatorEnd = Math.Abs(maxDate - minDate) < double.Epsilon ? maxDate + 1.0 / 24 : maxDate;
            for (var boundary = -0.5; boundary <= yLabels.Count - 0.5; boundary += 1.0)
            {
                var separator = plot.Add.Line(minDate, boundary, separatorEnd, boundary);
                separator.LineWidth = 1;
                separator.Color = Color.FromHex("#E0E0E0");
            }

            var dayStart = Math.Floor(minDate);
            var dayEnd = Math.Ceiling(separatorEnd);
            var yMin = -0.5;
            var yMax = yLabels.Count - 0.5;
            for (var day = dayStart; day <= dayEnd; day += 1.0)
            {
                var vertical = plot.Add.Line(day, yMin, day, yMax);
                vertical.LineWidth = 1;
                vertical.Color = Color.FromHex("#F0F0F0");
            }
        }

        plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
        plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.LowerRight;
        plot.Axes.Bottom.Label.Text = "Date";

        plot.FigureBackground.Color = Color.FromHex("#FFFFFF");
        plot.DataBackground.Color = Color.FromHex("#FFFFFF");

        plot.Legend.IsVisible = false;

        ProgCalPeriodsPlot.Refresh();
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

