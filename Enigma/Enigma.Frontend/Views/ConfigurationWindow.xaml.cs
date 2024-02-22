// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using Enigma.Domain.Constants;
using Color = System.Drawing.Color;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for configuration</summary>
public partial class ConfigurationWindow
{
    public ConfigurationWindow()
    {
        InitializeComponent();
        DefineColors();
    }
    
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        SubHeaderGeneral.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        SubHeaderPoints.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!; 
        SubHeaderAspects.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        SubHeaderColors.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        
        // Colors for aspectlines.
        ColorBlack.Background = new SolidColorBrush(Colors.Black);
        ColorBlack.Foreground = new SolidColorBrush(Colors.White);
        ColorGray.Background = new SolidColorBrush(Colors.Gray);
        ColorGray.Foreground = new SolidColorBrush(Colors.White);
        ColorSilver.Background = new SolidColorBrush(Colors.Silver);
        ColorSilver.Foreground = new SolidColorBrush(Colors.White);        
        ColorRed.Background = new SolidColorBrush(Colors.Red);
        ColorRed.Foreground = new SolidColorBrush(Colors.White);
        ColorOrange.Background = new SolidColorBrush(Colors.Orange);
        ColorOrange.Foreground = new SolidColorBrush(Colors.White);
        ColorGold.Background = new SolidColorBrush(Colors.Gold);
        ColorGold.Foreground = new SolidColorBrush(Colors.White);
        ColorGoldenrod.Background = new SolidColorBrush(Colors.Goldenrod);
        ColorGoldenrod.Foreground = new SolidColorBrush(Colors.White);
        ColorYellowGreen.Background = new SolidColorBrush(Colors.YellowGreen);
        ColorYellowGreen.Foreground = new SolidColorBrush(Colors.White);
        ColorSpringGreen.Background = new SolidColorBrush(Colors.SpringGreen);
        ColorSpringGreen.Foreground = new SolidColorBrush(Colors.White);
        ColorGreen.Background = new SolidColorBrush(Colors.Green);
        ColorGreen.Foreground = new SolidColorBrush(Colors.White);
        ColorBlue.Background = new SolidColorBrush(Colors.Blue);
        ColorBlue.Foreground = new SolidColorBrush(Colors.White);
        ColorDeepSkyBlue.Background = new SolidColorBrush(Colors.DeepSkyBlue);
        ColorDeepSkyBlue.Foreground = new SolidColorBrush(Colors.White);
        ColorCornflowerBlue.Background = new SolidColorBrush(Colors.CornflowerBlue);
        ColorCornflowerBlue.Foreground = new SolidColorBrush(Colors.White);
        ColorMagenta.Background = new SolidColorBrush(Colors.Magenta);
        ColorMagenta.Foreground = new SolidColorBrush(Colors.White);
        ColorPurple.Background = new SolidColorBrush(Colors.Purple);
        ColorPurple.Foreground = new SolidColorBrush(Colors.White);

        
    }

}