// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;


namespace Enigma.Frontend.Ui.ViewModels;

public partial class StartViewModel: ObservableObject
{
    private readonly StartModel _model = App.ServiceProvider.GetRequiredService<StartModel>();
    
    private void ShowSplashAndFinishView()
    {
        //Show();
        _model.HandleCheckForConfig();
        _model.HandleCheckDirForSettings();
        _model.HandleCheckNewVersion();
        // Hide();
        MainWindow mainWindow = new();              // Todo 0.2 add check for exceptions and show warning to user if an exception occurs.
        mainWindow.ShowDialog();
        Application.Current.Shutdown(0);
    }
    
    
    
}