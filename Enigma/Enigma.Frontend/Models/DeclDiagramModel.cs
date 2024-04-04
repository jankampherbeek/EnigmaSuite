// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

public sealed class DeclDiagramModel
{
    private readonly DataVaultCharts _dataVaultCharts;
    private readonly IDeclinationLongitudeForDataGridFactory _declinationLongitudeForDataGridFactory;
    private readonly CalculatedChart _calculatedChart;

    public DeclDiagramModel(IDeclinationLongitudeForDataGridFactory declinationLongitudeForDataGridFactory)
    {
        _dataVaultCharts = DataVaultCharts.Instance;
        _calculatedChart = _dataVaultCharts.GetCurrentChart();
        _declinationLongitudeForDataGridFactory = declinationLongitudeForDataGridFactory;
    }

    public List<PresentableDeclinationLongitude> GetDeclinationLongitudePositions()
    {
        return _declinationLongitudeForDataGridFactory.CreateDeclinationLongitudeForDataGrid(_calculatedChart);
    }
}