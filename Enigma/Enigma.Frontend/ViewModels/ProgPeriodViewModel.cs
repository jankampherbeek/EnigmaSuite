// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.References;
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
    [NotifyCanExecuteChangedFor(nameof(FinalizePeriodCommand))]
    [NotifyPropertyChangedFor(nameof(DescriptionValid))]
    [ObservableProperty] private string _description = "";
    
    
    public SolidColorBrush StartDateValid => IsStartDateValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush EndDateValid => IsEndDateValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush DescriptionValid => IsDescriptionValid() ? Brushes.White : Brushes.Yellow;
    
    
    private readonly ProgPeriodModel _model = App.ServiceProvider.GetRequiredService<ProgPeriodModel>();
    
    [RelayCommand(CanExecute = nameof(IsInputOk))]
    private void FinalizePeriod()
    {
        _model.CreatePeriodData(Description, StartDate, EndDate);
    }
    
    private bool IsInputOk()
    {
        if (StartDate == string.Empty || EndDate == string.Empty || Description == string.Empty) return false;
        if (!IsStartDateValid() || !IsEndDateValid()) return false;
        if (!IsDescriptionValid()) return false;
        return true;
        // return string.Compare(EndDate, StartDate, StringComparison.Ordinal) == 1;
    }
    
    private bool IsStartDateValid()
    {
        return StartDate == string.Empty || _model.IsDateValid(StartDate, Calendars.Gregorian, YearCounts.CE);
    }
    private bool IsEndDateValid()
    {
        return EndDate == string.Empty || _model.IsSecondDateValid(EndDate, Calendars.Gregorian, YearCounts.CE);
    }

    private bool IsDescriptionValid()
    {
        if (Description == string.Empty) return true;
        return Description.Trim().Length > 0;
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "ProgPeriodInput";    // TODO create helppage
        new HelpWindow().ShowDialog();
    }
    
}