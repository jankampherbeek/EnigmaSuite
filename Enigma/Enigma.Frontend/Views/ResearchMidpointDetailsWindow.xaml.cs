// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for midpoint details in research</summary>
public partial class ResearchMidpointDetailsWindow
{
    public ResearchMidpointDetailsWindow()
    {
        InitializeComponent();
        DefineColors();
    }
    
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    } 

}