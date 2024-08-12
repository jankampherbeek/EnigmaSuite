// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api;
using Enigma.Api.Charts.Prog.PrimDir;
using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Parsers;

namespace Enigma.Frontend.Ui.Charts.Prog.PrimDir;

/// <summary>Model for the input for primary directions.</summary>
public class PrimDirInputModel: DateTimeLocationModelBase
{
    private Rosetta _rosetta = Rosetta.Instance;
    private DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly IConfigurationApi _configApi;
    private readonly IPrimDirApi _primDirApi;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly ITextToDateConverter _textToDateConverter;
    public int MethodIndex { get; }
    public int ApproachIndex { get; }
    public int TimeKeyIndex { get; }
    public int ConverseIndex { get; }
    public int LatAspectsIndex { get; }
    private ConfigProg _configProg;
    
    public PrimDirInputModel(IDateInputParser dateInputParser, ITimeInputParser timeInputParser, 
        IGeoLongInputParser geoLongInputParser, IGeoLatInputParser geoLatInputParser,
        IConfigurationApi configApi, IPrimDirApi primDirApi,  IDescriptiveChartText descriptiveChartText,
        ITextToDateConverter textToDateConverter) 
        : base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser)
    {
        _configApi = configApi;
        _primDirApi = primDirApi;
        _configProg = CurrentConfig.Instance.GetConfigProg();
        _descriptiveChartText = descriptiveChartText;
        _textToDateConverter = textToDateConverter;
        MethodIndex = (int)_configProg.ConfigPrimDir.Method;
        ApproachIndex = (int)_configProg.ConfigPrimDir.Approach;
        TimeKeyIndex = (int)_configProg.ConfigPrimDir.TimeKey;
        ConverseIndex = (int)_configProg.ConfigPrimDir.ConverseOption;
        LatAspectsIndex = (int)_configProg.ConfigPrimDir.LatAspOptions;
    }

    public List<PresentablePrimDirs> GetActualPrimDirs(string startDateTxt, string endDateTxt, int methodIndex, 
        int timeKeyIndex, int approachIndex, int converseIndex)
    {
        List<PresentablePrimDirs> allPrimDirs = new();
        bool requestOk = CreateRequest(startDateTxt, endDateTxt, methodIndex, timeKeyIndex, approachIndex, converseIndex, out PrimDirRequest pdRequest);

        if (requestOk)
        {
            PrimDirResponse pdResponse = _primDirApi.CalcPrimDir(pdRequest);
            string result = pdResponse.ToString();

            // todo populate presentable primdirs            
        }
        return allPrimDirs;
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

    private bool CreateRequest(string startDateTxt, string endDateTxt, int methodIndex, int timeKeyIndex, int approachIndex, int converseIndex, out PrimDirRequest request)
    {
        bool noErrors = true;
        Calendars cal = Calendars.Gregorian;
        ConfigProg configProg = CurrentConfig.Instance.GetConfigProg();
        CalculatedChart? chart = _dataVaultCharts.GetCurrentChart();
        List<ChartPoints> significators = (from signSpec in configProg.ConfigPrimDir.Significators where signSpec.Value.IsUsed select signSpec.Key).ToList();
        List<ChartPoints> promissors = (from promSpec in configProg.ConfigPrimDir.Promissors where promSpec.Value.IsUsed select promSpec.Key).ToList();
        List<AspectTypes> aspects = (from aspectSpec in configProg.ConfigPrimDir.Aspects where aspectSpec.Value.IsUsed select aspectSpec.Key).ToList();

        SimpleDate? startDateResult = null;
        SimpleDate? endDateResult = null;
        noErrors = noErrors && _textToDateConverter.ConvertText(startDateTxt, cal, out startDateResult);
        noErrors = noErrors && _textToDateConverter.ConvertText(endDateTxt, cal, out endDateResult);
        SimpleDate? startDate = noErrors && (startDateResult != null) ? startDateResult : null;
        SimpleDate? endDate = noErrors && (endDateResult != null) ? endDateResult : null;
        PrimDirMethods method = PrimDirMethodsExtensions.PrimDirMethodForIndex(methodIndex);
        PrimDirTimeKeys timeKey = PrimDirTimeKeysExtensions.PrimDirTimeKeyForIndex(timeKeyIndex);
        PrimDirApproaches approach = PrimDirApproachesExtensions.PrimDirApproachForIndex(approachIndex);
        PrimDirConverseOptions converse = PrimDirConverseOptionsExtensions.PrimDirConverseOptionForIndex(converseIndex);
        request = new PrimDirRequest(chart, significators, promissors, aspects, startDate, endDate, method, timeKey,
                approach, converse);
        return noErrors;
    }
    
}