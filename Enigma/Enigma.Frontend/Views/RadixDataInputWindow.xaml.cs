// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Enigma.Domain.Constants;
using Enigma.Domain.LocationsZones;
using Enigma.Frontend.Ui.ViewModels;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for data input for a chart</summary>
public partial class RadixDataInputWindow
{
    public RadixDataInputWindow()
    {
        InitializeComponent();
        this.DataContext = new RadixDataInputViewModel();
        DefineColors();
    }
    
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        SubHeaderGeneral.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        SubHeaderLocation.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        SubHeaderDateTime.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        SubHeaderTimeZone.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
    }
    
    
    
}
