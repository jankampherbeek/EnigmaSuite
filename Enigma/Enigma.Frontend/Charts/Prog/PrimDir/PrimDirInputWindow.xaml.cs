// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Charts.Prog.PrimDir;

public partial class PrimDirInputWindow : Window
{
    public PrimDirInputWindow()
    {
        InitializeComponent();
        DefineColors();
    }

    private void DefineColors()
    {
        DescriptionBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.DESCRIPTION_BLOCK_COLOR)!;
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        SubHeaderSelections.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }
}