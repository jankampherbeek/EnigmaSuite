// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Enigma.Frontend.Ui.Support;

/// <summary>Mapper between colors and color names.</summary>
/// <remarks>Enigma supports only 15 colors.</remarks>
public interface IColorMapper
{
    /// <summary>Finds name for given color.</summary>
    /// <param name="color">The color to find the name for.</param>
    /// <returns>The resulting name.</returns>
    public string NameFromColor(Color color);

    /// <summary>Finds color for given name.</summary>
    /// <param name="name">The name to find the color for.</param>
    /// <returns>The resulting color.</returns>
    public Color ColorFromName(string name);
}

/// <inheritdoc/>
public class ColorMapper:IColorMapper
{
    private List<Tuple<string, Color>> _mappedColors;

    public ColorMapper()
    {
        _mappedColors = CreateMapping();
    }
    
    /// <inheritdoc/>
    public string NameFromColor(Color color)
    {
        return FindNameFromColor(color);
    }

    /// <inheritdoc/>
    public Color ColorFromName(string name)
    {
        return FindColorFromName(name);
    }

    private string FindNameFromColor(Color colorToFind)
    {
        string name = "Unknown color";
        foreach (Tuple<string, Color> mappedColor in _mappedColors.Where(mappedColor => mappedColor.Item2 == colorToFind))
        {
            name = mappedColor.Item1;
        }
        return name;
    }

    private Color FindColorFromName(string nameToFind)
    {
        Color color = Color.White;
        foreach (Tuple<string, Color> mappedColor in _mappedColors.Where(mappedColor => mappedColor.Item1 == nameToFind))
        {
            color = mappedColor.Item2;
        }
        return color;
    }
    
    private static List<Tuple<string, Color>> CreateMapping()
    {
        List<Tuple<string, Color>> colors = new()
        {
            new Tuple<string, Color>("Green", Color.Green),
            new Tuple<string, Color>("YellowGreen", Color.YellowGreen),
            new Tuple<string, Color>("SpringGreen", Color.SpringGreen),
            new Tuple<string, Color>("Red", Color.Red),
            new Tuple<string, Color>("Magenta", Color.Magenta),
            new Tuple<string, Color>("Purple", Color.Purple),
            new Tuple<string, Color>("Blue", Color.Blue),
            new Tuple<string, Color>("DeepSkyBlue", Color.DeepSkyBlue),
            new Tuple<string, Color>("CornflowerBlue", Color.CornflowerBlue),
            new Tuple<string, Color>("Gold", Color.Gold),
            new Tuple<string, Color>("Orange", Color.Orange),
            new Tuple<string, Color>("Gray", Color.Gray),
            new Tuple<string, Color>("Silver", Color.Silver),
            new Tuple<string, Color>("Black", Color.Black),
            new Tuple<string, Color>("Goldenrod", Color.Goldenrod)
        };
        return colors;
    }
}