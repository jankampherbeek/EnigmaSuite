// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Enigma.Frontend.Ui.Graphics;

namespace Enigma.Frontend.Ui.Interfaces;


// Interfaces for frontend graphics.
public interface IChartsWheelAspects
{
    public List<Line> CreateAspectLines(CalculatedChart currentChart, ChartsWheelMetrics metrics, Point centerPoint);
}

public interface IChartsWheelCircles
{
    public List<Ellipse> CreateCircles(ChartsWheelMetrics metrics);
    public List<Line> CreateDegreeLines(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant);
}

public interface IChartsWheelCusps
{
    public List<Line> CreateCuspLines(ChartsWheelMetrics metrics, Point centerPoint, List<double> housePositions, double longAscendant);
    public List<Line> CreateCardinalLines(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant, double longMc);
    public List<TextBlock> CreateCuspTexts(ChartsWheelMetrics metrics, Point centerPoint, List<double> housePositions, double longAscendant);
    public List<TextBlock> CreateCardinalIndicators(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant, double longMc);
}

public interface IChartsWheelSigns
{
    public List<Line> CreateSignSeparators(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant);
    public List<TextBlock> CreateSignGlyphs(ChartsWheelMetrics metrics, Point centerPoint, double longAscendant);
}


public interface IChartsWheelCelPoints
{
    public List<TextBlock> CreateCelPointGlyphs(ChartsWheelMetrics metrics, Dictionary<ChartPoints, FullPointPos> commonPoints, Point centerPoint, double longAscendant);
    public List<Line> CreateCelPointConnectLines(ChartsWheelMetrics metrics, Dictionary<ChartPoints, FullPointPos> celPoints, Point centerPoint, double longAscendant);
    public List<TextBlock> CreateCelPointTexts(ChartsWheelMetrics metrics, Dictionary<ChartPoints, FullPointPos> celPoints, Point centerPoint, double longAscendant);
}




