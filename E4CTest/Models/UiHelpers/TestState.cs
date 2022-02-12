// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.Models.UiHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace E4CTest
{
    [TestClass]
    public class TestChartsStock
    {

        [TestMethod]
        public void TestAddChart()
        {
            ChartData chart1 = CreateChartData(100, 3);
            ChartData chart2 = CreateChartData(110, 5);
            IChartsStock chartsStock = new ChartsStock();
            chartsStock.AddChart(chart1);
            chartsStock.AddChart(chart2);
            Assert.AreEqual(5, chartsStock.GetCurrentChartTempId());
        }

        [TestMethod]
        public void TestRemoveChart()
        {
            ChartData chart1 = CreateChartData(100, 3);
            ChartData chart2 = CreateChartData(110, 5);
            IChartsStock chartsStock = new ChartsStock();
            chartsStock.AddChart(chart1);
            chartsStock.AddChart(chart2);
            Assert.AreEqual(5, chartsStock.GetCurrentChartTempId());
            chartsStock.RemoveChart(5);
            Assert.AreEqual(0, chartsStock.GetCurrentChartTempId());
            List<int> allChartTempdIds = chartsStock.GetAllChartTempIds();
            Assert.AreEqual(1, allChartTempdIds.Count);
        }

        [TestMethod]
        public void TestGetCurrentChartLatest()
        {
            ChartData chart1 = CreateChartData(100, 3);
            ChartData chart2 = CreateChartData(110, 5);
            IChartsStock chartsStock = new ChartsStock();
            chartsStock.AddChart(chart1);
            chartsStock.AddChart(chart2);
            Assert.AreEqual(chart2, chartsStock.GetCurrentChart());
        }

        [TestMethod]
        public void TestSetGetCurrentChart()
        {
            ChartData chart1 = CreateChartData(100, 3);
            ChartData chart2 = CreateChartData(110, 5);
            IChartsStock chartsStock = new ChartsStock();
            chartsStock.AddChart(chart1);
            chartsStock.AddChart(chart2);
            chartsStock.SetCurrentChart(3);
            Assert.AreEqual(chart1, chartsStock.GetCurrentChart());
        }

        [TestMethod]
        public void TestSetCurrentChartDoesNotExist()
        {
            ChartData chart1 = CreateChartData(100, 3);
            IChartsStock chartsStock = new ChartsStock();
            chartsStock.AddChart(chart1);
            chartsStock.SetCurrentChart(100);
            Assert.AreEqual(chart1, chartsStock.GetCurrentChart());
        }

        [TestMethod]
        public void TestGetCurrentChartNotAvailable()
        {
            IChartsStock chartsStock = new ChartsStock();
            Assert.IsNull(chartsStock.GetCurrentChart());
        }

        [TestMethod]
        public void TestGetCurrentChartTempIdWhenEmpty()
        {
            IChartsStock chartsStock = new ChartsStock();
            Assert.AreEqual(0, chartsStock.GetCurrentChartTempId());
        }

        [TestMethod]
        public void TestGetMetData()
        {
            ChartData chart1 = CreateChartData(100, 3);
            IChartsStock chartsStock = new ChartsStock();
            chartsStock.AddChart(chart1);
            MetaData metaData = chartsStock.GetMetaData(3);
            Assert.AreEqual(metaData, chart1.ChartMetaData);
        }

        [TestMethod]
        public void TestGetMetDataWrongTempId()
        {
            ChartData chart1 = CreateChartData(100, 3);
            IChartsStock chartsStock = new ChartsStock();
            chartsStock.AddChart(chart1);
            MetaData metaData = chartsStock.GetMetaData(6);
            Assert.IsNull(metaData);
        }

        [TestMethod]
        public void TestGetAllChartTempIds()
        {
            ChartData chart1 = CreateChartData(100, 3);
            ChartData chart2 = CreateChartData(110, 5);
            IChartsStock chartsStock = new ChartsStock();
            chartsStock.AddChart(chart1);
            chartsStock.AddChart(chart2);
            List<int> allTempIds = chartsStock.GetAllChartTempIds();
            Assert.AreEqual(2, allTempIds.Count);
            Assert.AreEqual(3, allTempIds[0]);
            Assert.AreEqual(5, allTempIds[1]);
        }

        [TestMethod]
        public void TestGetNewTempId()
        {
            IChartsStock _chartsStock = new ChartsStock();
            int _newId = _chartsStock.GetNewTempId();
            Assert.AreEqual(1, _newId);
            _newId = _chartsStock.GetNewTempId();
            Assert.AreEqual(2, _newId);
        }

        private ChartData CreateChartData(int index, int tempId)
        {
            MetaData _metaData = new("name", "description", "source", ChartCategories.Female, RoddenRatings.A);
            Location _location = new("location full name", 100.0, 20.0);
            SimpleDateTime _dateTime = new(2022, 2, 12, 0.0, Calendars.Gregorian);
            FullDateTime _fullDateTime = new("date text", "time text", 123456.789, _dateTime);
            return new ChartData(index, tempId, _metaData, _location, _fullDateTime);


        }

    }
}