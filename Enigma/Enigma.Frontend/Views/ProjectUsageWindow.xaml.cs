// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for project usage</summary>
public partial class ProjectUsageWindow
{
    public ProjectUsageWindow()
    {
        InitializeComponent();
        DefineColors();
    }
 
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    private void DefineColors()
    {
        ActionButtonBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.ACTION_BUTTON_BLOCK_COLOR)!;
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        SubHeaderProjectDetails.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        SubHeaderAvailableMethods.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
    }

}