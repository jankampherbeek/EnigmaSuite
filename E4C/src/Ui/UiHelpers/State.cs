// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Ui.Domain;
using System.Collections.Generic;

namespace E4C.Ui.UiHelpers
{

    public interface IChartsStock
    {
        public void AddChart(ChartData chartData);
        public void RemoveChart(int tempId);
        public void SetCurrentChart(int tempId);
        public int GetNewTempId();
        public int GetCurrentChartTempId();
        public ChartData? GetCurrentChart();
        public List<int> GetAllChartTempIds();
        public MetaData? GetMetaData(int tempId);
    }

    public class ChartsStock : IChartsStock
    {
        private int _latestTempId = 0;
        private int _currentTempId = 0;
        readonly private List<ChartData> _charts = new();


        public void AddChart(ChartData chartData)
        {
            _currentTempId = chartData.TempId;
            if (_currentTempId > _latestTempId)
            {
                _latestTempId = _currentTempId;
            }
            _charts.Add(chartData);
        }

        public List<int> GetAllChartTempIds()
        {
            List<int> allTempIds = new();
            foreach (ChartData chartData in _charts)
            {
                allTempIds.Add(chartData.TempId);
            }
            return allTempIds;
        }

        public ChartData? GetCurrentChart()
        {
            foreach (ChartData chartData in _charts)
            {
                if (chartData.TempId == _currentTempId)
                {
                    return chartData;
                }
            }
            return null;
        }

        public int GetCurrentChartTempId()
        {
            return _currentTempId;
        }

        public MetaData? GetMetaData(int tempId)
        {
            foreach (ChartData chartData in _charts)
            {
                if (chartData.TempId == tempId)
                {
                    return chartData.ChartMetaData;
                }
            }
            return null;
        }

        public int GetNewTempId()
        {
            return ++_latestTempId;
        }

        public void RemoveChart(int tempId)
        {
            if (tempId == _currentTempId)
            {
                _currentTempId = 0;
            }
            ChartData? removableChartData = null;
            foreach (ChartData chartData in _charts)
            {
                if (chartData.TempId == tempId)
                {
                    removableChartData = chartData;
                }
            }
            if (removableChartData != null)
            {
                _charts.Remove(removableChartData);
            }
        }

        public void SetCurrentChart(int tempId)
        {
            foreach (ChartData chartData in _charts)
            {
                if (chartData.TempId == tempId)
                {
                    _currentTempId = tempId;
                }
            }
        }
    }

}