// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace Enigma.Frontend.Ui.Views;

public class InverseBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue;
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue;
        return value;
    }
}

/// <summary>View for chart wheel</summary>
/// <remarks>Still using MVC instead of MVVM for this view as binding multiple visuals with a canvas is rather challenging</remarks>
public partial class ChartsWheelWindow
{
    private ChartsWheelCanvasController _canvasController;

    public ChartsWheelWindow()
    {
        InitializeComponent();
        _canvasController = App.ServiceProvider.GetRequiredService<ChartsWheelCanvasController>();
        
        // Set up property change handling for the ViewModel
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is ChartsWheelViewModel viewModel)
        {
            // Sync the controller with the ViewModel
            _canvasController.ShowSignBackgroundColors = viewModel.ShowSignBackgroundColors;
            
            // Set up property change notification
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            
            System.Diagnostics.Debug.WriteLine($"[DEBUG] DataContext changed: ShowSignBackgroundColors = {viewModel.ShowSignBackgroundColors}");
        }
    }

    private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"[DEBUG] OnViewModelPropertyChanged called: PropertyName = {e.PropertyName}");
        if (e.PropertyName == nameof(ChartsWheelViewModel.ShowSignBackgroundColors))
        {
            var viewModel = (ChartsWheelViewModel)sender;
            _canvasController.ShowSignBackgroundColors = viewModel.ShowSignBackgroundColors;
            System.Diagnostics.Debug.WriteLine($"[DEBUG] ViewModel property changed: ShowSignBackgroundColors = {viewModel.ShowSignBackgroundColors}");
            Populate();
        }
    }

    public void Populate()
    {
        WheelCanvas.Children.Clear();
        _canvasController.PrepareDraw();
        DrawChartFrame();
        DrawCusps();
        DrawCelPoints();
        if (!_canvasController.NoAspects)
        {
            DrawAspects();
        }
    }



    private void DrawChartFrame()
    {
        System.Diagnostics.Debug.WriteLine($"[DEBUG] DrawChartFrame: Canvas size: {WheelCanvas.Width}x{WheelCanvas.Height}");
        AddToWheel(new List<UIElement>(_canvasController.WheelCircles));
        AddToWheel(new List<UIElement>(_canvasController.SignBackgroundSectors));
        AddToWheel(new List<UIElement>(_canvasController.SignSeparators));
        AddToWheel(new List<UIElement>(_canvasController.SignGlyphs));
        AddToWheel(new List<UIElement>(_canvasController.DegreeLines));
    }


    private void DrawCusps()
    {
        AddToWheel(new List<UIElement>(_canvasController.CuspLines));
        AddToWheel(new List<UIElement>(_canvasController.CuspCardinalLines));
        AddToWheel(new List<UIElement>(_canvasController.CuspTexts));
        AddToWheel(new List<UIElement>(_canvasController.CuspCardinalIndicators));
    }

    private void DrawCelPoints()
    {
        AddToWheel(new List<UIElement>(_canvasController.CelPointGlyphs));
        AddToWheel(new List<UIElement>(_canvasController.CelPointConnectLines));
        AddToWheel(new List<UIElement>(_canvasController.CelPointTexts));
    }

    private void DrawAspects()
    {
        AddToWheel(new List<UIElement>(_canvasController.AspectLines));
    }

    private void AddToWheel(List<UIElement> uiElements)
    {
        if (uiElements.Count == 0)
        {
            System.Diagnostics.Debug.WriteLine("[DEBUG] AddToWheel: List is empty");
        }
        else
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] AddToWheel: Adding {uiElements.Count} elements of type {uiElements.FirstOrDefault()?.GetType().Name ?? "None"}");
        }
        foreach (var uiElement in uiElements)
        {
            WheelCanvas.Children.Add(uiElement);
        }
    }

    private void WheelGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double availHeight = ActualHeight - 220.0;
        double minSize = Math.Min(availHeight, ActualWidth);
        _canvasController.Resize(minSize);
        WheelCanvas.Height = _canvasController.CanvasSize;
        WheelCanvas.Width = _canvasController.CanvasSize;
        Populate();
    }

    private void NoTime_Checked(object sender, RoutedEventArgs e)
    {
        _canvasController.NoTime = true;
        Populate();
    }

    private void NoTime_Unchecked(object sender, RoutedEventArgs e)
    {
        _canvasController.NoTime = false;
        Populate();
    }

    private void NoAspects_Checked(object sender, RoutedEventArgs e)
    {
        _canvasController.NoAspects = true;
        Populate();
    }

    private void NoAspects_UnChecked(object sender, RoutedEventArgs e)
    {
        _canvasController.NoAspects = false;
        Populate();
    }

    private void SignColors_Checked(object sender, RoutedEventArgs e)
    {
        _canvasController.ShowSignBackgroundColors = false;
        System.Diagnostics.Debug.WriteLine("[DEBUG] SignColors_Checked: Setting ShowSignBackgroundColors = false");
        Populate();
    }

    private void SignColors_Unchecked(object sender, RoutedEventArgs e)
    {
        _canvasController.ShowSignBackgroundColors = true;
        System.Diagnostics.Debug.WriteLine("[DEBUG] SignColors_Unchecked: Setting ShowSignBackgroundColors = true");
        Populate();
    }

    private void ExportClick(object sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                RenderTargetBitmap renderBitmap = new(
                    (int)WheelCanvas.ActualWidth,
                    (int)WheelCanvas.ActualHeight,
                    96d, 96d, PixelFormats.Pbgra32);
                renderBitmap.Render(WheelCanvas);

                PngBitmapEncoder encoder = new();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using FileStream file = File.Create(saveFileDialog.FileName);
                encoder.Save(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message, "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
