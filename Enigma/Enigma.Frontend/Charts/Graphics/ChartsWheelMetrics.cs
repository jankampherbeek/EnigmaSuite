// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Frontend.Charts.Graphics;

/// <summary>
/// 
/// </summary>
public class ChartsWheelMetrics
{
    private double _sizeFactor;
    private double _baseSize = 700.0;
    private double _baseStrokeSize = 2.0;

    public double SignGlyphSize { get; set; }
    public double GridSize { get; set; }
    public double StrokeSize { get; set; }
    public double OuterCircle { get; set; }
    public double OuterSignCircle { get; set; }
    public double OuterHouseCircle { get; set; }
    public double OuterAspectCircle { get; set; }
    public double SignGlyphCircle { get; set; }


    private readonly double OuterCircleInitial = 0.98;
    private readonly double OuterSignCircleInitial = 0.89;
    private readonly double OuterHouseCircleInitial = 0.79;
    private readonly double OuterAspectCircleInitial = 0.44;
    private readonly double SignGlyphCircleInitial = 0.84;

    private readonly double SignGlyphSizeInitial = 18.0;

    public ChartsWheelMetrics()
    {
        GridSize = 700.0;
        _sizeFactor = 1.0;
        DefineSizes();
    }

    public void SetSizeFactor(double newFactor)
    {
        _sizeFactor = newFactor;
        DefineSizes();
    }

    private void DefineSizes()
    {

        GridSize = _baseSize * _sizeFactor;
        StrokeSize = _baseStrokeSize * _sizeFactor;
        OuterCircle = OuterCircleInitial * GridSize;
        OuterSignCircle = OuterSignCircleInitial * GridSize;
        OuterHouseCircle = OuterHouseCircleInitial * GridSize;
        OuterAspectCircle = OuterAspectCircleInitial * GridSize;
        SignGlyphCircle = SignGlyphCircleInitial * GridSize;
        SignGlyphSize = SignGlyphSizeInitial * GridSize;
    }

}