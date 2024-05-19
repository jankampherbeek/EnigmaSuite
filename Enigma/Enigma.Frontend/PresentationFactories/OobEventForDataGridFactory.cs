// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;
using Microsoft.Win32;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>Factory for building presentable OOB events.</summary>
public interface IOobEventForDataGridFactory
{
    /// <summary>Create a list of OOB events to be used in a data grid.</summary>
    /// <param name="oobEvents">The events.</param>
    /// <returns>The presentable events.</returns>
    List<PresentableOobEvents> CreateOobEventForDataGrid(IEnumerable<OobCalEvent> oobEvents);
}

// ====================== Implementation ==============================================

public class OobEventForDataGridFactory: IOobEventForDataGridFactory
{

    private readonly Rosetta _rosetta = Rosetta.Instance;
    
    public List<PresentableOobEvents> CreateOobEventForDataGrid(IEnumerable<OobCalEvent> oobEvents)
    {
        return oobEvents.Select(CreatePresOobEvent).ToList();

    }

    private PresentableOobEvents CreatePresOobEvent(OobCalEvent oobEvent)
    {
        char pointglyph = GlyphsForChartPoints.FindGlyph(oobEvent.Point);
        string typeOfChange = _rosetta.GetText(oobEvent.EventType.GetDetails().RbKey);
        return new PresentableOobEvents(oobEvent.Year, oobEvent.Month, oobEvent.Day, pointglyph, typeOfChange);
    }
    
  
}