// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>Conversions for presentable primary direction positions.</summary>
public interface IPrimDirForPresentationFactory
{
    /// <summary>Convert results primary directions PresentablePrimDirPositions.</summary>
    /// <param name="positions">The positions to convert.</param>
    /// <returns>The resulting PresentableProgPositions.</returns>
    public List<PresentablePrimDirs> CreatePresPrimDirs(List<PrimDirHit> presPdHits);
}

/// <inheritdoc/>
public sealed class PrimDirForPresentationFactory:IPrimDirForPresentationFactory
{
    private readonly GlyphsForChartPoints _glyphsForChartPoints;

    public PrimDirForPresentationFactory(GlyphsForChartPoints glyphsForChartPoints)
    {
        _glyphsForChartPoints = glyphsForChartPoints;
    }
    
    /// <inheritdoc/>
    public List<PresentablePrimDirs> CreatePresPrimDirs(List<PrimDirHit> pdHits)
    {
        List<PresentablePrimDirs> presPrimDirs = new(); 
        foreach (var pdHit in pdHits)
        {
            presPrimDirs.Add(CreateSinglePresPrimDir(pdHit));
        }
        return presPrimDirs;
    }

    
    private PresentablePrimDirs CreateSinglePresPrimDir(PrimDirHit pdHit)
    {
        char signGlyph = GlyphsForChartPoints.FindGlyph(pdHit.Significator);
        char promGlyph = GlyphsForChartPoints.FindGlyph(pdHit.Promissor);
        char aspGlyph = pdHit.Aspect.GetDetails().Glyph;
        return new PresentablePrimDirs(pdHit.DateTxt, promGlyph, aspGlyph, signGlyph, "D");
    }
    
}