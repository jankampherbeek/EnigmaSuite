// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

public class ConfigProgModel
{
    
    
    public static List<string> AllPrimDirMethods()
    {
        return PrimaryDirMethodsExtensions.AllDetails().Select(method => method.MethodName).ToList();
    }
    
    public static List<string> AllPrimDirKeys()
    {
        return PrimaryKeyExtensions.AllDetails().Select(key => key.Text).ToList();
    }

    public static List<SelectableChartPointDetails> AllSignificators()
    {
        Dictionary<ChartPoints, ChartPointConfigSpecs> cpSpecs = CurrentConfig.Instance.GetConfig().ChartPoints;
        return (from cpSpec in cpSpecs 
            where cpSpec.Value.IsUsed let isSelected = false 
            select new SelectableChartPointDetails { Selected = isSelected, ChartPoint = cpSpec.Key, 
                Glyph = cpSpec.Value.Glyph, Name = cpSpec.Key.GetDetails().Text }).ToList();
    }

    
}