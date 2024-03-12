using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Views;

public partial class RadixLongitudeEquivalentsWindow : Window
{
    public RadixLongitudeEquivalentsWindow()
    {
        InitializeComponent();
        DefineColors();
    }
    
    private void DefineColors()
    {
        DescriptionBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.DESCRIPTION_BLOCK_COLOR)!;
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }
    
    
}