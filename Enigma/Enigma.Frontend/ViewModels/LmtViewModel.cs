// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Models;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class LmtViewModel : ObservableObject
{
    [ObservableProperty] private string _description = "Enter offset as ddd:mm:ss";
    [ObservableProperty] private string _inputtedText = "";


    [RelayCommand] private void Click()
    {
        MessageBox.Show("I have been clicked....");
        Console.WriteLine("Clicked");
    }
}