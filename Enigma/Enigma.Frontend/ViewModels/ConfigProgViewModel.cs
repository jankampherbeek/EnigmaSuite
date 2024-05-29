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
    
    [ObservableProperty] private string _primDirHeader;
    [ObservableProperty] private string _btnHelp;
    [ObservableProperty] private string _btnClose;
    [ObservableProperty] private string _btnSave;
    [ObservableProperty] private string _primDirTab;
    [ObservableProperty] private string _primDirHintMethod;
    [ObservableProperty] private string _primDirHintTimeKey;
    [ObservableProperty] private string _primDirHintLatAspects;
    [ObservableProperty] private string _primDirHintApproach;
    [ObservableProperty] private string _primDirHintConverse;
    [ObservableProperty] private string _primDirSignificators;
    [ObservableProperty] private string _primDirPromissors;
    [ObservableProperty] private string _primDirAspects;
    
    
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
        AllSymDirKeys =  new ObservableCollection<string>(ConfigProgModel.AllSymDirKeys());
        AllTransitPoints = new ObservableCollection<ProgPoint>(ConfigProgModel.AllTransitPoints());
        AllSecDirPoints = new ObservableCollection<ProgPoint>(ConfigProgModel.AllSecDirPoints());
        AllSymDirPoints = new ObservableCollection<ProgPoint>(ConfigProgModel.AllSymDirPoints());
        AllPdSignificators = new ObservableCollection<ProgPoint>(_model.AllSignificators());
        AllPdPromissors = new ObservableCollection<ProgPoint>(_model.AllPromissors());
        AllPdAspects = new ObservableCollection<ProgAspect>(_model.AllAspects());
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
        PdConverseIndex = _model.PdConverseIndex;
        PdLatAspectsIndex = _model.PdLatAspectsIndex;
    }

    private void DefineTexts()
    {
        BtnHelp = _rosetta.GetText("shr.btn.help");
        BtnClose = _rosetta.GetText("shr.btn.close");
        BtnSave = _rosetta.GetText("shr.btn.save");
        PrimDirTab = _rosetta.GetText("vw.configprog.tabpd");
        PrimDirHeader = _rosetta.GetText("vw.configprog.pd.title");
        PrimDirHintMethod = _rosetta.GetText("vw.configprog.pd.hintmethod");
        PrimDirHintTimeKey = _rosetta.GetText("vw.configprog.pd.hinttimekey");
        PrimDirHintLatAspects = _rosetta.GetText("vw.configprog.pd.hintlataspects");
        PrimDirHintApproach = _rosetta.GetText("vw.configprog.pd.hintmundanezodiac");
        PrimDirHintConverse = _rosetta.GetText("vw.configprog.pd.hintconverse");
        PrimDirSignificators = _rosetta.GetText("vw.configprog.pd.significators");
        PrimDirPromissors = _rosetta.GetText("vw.configprog.pd.promissors");
        PrimDirAspects = _rosetta.GetText("vw.configprog.pd.aspects");
        
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
                PrimDirConverseOptionsExtensions.PrimDirConverseOptionForIndex(PdConverseIndex),
                PrimDirLatAspOptionsExtensions.PrimDirLatAspOptionForIndex(PdLatAspectsIndex),
                AllPdSignificators.ToDictionary(point => point.ChartPoint, point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)),
                AllPdPromissors.ToDictionary(point => point.ChartPoint, point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)),
                AllPdAspects.ToDictionary(aspect => aspect.Aspect, aspect => new AspectConfigSpecs(aspect.IsUsed, aspect.Glyph, 0, false)));
            ConfigProg configProg = new(configTransits, configSecDir, configSymDir, configPrimDir); 
            _model.UpdateConfig(configProg);
            MessageBox.Show(PROG_CONFIG_SAVED, StandardTexts.TITLE_ERROR);
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