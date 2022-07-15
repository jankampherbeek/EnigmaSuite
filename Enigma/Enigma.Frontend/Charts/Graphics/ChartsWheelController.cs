// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.State;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.Charts.Graphics;

public class ChartsWheelController
{
    private readonly DataVault _dataVault;
    private CalculatedChart? _currentChart;

    public ChartsWheelController()
    {
        _dataVault = DataVault.Instance;
    }

    public double GetAscendantLongitude()
    {
        double ascLong = -double.MaxValue;
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            ascLong = _currentChart.FullHousePositions.Ascendant.Longitude;
        }
        return ascLong;
    }

    public List<double> GetHouseLongitudesCurrentChart()
    {
        List<double> longitudes = new List<double>();
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            foreach (var cusp in _currentChart.FullHousePositions.Cusps)
            {
                longitudes.Add(cusp.Longitude);
            }

        }
        return longitudes;
    }


}