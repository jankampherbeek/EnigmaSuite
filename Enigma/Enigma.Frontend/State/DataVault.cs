// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain;
using System.Collections.Generic;

namespace Enigma.Frontend.State;


/// <summary>Central vault for calculated charts and other data.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public sealed class DataVault
{
    private static readonly DataVault instance = new DataVault();

    private List<CalculatedChart> _allCharts = new(); 
    private CalculatedChart? _lastChart;

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static DataVault()
    {
    }

    private DataVault()
    {
    }

    public static DataVault Instance
    {
        get
        {
            return instance;
        }
    }

    public void AddNewChart(CalculatedChart newChart)
    {
        _allCharts.Add(newChart);
        _lastChart = newChart;
    }


    public CalculatedChart? GetLastChart()
    {
        if (_lastChart != null) return _lastChart;
        else
        {
            // todo log error
            return null;
        }

        
    }

}