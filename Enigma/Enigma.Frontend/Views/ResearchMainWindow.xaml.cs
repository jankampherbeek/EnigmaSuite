// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Views;

/// <summary>ViewModel for main research page</summary>
public partial class ResearchMainWindow
{
    public ResearchMainWindow()
    {
        InitializeComponent();
        DefineColors();
    }

    private void DefineColors()
    {
        MainMenu.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.MENU_COLOR)!;
        ActionButtonBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.ACTION_BUTTON_BLOCK_COLOR)!;
        DescriptionBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.DESCRIPTION_BLOCK_COLOR)!;              
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        SubHeaderExistingProjects.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;        
    }
    
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}