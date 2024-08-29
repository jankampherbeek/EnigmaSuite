// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ConfigProgViewModel:ObservableObject
{
    private Rosetta _rosetta = Rosetta.Instance; 
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.CONFIG_PROG;
    private const string PROG_CONFIG_SAVED = "The configuration for progressions was successfully saved.";    
    [NotifyPropertyChangedFor(nameof(OrbSecDirValid))]
    [ObservableProperty] private string _orbSecDirText;
    [NotifyPropertyChangedFor(nameof(OrbSymDirValid))]
    [ObservableProperty] private string _orbSymDirText;
    [NotifyPropertyChangedFor(nameof(OrbTransitValid))]
    [ObservableProperty] private string _orbTransitText;

    [ObservableProperty] private ObservableCollection<string> _allSymDirKeys;
    [ObservableProperty] private ObservableCollection<string> _allPdMethods;
    [ObservableProperty] private ObservableCollection<string> _allPdTimeKeys;
    [ObservableProperty] private ObservableCollection<string> _allPdLatAspects;
    [ObservableProperty] private ObservableCollection<string> _allPdApproaches;
    [ObservableProperty] private ObservableCollection<string> _allPdConverseOptions;
    
    [ObservableProperty] private ObservableCollection<ProgPoint> _allTransitPoints;
    [ObservableProperty] private ObservableCollection<ProgPoint> _allSecDirPoints;
    [ObservableProperty] private ObservableCollection<ProgPoint> _allSymDirPoints;
    [ObservableProperty] private ObservableCollection<ProgPoint> _allPdSignificators;
    [ObservableProperty] private ObservableCollection<ProgPoint> _allPdPromissors;
    [ObservableProperty] private ObservableCollection<ProgAspect> _allPdAspects;
    [ObservableProperty] private int _symDirTimeKeyIndex;
    
    [ObservableProperty] private int _pdMethodIndex;
    [ObservableProperty] private int _pdTimeKeyIndex;
    [ObservableProperty] private int _pdApproachIndex;
    [ObservableProperty] private int _pdConverseIndex;
    [ObservableProperty] private int _pdLatAspectsIndex;

    [ObservableProperty] private string _title;
    [ObservableProperty] private string _btnHelp;
    [ObservableProperty] private string _btnClose;
    [ObservableProperty] private string _btnSave;
    [ObservableProperty] private string _hintOrb;
    [ObservableProperty] private string _primDirTab;
    [ObservableProperty] private string _primDirHeader;
    [ObservableProperty] private string _primDirHintMethod;
    [ObservableProperty] private string _primDirHintTimeKey;
    [ObservableProperty] private string _primDirHintApproach;
    [ObservableProperty] private string _primDirSignificators;
    [ObservableProperty] private string _primDirPromissors;


    [ObservableProperty] private string _secDirTab;
    [ObservableProperty] private string _secDirHeader;
    [ObservableProperty] private string _secDirPoints;
    
    [ObservableProperty] private string _symDirTab;
    [ObservableProperty] private string _symDirHeader;
    [ObservableProperty] private string _symDirPoints;
    [ObservableProperty] private string _symDirTimeKey;
    
    [ObservableProperty] private string _transitTab;
    [ObservableProperty] private string _transitHeader;
    [ObservableProperty] private string _transitPoints;
    
    
    
    private double _orbSecDirValue;
    private double _orbSymDirValue;
    private double _orbTransitValue;
    
    private bool _saveClicked;
    
    private readonly ConfigProgModel _model = App.ServiceProvider.GetRequiredService<ConfigProgModel>();
    
    public SolidColorBrush OrbSecDirValid => CheckOrbSecDir() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush OrbSymDirValid => CheckOrbSymDir() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush OrbTransitValid => CheckOrbTransit() ? Brushes.Gray : Brushes.Red;
    
    public ConfigProgViewModel()
    {
        DefineTexts();
        AllPdMethods = new ObservableCollection<string>(_model.AllPdMethods());
        AllPdConverseOptions = new ObservableCollection<string>(_model.AllPdConverseOptions());
        AllPdLatAspects = new ObservableCollection<string>(_model.AllPdLatAspects());
        AllPdTimeKeys = new ObservableCollection<string>(_model.AllPdTimeKeys());
        AllPdApproaches = new ObservableCollection<string>(_model.AllPdApproaches());
        AllSymDirKeys =  new ObservableCollection<string>(_model.AllSymDirKeys());
        AllTransitPoints = new ObservableCollection<ProgPoint>(_model.AllTransitPoints());
        AllSecDirPoints = new ObservableCollection<ProgPoint>(_model.AllSecDirPoints());
        AllSymDirPoints = new ObservableCollection<ProgPoint>(_model.AllSymDirPoints());
        AllPdSignificators = new ObservableCollection<ProgPoint>(_model.AllSignificators());
        AllPdPromissors = new ObservableCollection<ProgPoint>(_model.AllPromissors());
        SymDirTimeKeyIndex = _model.SymDirTimeKeyIndex;
        _orbSecDirValue = _model.SecDirOrb;
        _orbSymDirValue = _model.SymDirOrb;
        _orbTransitValue = _model.TransitOrb;
        OrbSecDirText = _orbSecDirValue.ToString((CultureInfo.InvariantCulture));
        OrbSymDirText = _orbSymDirValue.ToString((CultureInfo.InvariantCulture));
        OrbTransitText = _orbTransitValue.ToString((CultureInfo.InvariantCulture));
        
        PdMethodIndex = _model.PdMethodIndex;
        PdApproachIndex = _model.PdApproachIndex;
        PdTimeKeyIndex = _model.PdTimeKeyIndex;
   }

    private void DefineTexts()
    {
        Title = _rosetta.GetText("vw.configprog.title");
        BtnHelp = _rosetta.GetText("shr.btn.help");
        BtnClose = _rosetta.GetText("shr.btn.close");
        BtnSave = _rosetta.GetText("shr.btn.save");
        HintOrb = _rosetta.GetText("vw.configprog.hintorb");
        PrimDirTab = _rosetta.GetText("vw.configprog.tabpd");
        SecDirTab = _rosetta.GetText("vw.configprog.tabsc");
        SymDirTab = _rosetta.GetText("vw.configprog.tabsm");
        TransitTab = _rosetta.GetText("vw.configprog.tabtr");
        
        PrimDirHeader = _rosetta.GetText("vw.configprog.pd.title");
        PrimDirHintMethod = _rosetta.GetText("vw.configprog.pd.hintmethod");
        PrimDirHintTimeKey = _rosetta.GetText("vw.configprog.pd.hinttimekey");
        PrimDirHintApproach = _rosetta.GetText("vw.configprog.pd.hintmundanezodiac");
        PrimDirSignificators = _rosetta.GetText("vw.configprog.pd.significators");
        PrimDirPromissors = _rosetta.GetText("vw.configprog.pd.promissors");
       
        SecDirHeader = _rosetta.GetText("vw.configprog.sc.title");
        SecDirPoints = _rosetta.GetText("vw.configprog.sc.points");
        
        SymDirHeader = _rosetta.GetText("vw.configprog.sm.title");
        SymDirPoints = _rosetta.GetText("vw.configprog.sm.points");
        SymDirTimeKey = _rosetta.GetText("vw.configprog.sm.timekey");
        
        TransitHeader = _rosetta.GetText("vw.configprog.tr.title");
        TransitPoints = _rosetta.GetText("vw.configprog.tr.points");
    }
    
    
    private bool CheckOrbSecDir()
    {
        if (string.IsNullOrEmpty(OrbSecDirText) && !_saveClicked) return true; 
        return double.TryParse(OrbSecDirText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _orbSecDirValue);
    }
    
    private bool CheckOrbSymDir()
    {
        if (string.IsNullOrEmpty(OrbSymDirText) && !_saveClicked) return true; 
        return double.TryParse(OrbSymDirText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _orbSymDirValue);
    }
    
    private bool CheckOrbTransit()
    {
        if (string.IsNullOrEmpty(OrbTransitText) && !_saveClicked) return true; 
        return double.TryParse(OrbTransitText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _orbTransitValue);
    }

    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!CheckOrbSecDir())
        {
            errorsText.Append(StandardTexts.ERROR_ORB_SECDIR + EnigmaConstants.NEW_LINE);
        }
        if (!CheckOrbSymDir())
        {
            errorsText.Append(StandardTexts.ERROR_ORB_SYMDIR + EnigmaConstants.NEW_LINE);
        }
        if (!CheckOrbTransit())
        {
            errorsText.Append(StandardTexts.ERROR_ORB_TRANSIT + EnigmaConstants.NEW_LINE);
        }        
        return errorsText.ToString();
    }
    
    
    
    [RelayCommand]
    private void SaveConfig()
    {
        _saveClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            ConfigProgTransits configTransits = new(_orbTransitValue, AllTransitPoints.ToDictionary(point => point.ChartPoint, 
                point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)));
            ConfigProgSecDir configSecDir = new (_orbSecDirValue, AllSecDirPoints.ToDictionary(point => point.ChartPoint, 
                point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)));
            ConfigProgSymDir configSymDir = new(_orbSymDirValue,
                SymbolicKeyExtensions.SymbolicKeysForIndex(SymDirTimeKeyIndex),
                AllSymDirPoints.ToDictionary(point => point.ChartPoint, 
                    point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)));
            ConfigProgPrimDir configPrimDir = new(
                PrimDirMethodsExtensions.PrimDirMethodForIndex(PdMethodIndex),
                PrimDirApproachesExtensions.PrimDirApproachForIndex(PdApproachIndex),
                PrimDirTimeKeysExtensions.PrimDirTimeKeyForIndex(PdTimeKeyIndex),
                AllPdSignificators.ToDictionary(point => point.ChartPoint,
                    point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)),
                AllPdPromissors.ToDictionary(point => point.ChartPoint,
                    point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph))); 
                ConfigProg configProg = new(configTransits, configSecDir, configSymDir, configPrimDir); 
            _model.UpdateConfig(configProg);
            MessageBox.Show(PROG_CONFIG_SAVED);
            Log.Information("ConfigProgViewModel.SaveConfig(): send CloseMessage");
            WeakReferenceMessenger.Default.Send((new ProgConfigUpdatedMessage(VM_IDENTIFICATION)));            
            WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
        }
        else
        {
            MessageBox.Show(errors, StandardTexts.TITLE_ERROR);
        }        
    }
    
    [RelayCommand] private static void Close()
    {
        Log.Information("ConfigProgViewModel.Close(): send CloseMessage");        
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("ConfigProgViewModel.Help(): send HelpMessage");        
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));        
    }
    
}