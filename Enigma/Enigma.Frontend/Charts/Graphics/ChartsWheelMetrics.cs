// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Frontend.Charts.Graphics;

/// <summary>
/// 
/// </summary>
public class ChartsWheelMetrics
{
    private double _baseSize = 700.0;
    private double _baseStrokeSize = 2.0;
    private double _baseConnectLineSize = 1.0;

    public double SizeFactor { get; private set; }


    public double SignGlyphSize { get; private set; }
    public double SolSysPointGlyphSize { get; private set; }
    public double PositionTextSize { get; private set; }
    public double GlyphXOffset { get; private set; }
    public double GlyphYOffset { get; private set; }
    public double GridSize { get; private set; }
    public double StrokeSize { get; private set; }
    public double ConnectLineSize { get; private set; }
    public double OuterCircle { get; private set; }
    public double OuterSignCircle { get; private set; }
    public double OuterHouseCircle { get; private set; }
    public double CuspTextCircle { get; private set; }
    public double OuterAspectCircle { get; private set; }
    public double OuterConnectionCircle { get; private set; }
    public double SignGlyphCircle { get; private set; }
    public double DegreesCircle { get; private set; }
    public double Degrees5Circle { get; private set ; }
    public double SolSysPointGlyphCircle { get; private set; }
    public double SolSysPointTextCircle { get; private set; }   

    private readonly double OuterCircleInitial = 0.98;
    private readonly double OuterSignCircleInitial = 0.89;
    private readonly double OuterHouseCircleInitial = 0.79;
    private readonly double CuspTextCircleInitial = 0.76;
    private readonly double OuterAspectCircleInitial = 0.44;
    private readonly double SignGlyphCircleInitial = 0.84;
    private readonly double DegreesCircleInitial = 0.775;
    private readonly double Degrees5CircleInitial = 0.76;
    private readonly double SolSysPointGlyphCircleInitial = 0.54;
    private readonly double OuterConnectionCircleInitial = 0.48;
    private readonly double SolSysPointTextCircleInitial = 0.64;

    private readonly double SignGlyphSizeInitial = 28.0;
    private readonly double SolSysPointGlyphSizeInitial = 24.0;
    private readonly double PositionTextSizeInitial = 10.0;
    private readonly double GlyphXOffsetInitial = 0.0;
    private readonly double GlyphYOffsetInitial = 0.0;

    public ChartsWheelMetrics()
    {
        GridSize = 700.0;
        SizeFactor = 1.0;
        DefineSizes();
    }

    public void SetSizeFactor(double newFactor)
    {
        SizeFactor = newFactor;
        DefineSizes();
    }

    private void DefineSizes()
    {

        GridSize = _baseSize * SizeFactor;
        StrokeSize = _baseStrokeSize * SizeFactor;
        ConnectLineSize = _baseConnectLineSize * SizeFactor;
        OuterCircle = OuterCircleInitial * GridSize;
        OuterSignCircle = OuterSignCircleInitial * GridSize;
        OuterHouseCircle = OuterHouseCircleInitial * GridSize;
        CuspTextCircle = CuspTextCircleInitial * GridSize;
        OuterAspectCircle = OuterAspectCircleInitial * GridSize;
        SignGlyphCircle = SignGlyphCircleInitial * GridSize;
        DegreesCircle = DegreesCircleInitial * GridSize;
        Degrees5Circle = Degrees5CircleInitial * GridSize;
        SolSysPointGlyphCircle = SolSysPointGlyphCircleInitial * GridSize;
        OuterConnectionCircle = OuterConnectionCircleInitial * GridSize;
        SolSysPointTextCircle = SolSysPointTextCircleInitial * GridSize;
        GlyphXOffset = GlyphXOffsetInitial * GridSize;
        GlyphYOffset = GlyphYOffsetInitial * GridSize;

        SignGlyphSize = SignGlyphSizeInitial * (GridSize / 700.0);
        SolSysPointGlyphSize = SolSysPointGlyphSizeInitial * (GridSize / 700.0);
        PositionTextSize = PositionTextSizeInitial * (GridSize  / 700.0);

    }

}