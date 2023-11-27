// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for research result</summary>
public partial class ResearchResultWindow
{
    public ResearchResultWindow()
    {
        InitializeComponent();
        DefineColors();
    }
    
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        SubHeaderProject.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        SubHeaderMethod.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.SUB_HEADER_COLOR)!;
        TestData.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.TESTDATA_RESULTS_COLOR)!;
        ControlData.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.CONTROLDATA_RESULTS_COLOR)!;
    } 

}