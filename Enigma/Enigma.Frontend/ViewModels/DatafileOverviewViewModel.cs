// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for an overview of data files</summary>
public partial class DatafileOverviewViewModel: ObservableObject
{
    [ObservableProperty] private ObservableCollection<string> _dataNames;    

    public DatafileOverviewViewModel()
    {
        DatafileOverviewModel model = App.ServiceProvider.GetRequiredService<DatafileOverviewModel>();
        _dataNames = new ObservableCollection<string>(model.GetDataNames());
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "DatafileOverview";
        new HelpWindow().ShowDialog();
    }

}
