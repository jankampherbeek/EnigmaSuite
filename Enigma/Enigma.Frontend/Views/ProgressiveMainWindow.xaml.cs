// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Views;

public partial class ProgressiveMainWindow : Window
{
    public ProgressiveMainWindow()
    {
        InitializeComponent();
        DefineColors();
    }
    
    private void DefineColors()
    {
        MainMenu.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.MENU_COLOR)!;
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        SubHeaderEventsPeriods.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        ActionButtonBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.ACTION_BUTTON_BLOCK_COLOR)!;
    }

    
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
}