// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Settings;
using Enigma.Frontend.Ui.State;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.ResearchProjects;


public class ResearchMethodInputController
{
    private readonly AstroConfig _currentAstroConfig;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly IRosetta _rosetta;

    public ResearchMethodInputController(IRosetta rosetta, IDoubleToDmsConversions doubleToDmsConversions)
    {
        _rosetta = rosetta;
        _currentAstroConfig = CurrentConfig.Instance.GetConfig();
        _doubleToDmsConversions = doubleToDmsConversions;
    }


    public List<ConfigItem> GetAllConfigItems()
    {
        List<ConfigItem> configItems = new();
        configItems.Add(new ConfigItem() 
        {
            ConfigName = _rosetta.TextForId("astroconfigwindow.housesystem"), 
            ConfigValue = _rosetta.TextForId(_currentAstroConfig.HouseSystem.GetDetails().TextId) 
        });
        configItems.Add(new ConfigItem()
        {
            ConfigName = _rosetta.TextForId("astroconfigwindow.zodiactype"),
            ConfigValue = _rosetta.TextForId(_currentAstroConfig.ZodiacType.GetDetails().TextId)
        });
        configItems.Add(new ConfigItem()
        {
            ConfigName = _rosetta.TextForId("astroconfigwindow.ayanamsha"),
            ConfigValue = _rosetta.TextForId(_currentAstroConfig.Ayanamsha.GetDetails().TextId)
        });
        configItems.Add(new ConfigItem()
        {
            ConfigName = _rosetta.TextForId("astroconfigwindow.observerpos"),
            ConfigValue = _rosetta.TextForId(_currentAstroConfig.ObserverPosition.GetDetails().TextId)
        });
        configItems.Add(new ConfigItem()
        {
            ConfigName = _rosetta.TextForId("astroconfigwindow.projectiontype"),
            ConfigValue = _rosetta.TextForId(_currentAstroConfig.ProjectionType.GetDetails().TextId)
        });
        configItems.Add(new ConfigItem()
        {
            ConfigName = _rosetta.TextForId("astroconfigwindow.baseorbaspect"),
            ConfigValue = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(_currentAstroConfig.BaseOrbAspects)
        });
        configItems.Add(new ConfigItem()
        {
            ConfigName = _rosetta.TextForId("astroconfigwindow.baseorbmidpoint"),
            ConfigValue = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(_currentAstroConfig.BaseOrbMidpoints)
        });
        return configItems;
    }
    

}


/// <summary>DTO for populating Listbox with configuration values.</summary>
public class ConfigItem
{
    public string ConfigName { get; set; }
    public string ConfigValue { get; set; }
}