// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Views;

public partial class CalcHouseComparisonWindow : Window
{
    public CalcHouseComparisonWindow()
    {
        InitializeComponent();
        DefineColors();
    }
    
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }
    
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}