// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>
/// Create presentable version for data with zodiac divisions to be used in a datagrid
/// </summary>
public interface IZodiacDivisionForDataGridFactory
{
    /// <summary>
    /// Convert data to a presentable zodiac division
    /// </summary>
    /// <param name="dataList">Contains list of chartpoint and string array pairs : longitude and glyphs for sign, decan, dodecatemoria and bound</param>
    /// <returns>A presentable version of the data</returns>
    public IEnumerable<PresentableZodiacDivisions> CreateZodiacDivisionForDataGrid(List<KeyValuePair<ChartPoints, string[]>> dataList);

}

/// <inheritdoc/>
public class ZodiacDivisionForDataGridFactory: IZodiacDivisionForDataGridFactory
{
    /// <inheritdoc/>
    public IEnumerable<PresentableZodiacDivisions> CreateZodiacDivisionForDataGrid(List<KeyValuePair<ChartPoints, string[]>> dataList)
    {
        var allDivisions = new List<PresentableZodiacDivisions>();

        foreach (var data in dataList)
        {
            if (data.Value.Length < 6)
            {
                Log.Error($"ZodiacDivisionForDataGridFactory encountered a string array with less than 6 positions: {data}");
                continue;
            }

            var planet = data.Value[0];  // Planet glyph
            var longitude = data.Value[1]; // Longitude
            var signs = data.Value[2];   // Signs glyph
            var decans = data.Value[3];  // Decans glyph
            var dodecatemoria = data.Value[4]; // Dodecatemoria glyph
            var bounds = data.Value[5];  // Bounds glyph
            
            allDivisions.Add(new PresentableZodiacDivisions(longitude, planet, signs, decans, dodecatemoria, bounds));
        }
        return allDivisions;
    }
}

