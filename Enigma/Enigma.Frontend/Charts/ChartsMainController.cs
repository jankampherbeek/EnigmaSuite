// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.Ui.Charts.Graphics;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Enigma.Frontend.Ui.Charts;

public class ChartsMainController
{
    private ChartsWheel? _chartsWheel;

    private List<Window> _openWindows = new();

    public void ShowCurrentChart()
    {
        _chartsWheel = App.ServiceProvider.GetRequiredService<ChartsWheel>();
        _openWindows.Add(_chartsWheel);
        _chartsWheel.Show();
        _chartsWheel.DrawChart();

    }

    public void HandleClose()
    {
        foreach (Window window in _openWindows)
        {
            window.Close();
        }
    }

}