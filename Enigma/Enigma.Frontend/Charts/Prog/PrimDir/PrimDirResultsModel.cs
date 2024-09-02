// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Charts.Prog.PrimDir;
using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.Charts.Prog.PrimDir;

public class PrimDirResultsModel
{
    private readonly IPrimDirApi _primDirApi;
    private readonly ITextToDateConverter _textToDateConverter;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly IPrimDirForPresentationFactory _primDirForPresentationFactory;
    private DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private DataVaultProg _dataVaultProg = DataVaultProg.Instance;
    private ConfigProg _configProg;
    public string MethodName { get; }
    public string Period { get; set; }
    public string Details { get; }
    
    public List<PresentablePrimDirs> ActualPrimDirs { get; set; }
    
    public PrimDirResultsModel(IPrimDirApi primDirapi, 
        ITextToDateConverter textToDateConverter,
        IDescriptiveChartText descriptiveChartText,
        IPrimDirForPresentationFactory primDirForPresentationFactory)
    {
        _configProg = CurrentConfig.Instance.GetConfigProg();
        _primDirApi = primDirapi;
        _textToDateConverter = textToDateConverter;
        _descriptiveChartText = descriptiveChartText;
        _primDirForPresentationFactory = primDirForPresentationFactory;
        MethodName = DefineMethodName(_configProg.ConfigPrimDir);
        Period = DefinePeriod("", "");
        Details = DefineDetails();        
    }
    
    public void CalcActualPrimDirs()
    {
        string startDateTxt = _dataVaultProg.PrimDirStarDate;
        string endDateTxt = _dataVaultProg.PrimDirEndDate;
        Period = DefinePeriod(startDateTxt, endDateTxt);
        bool requestOk = CreateRequest(startDateTxt, endDateTxt, out PrimDirRequest pdRequest);

        if (requestOk)
        {
            PrimDirResponse pdResponse = _primDirApi.CalcPrimDir(pdRequest);
            ActualPrimDirs = _primDirForPresentationFactory.CreatePresPrimDirs(pdResponse.Hits);
        }
       
    }
    
    private bool CreateRequest(string startDateTxt, string endDateTxt, out PrimDirRequest request)
    {
        bool noErrors = true;
        Calendars cal = Calendars.Gregorian;
        ConfigProg configProg = CurrentConfig.Instance.GetConfigProg();
        AstroConfig configRadix = CurrentConfig.Instance.GetConfig();
        CalculatedChart? chart = _dataVaultCharts.GetCurrentChart();
        List<ChartPoints> significators = (from signSpec in configProg.ConfigPrimDir.Significators where signSpec.Value.IsUsed select signSpec.Key).ToList();
        List<ChartPoints> promissors = (from promSpec in configProg.ConfigPrimDir.Promissors where promSpec.Value.IsUsed select promSpec.Key).ToList();
        List<ChartPoints> supportedSignificators = new();
        List<ChartPoints> supportedPromissors = new();
        var supportedPoints = configRadix.ChartPoints;
        foreach (var signPoint in significators)
        {
           if (supportedPoints.ContainsKey(signPoint)) supportedSignificators.Add(signPoint); 
        }
        foreach (var promPoint in promissors)
        {
            if (supportedPoints.ContainsKey(promPoint)) supportedPromissors.Add(promPoint);
        }
        SimpleDate? startDateResult = null;
        SimpleDate? endDateResult = null;
        noErrors = noErrors && _textToDateConverter.ConvertText(startDateTxt, cal, out startDateResult);
        noErrors = noErrors && _textToDateConverter.ConvertText(endDateTxt, cal, out endDateResult);
        SimpleDate? startDate = noErrors && (startDateResult != null) ? startDateResult : null;
        SimpleDate? endDate = noErrors && (endDateResult != null) ? endDateResult : null;
        PrimDirMethods method = _configProg.ConfigPrimDir.Method; 
        PrimDirTimeKeys timeKey = _configProg.ConfigPrimDir.TimeKey;
        PrimDirApproaches approach = _configProg.ConfigPrimDir.Approach;
        request = new PrimDirRequest(chart, supportedSignificators, supportedPromissors, startDate, endDate, method, timeKey, approach);
        return noErrors;
    }

    private string DefineMethodName(ConfigProgPrimDir configPd)
    {
        string method = configPd.Method.ToString();
        string approach = configPd.Approach.ToString();
        string timeKey = configPd.TimeKey.ToString();
        return method + " " + approach + ". Time key: " + timeKey + ".";
    }

    private string DefinePeriod(string start, string end)
    {
        if (start.Trim().Length <= 5) return "Undefined";
        return start + " - " + end;
    }

    private string DefineDetails()
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        return chart != null
            ? _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData)
            : "";
    }
    
    
}