// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Calc.DateTime;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgPeriodViewModel: ObservableObject
{
    [NotifyCanExecuteChangedFor(nameof(FinalizePeriodCommand))]
    [NotifyPropertyChangedFor(nameof(StartDateValid))]
    [ObservableProperty] private string _startDate = "";
    [NotifyCanExecuteChangedFor(nameof(FinalizePeriodCommand))]
    [NotifyPropertyChangedFor(nameof(EndDateValid))]
    [ObservableProperty] private string _endDate = "";
    
    public SolidColorBrush StartDateValid => IsStartDateValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush EndDateValid => IsEndDateValid() ? Brushes.White : Brushes.Yellow;
    
    private readonly ProgPeriodModel _model = App.ServiceProvider.GetRequiredService<ProgPeriodModel>();
    
    [RelayCommand(CanExecute = nameof(IsInputOk))]
    private void FinalizePeriod()
    {
        //_model.CreatePeriodData(Description);
    }
    
    private bool IsInputOk()
    {
        if (StartDate == string.Empty || EndDate == string.Empty) return false;
        if (!IsStartDateValid() || !IsEndDateValid()) return false;
        return string.Compare(EndDate, StartDate, StringComparison.Ordinal) == -1;
    }
    
    private bool IsStartDateValid()
    {
        if (StartDate == string.Empty) return true;
        return _model.IsDateValid(StartDate, Calendars.Gregorian, YearCounts.CE);
    }
    private bool IsEndDateValid()
    {
        if (EndDate == string.Empty) return true;
        return _model.IsDateValid(EndDate, Calendars.Gregorian, YearCounts.CE);
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "ProgPeriodInput";    // TODO create helppage
        new HelpWindow().ShowDialog();
    }
    
}