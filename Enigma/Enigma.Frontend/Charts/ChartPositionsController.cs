// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain;
using Enigma.Domain.Charts;
using Enigma.Frontend.PresentationFactories;
using Enigma.Frontend.State;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;


namespace Enigma.Frontend.Charts;

/// <summary>Controller (according to MVC pattern) for the view ChartPositionsWindow.</summary>
public class ChartPositionsController
{

    private CalculatedChart? _currentChart;

    private readonly IHousePosForDataGridFactory _housePosForDataGridFactory;
    private readonly ICelPointForDataGridFactory _celPointForDataGridFactory;
    private readonly DataVault _dataVault;

    public ChartPositionsController(IHousePosForDataGridFactory housePosForDataGridFactory, ICelPointForDataGridFactory celPointForDataGridFactory)
    {
        _dataVault = DataVault.Instance;
        _housePosForDataGridFactory = housePosForDataGridFactory;
        _celPointForDataGridFactory = celPointForDataGridFactory;
    }

    public ChartData GetMeta()
    {
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            return _currentChart.InputtedChartData;    
        }
        else
        {
            return null;
        }
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

    public List<PresentableSolSysPointPositions> GetCelPointPositionsCurrentChart()
    {
        _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            return _celPointForDataGridFactory.CreateCelPointPosForDataGrid(_currentChart.SolSysPointPositions);
        }
        else
        {
            return new List<PresentableSolSysPointPositions>();
        }
    }

    public CalculatedChart GetCalculatedChart()
    {
        return _dataVault.GetLastChart();
    }

}