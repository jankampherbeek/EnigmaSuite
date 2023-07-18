// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Enigma.Frontend.Ui.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for help files</summary>
public partial class HelpViewModel: ObservableObject
{
    [ObservableProperty] private Uri? _html;

    public HelpViewModel()
    {
        HelpModel model = App.ServiceProvider.GetRequiredService<HelpModel>();
        Html = model.HtmlUri;
    }
    
    
}