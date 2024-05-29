// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class OobCalViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.OOB_CAL;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _btnClose;
    [ObservableProperty] private string _btnHelp;
    [ObservableProperty] private string _header; 
    [ObservableProperty] private string _dayTxt;
    [ObservableProperty] private string _monthTxt;
    [ObservableProperty] private string _yearTxt;
    [ObservableProperty] private string _typeOfChange;
    [ObservableProperty] private ObservableCollection<PresentableOobEvents> _oobEvents;
    public OobCalViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<OobCalModel>();
        _description = model.DescriptiveText();
        _oobEvents = new ObservableCollection<PresentableOobEvents>(model.GetOobEvents());
        DefineTexts();
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

    private void DefineTexts()
    {
        Rosetta rosetta = Rosetta.Instance;
        BtnClose = rosetta.GetText("shr.btn.close");
        BtnHelp = rosetta.GetText("shr.btn.help");
        Header = rosetta.GetText("vw.oobcal.title");
        DayTxt = rosetta.GetText("vw.oobcal.day");
        MonthTxt = rosetta.GetText("vw.oobcal.month");
        YearTxt = rosetta.GetText("vw.oobcal.year");
        TypeOfChange = rosetta.GetText("vw.oobcal.type_of_chance");
    }

}