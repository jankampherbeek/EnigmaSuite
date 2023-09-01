// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows.Controls;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Graphics;

/// <summary>
/// Text to be shown in a graphical environment.
/// </summary>
internal sealed class DimTextBlock
{
    private readonly FontFamily _fontFamily;
    private readonly double _fontSize;
    private readonly double _opacity;
    private readonly Color _color;
    public DimTextBlock(FontFamily fontFamily, double fontSize, double opacity, Color color)
    {
        _fontFamily = fontFamily;
        _fontSize = fontSize;
        _opacity = opacity;
        _color = color;
    }


    public TextBlock CreateTextBlock(string text, double posLeft, double posTop)
    {
        TextBlock textBlock = new()
        {
            Text = text,
            FontFamily = _fontFamily,
            FontSize = _fontSize,
            Opacity = _opacity,
            Foreground = new SolidColorBrush(_color)
        };
        Canvas.SetLeft(textBlock, posLeft);
        Canvas.SetTop(textBlock, posTop);
        return textBlock;
    }

    public TextBlock CreateTextBlock(string text, double posLeft, double posTop, RotateTransform transform)
    {
        TextBlock textBlock = CreateTextBlock(text, posLeft, posTop);
        textBlock.RenderTransform = transform;
        return textBlock;
    }

}
