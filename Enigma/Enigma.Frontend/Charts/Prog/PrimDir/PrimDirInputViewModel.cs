// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Constants;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Charts.Prog.PrimDir;

public partial class PrimDirInputViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.CHARTS_PROG_PRIMDIR_INPUT;   
    private readonly PrimDirInputModel _model = App.ServiceProvider.GetRequiredService<PrimDirInputModel>();
    Rosetta _rosetta = Rosetta.Instance;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    
    
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _btnHelp;
    [ObservableProperty] private string _btnClose;
    [ObservableProperty] private string _btnCalculate;
    [ObservableProperty] private string _hintStartDate;
    [ObservableProperty] private string _hintEndDate;

    [NotifyCanExecuteChangedFor(nameof(FinalizeInputCommand))]
    [NotifyPropertyChangedFor(nameof(StartDateValid))]
    [ObservableProperty] private string _startDate = "";
    [NotifyCanExecuteChangedFor(nameof(FinalizeInputCommand))]
    [NotifyPropertyChangedFor(nameof(EndDateValid))]
    [ObservableProperty] private string _endDate = "";

    
    private bool _calculateClicked;
    
    public SolidColorBrush StartDateValid => IsStartDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush EndDateValid => IsEndDateValid() ? Brushes.Gray : Brushes.Red;
    
    
    public PrimDirInputViewModel()
    {
        DefineTexts();
    }

    private void DefineTexts()
    {
        BtnHelp = _rosetta.GetText("shr.btn.help");
        BtnClose = _rosetta.GetText("shr.btn.close");
        BtnCalculate = _rosetta.GetText("shr.btn.calculate");
        Title = _rosetta.GetText("vw.progpdinput.title");
        HintStartDate = _rosetta.GetText("vw.progpdinput.hintenddate");
        HintEndDate = _rosetta.GetText("vw.progpdinput.hintstartdate");
    }
    
    [RelayCommand]
    private void FinalizeInput()
    {
        _calculateClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            DataVaultProg.Instance.PrimDirStarDate = _startDate;
            DataVaultProg.Instance.PrimDirEndDate = _endDate;

            WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.PRIM_DIR_RESULTS));
            WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
        }
        else
        {
            MessageBox.Show(errors, StandardTexts.TITLE_ERROR);
        }

    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();

        if (!IsStartDateValid())
            errorsText.Append(_rosetta.GetText(StandardTexts.ERROR_STARTDATE) + EnigmaConstants.NEW_LINE);
        if (!IsEndDateValid())
            errorsText.Append(_rosetta.GetText(StandardTexts.ERROR_ENDDATE) + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
    }
    
    private bool IsStartDateValid()
    {
        if (string.IsNullOrEmpty(StartDate) && !_calculateClicked) return true; 
        return _model.IsDateValid(StartDate, Calendars.Gregorian, YearCounts.CE);
    }
    
    private bool IsEndDateValid()
    {
        if (string.IsNullOrEmpty(EndDate) && !_calculateClicked) return true; 
        return _model.IsDateValid(EndDate, Calendars.Gregorian, YearCounts.CE);
    }
    
    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
    }

    
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    

}