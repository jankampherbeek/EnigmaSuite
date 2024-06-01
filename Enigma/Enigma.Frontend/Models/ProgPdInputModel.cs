// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Parsers;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for the input for primary directions.</summary>
public class ProgPdInputModel: DateTimeLocationModelBase
{
    private Rosetta _rosetta = Rosetta.Instance;
    private DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly IConfigurationApi _configApi;
    private readonly IDescriptiveChartText _descriptiveChartText;
    public int MethodIndex { get; }
    public int ApproachIndex { get; }
    public int TimeKeyIndex { get; }
    public int ConverseIndex { get; }
    public int LatAspectsIndex { get; }
    private ConfigProg _configProg;
    
    public ProgPdInputModel(IDateInputParser dateInputParser, ITimeInputParser timeInputParser, 
        IGeoLongInputParser geoLongInputParser, IGeoLatInputParser geoLatInputParser,
        IConfigurationApi configApi, IDescriptiveChartText descriptiveChartText) 
        : base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser)
    {
        _configApi = configApi;
        _configProg = CurrentConfig.Instance.GetConfigProg();
        _descriptiveChartText = descriptiveChartText;
        MethodIndex = (int)_configProg.ConfigPrimDir.Method;
        ApproachIndex = (int)_configProg.ConfigPrimDir.Approach;
        TimeKeyIndex = (int)_configProg.ConfigPrimDir.TimeKey;
        ConverseIndex = (int)_configProg.ConfigPrimDir.ConverseOption;
        LatAspectsIndex = (int)_configProg.ConfigPrimDir.LatAspOptions;
    }
    
    
    public List<string> AllMethods()
    {
        return PrimDirMethodsExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }
    
    public List<string> AllTimeKeys()
    {
        return PrimDirTimeKeysExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }
    
    public List<string> AllLatAspects()
    {
        return PrimDirLatAspOptionsExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }
    
    public List<string> AllConverseOptions()
    {
        return PrimDirConverseOptionsExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }

    public List<string> AllApproaches()
    {
        return PrimDirApproachesExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }
    
    public string DescriptiveText()
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        return chart != null
            ? _descriptiveChartText.FullDescriptiveText(config, chart.InputtedChartData)
            : "";
    }
}