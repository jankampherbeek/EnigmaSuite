// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.InputSupport.PresentationFactories;
using Enigma.Frontend.State;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;


namespace Enigma.Frontend.Charts;

/// <summary>Controller (according to MVC pattern) for the view ChartPositionsWindow.</summary>
public class ChartPositionsController
{

    private CalculatedChart? _currentChart;

    private IHousePosForDataGridFactory _housePosForDataGridFactory;
    private DataVault _dataVault;

    public ChartPositionsController(IHousePosForDataGridFactory housePosForDataGridFactory)
    {
        _dataVault = DataVault.Instance;
        _housePosForDataGridFactory = housePosForDataGridFactory;
    }


    public List<PresentableHousePositions> GetHousePositionsCurrentChart()
    {
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            return _housePosForDataGridFactory.CreateHousePosForDataGrid(_currentChart.FullHousePositions);
        }
        else
        {
            return new List<PresentableHousePositions>();
        }
    }


}