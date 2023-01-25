// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using System.Collections.Generic;


namespace Enigma.Frontend.Ui.Charts;

/// <summary>Controller (according to MVC pattern) for the view ChartPositionsWindow.</summary>
public class ChartPositionsController
{

    private readonly ChartAspectsWindow _chartAspectsWindow;
    private readonly IHousePosForDataGridFactory _housePosForDataGridFactory;
    private readonly ICelPointForDataGridFactory _celPointForDataGridFactory;
    private readonly DataVault _dataVault;

    public ChartPositionsController(IHousePosForDataGridFactory housePosForDataGridFactory,
        ICelPointForDataGridFactory celPointForDataGridFactory,
        ChartAspectsWindow chartAspectsWindow)
    {
        _dataVault = DataVault.Instance;
        _housePosForDataGridFactory = housePosForDataGridFactory;
        _celPointForDataGridFactory = celPointForDataGridFactory;
        _chartAspectsWindow = chartAspectsWindow;
    }

    public ChartData? GetMeta()
    {
        CalculatedChart? _currentChart = _dataVault.GetLastChart();
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
        CalculatedChart? _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            return _housePosForDataGridFactory.CreateHousePosForDataGrid(new FullHousesPositions(_currentChart.Positions.Angles, _currentChart.Positions.Cusps));
        }
        else
        {
            return new List<PresentableHousePositions>();
        }
    }

    public List<PresentableCommonPositions> GetCelPointPositionsCurrentChart()
    {
        CalculatedChart? _currentChart = _dataVault.GetLastChart();
        if (_currentChart != null)
        {
            return _celPointForDataGridFactory.CreateCelPointPosForDataGrid(_currentChart.Positions.CommonPoints);
        }
        else
        {
            return new List<PresentableCommonPositions>();
        }
    }

    public CalculatedChart? GetCalculatedChart()
    {
        return _dataVault.GetLastChart();
    }

    public void ShowAspects()
    {
        _chartAspectsWindow.Show();
        _chartAspectsWindow.Populate();


    }

}