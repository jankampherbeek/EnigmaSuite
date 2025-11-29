// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for Startpoint Zodiac</summary>
public partial class StartZodWindow
{
    public StartZodWindow()
    {
        InitializeComponent();
        DefineColors();
    }
    
    
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        ChartNameText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }
}
