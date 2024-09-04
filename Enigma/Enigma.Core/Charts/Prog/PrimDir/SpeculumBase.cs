// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Charts.Prog.PrimDir;

/// <summary>General elements for a speculum. These elements are not specific for a chartpoint.</summary>
public class SpeculumBase
{
    private const double HALF_CIRCLE = 180.0;
    public PrimDirMethods Method { get; private set; }
    public PrimDirApproaches Approach { get; private set; }
    public double GeoLat { get; private set; }
    public double JdRadix { get; private set; }
    public double RaMc { get; private set; }
    public double RaIc { get; private set; }
    public double LonMc { get; private set; }
    public double RaAsc { get; private set; }
    public double LonAsc { get; private set; }
    public double OaAsc { get; private set; }
    public double OblEcl { get; private set; }
    
    
    public SpeculumBase(PrimDirRequest request)
    {
        CalculatedChart chart = request.Chart;
        Method = request.Method;
        Approach = request.Approach;
        GeoLat = chart.InputtedChartData.Location.GeoLat;
        JdRadix = chart.InputtedChartData.FullDateTime.JulianDayForEt;
        RaMc = chart.Positions[ChartPoints.Mc].Equatorial.MainPosSpeed.Position;
        RaIc = RaMc <= HALF_CIRCLE ? RaMc + HALF_CIRCLE : RaMc - HALF_CIRCLE;
        LonMc = chart.Positions[ChartPoints.Mc].Ecliptical.MainPosSpeed.Position;
        RaAsc = chart.Positions[ChartPoints.Ascendant].Equatorial.MainPosSpeed.Position;
        LonAsc = chart.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        OaAsc =  RaMc + 90.0;
     //   if (GeoLat < 0.0) OaAsc = RaIc + 90.0;
        if (OaAsc >= 360.0) OaAsc -= 360.0;
        OblEcl = chart.Obliquity;
    }

}
