// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Util;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.Progressive;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Calc.Progressive;


public sealed class Speculum: ISpeculum
{
    public PrimarySystems PrimarySystem => _primSys;
    public double GeoLat => _gl;
    public double RaMc => _ramc;
    public double RaIc => _raic;
    public double OblAscAscendant => _oaasc;
    public double OblDescDescendant => _oddesc;

    public List<ISpeculumItem> SpeculumItems => _specItems;

    private readonly PrimarySystems _primSys;
    private readonly double _gl;                // geographic latitude
    private readonly double _ramc;              // right ascension mc
    private readonly double _raic;              // right ascension ic
    private readonly double _oaasc;                      // oblique ascension ascendant
    private readonly double _oddesc;                     // oblique ascension descendant
    private readonly List<ISpeculumItem> _specItems;

    public Speculum(PrimarySystems primSys, CalculatedChart calcChart, List<ChartPoints> promissors, List<ChartPoints> significators)
    {
        _primSys = primSys;
        _gl = calcChart.InputtedChartData.Location.GeoLat;

        foreach (var position in calcChart.Positions)
        {
            if (position.Key == ChartPoints.Mc)
            {
                _ramc = position.Value.Equatorial.MainPosSpeed.Position;
                _raic = RangeUtil.ValueToRange(_ramc + 180.0, 0.0, 360.0);
                _oaasc = RangeUtil.ValueToRange(_ramc + 90.0, 0.0, 360.0);
                _oddesc = RangeUtil.ValueToRange(_ramc - 90.0, 0.0, 360.0);
            }

            // handle promissors/significators
        }

    }
}



/// <summary>Speculum for Placidean directions, using the semi-arc.</summary>
public abstract class SpeculumItem : ISpeculumItem
{
    /// <inheritdoc/>
    public double RightAscension => _raPlanet;
    /// <inheritdoc/>
    public double Declination => _declPlanet;
    /// <inheritdoc/>
    public double MeridianDistanceMc => _mdmc;
    /// <inheritdoc/>
    public double MeridianDistanceIc => _mdic;
    /// <inheritdoc/>
    public double AscensionalDifference => _ad;
    /// <summary>Oblique ascension of promissor.</summary>
    public double ObliqueAscensionPromissor => _oadpl;
    /// <summary>Horizontal distance</summary>
    public double HorizontalDistance => _hd;
    /// <summary>Diurnal semi-arc</summary>
    public double DiurnalSemiArc => _dsa;
    /// <summary>Diurnal semi-arc</summary>
    public double NocturnalSemiArc => _nsa;


    protected double _raPlanet;
    protected double _declPlanet;
    protected double _mdmc;                       // meridian distance from mc
    protected double _mdic;                       // meridian distance from ic
    protected double _ad;                         // ascensional difference
    protected double _oadpl;                      // oblique ascension or descension planet
    protected double _geoLat;
    protected double _hd;                         // horizontal distance
    protected double _dsa;                        // diurnal semi-arc
    protected double _nsa;                        // nocturnal semi-arc
    protected bool _east;
    protected bool _north;

    public SpeculumItem(double geoLat, double raMc, double raIc, double oaAsc, double odDesc, double raPlanet, double declPlanet)
    {
        _raPlanet = raPlanet;
        _declPlanet = declPlanet;
        _geoLat = geoLat;
        _mdmc = RangeUtil.ValueToRange(_raPlanet - raMc, 0.0, 360.0);
        _mdic = RangeUtil.ValueToRange(_raPlanet - raIc, 0.0, 360.0);
        _ad = MathExtra.AscensionalDifference(_declPlanet, geoLat);
        _north = _geoLat >= 0.0;
        _east = MathExtra.IsEasternHemiSphere(_raPlanet, raMc);
        _oadpl = MathExtra.ObliqueAscension(_raPlanet, _ad, _east, _north);
        if (_east)
        {
            _hd = _oadpl - oaAsc;
        } else
        {
            _hd = RangeUtil.ValueToRange(_oadpl + 180.0, 0.0, 360.0) - odDesc;
        }
        _dsa = RangeUtil.ValueToRange(Math.Abs(_hd) + Math.Abs(_mdmc), 0.0, 360.0);
        _nsa = RangeUtil.ValueToRange(Math.Abs(_hd) + Math.Abs(_mdic), 0.0, 360.0);

    }
}

public class SpeculumItemPlacidus : SpeculumItem
{

    /// <summary>Proportional semi arc</summary>
    public double ProportionalSemiArc => _psa;

    private double _psa;

    public SpeculumItemPlacidus(double geoLat, double raMc, double raIc, double oaAsc, double odDesc, double raPlanet, double declPlanet) :
        base(geoLat, raMc, raIc, oaAsc, odDesc, raPlanet, declPlanet)
    {
        _psa = (_hd >= 0.0) ? _mdmc / _dsa : _mdic / _nsa;
    }

}

public class SpeculumItemRegiomontanus: SpeculumItem
{
    public double Pole => _pole;
    public double AdPole => _adPole;
    public double OadPole => _oadPole;

    private double _pole;
    private double _adPole;
    private double _oadPole;

    public SpeculumItemRegiomontanus(double geoLat, double raMc, double raIc, double oaAsc, double odDesc, double raPlanet, double declPlanet) :
        base(geoLat, raMc, raIc, oaAsc, odDesc, raPlanet, declPlanet)
    {
        Calculate();
    }

    private void Calculate()
    {
        double geoLatRad = MathExtra.DegToRad(_geoLat);
        double declPlRad = MathExtra.DegToRad(_declPlanet);
        double mdMcRad = MathExtra.DegToRad(_mdmc);
        double xRad = Math.Atan(Math.Tan(declPlRad) / Math.Cos(mdMcRad));
        double yRad = geoLatRad - xRad;
        double zRad = Math.Atan(Math.Cos(yRad) / (Math.Tan(mdMcRad) * Math.Cos(xRad)));
        double poleRad = Math.Asin(Math.Sin(geoLatRad) * Math.Cos(zRad));
        _pole = MathExtra.RadToDeg(poleRad);
        double adPoleRad = Math.Asin(Math.Tan(declPlRad) * Math.Tan(poleRad));
        _adPole = MathExtra.RadToDeg(adPoleRad);
        _oadPole = _east ? _raPlanet - _adPole : _raPlanet + _adPole;

        // moving point needs additional calculation
    }

}


