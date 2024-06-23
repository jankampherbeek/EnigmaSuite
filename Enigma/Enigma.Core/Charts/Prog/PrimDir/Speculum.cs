// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Charts.Prog.PrimDir;

/// <summary>Speculum for primary directions.</summary>
public class Speculum
{
    private Dictionary<ChartPoints, ISpeculumPoint> _speculumPoints = new();
    public SpeculumBase Base { get; set; }

    public Speculum(PrimDirRequest request)
    {
        Base = new SpeculumBase(request);
        
        foreach (KeyValuePair<ChartPoints, FullPointPos> pointFullPos
                 in request.Chart.Positions
                     .Where(pointFullPos => request.Promissors.Contains(pointFullPos.Key)
                                            || request.Significators.Contains(pointFullPos.Key)))
        {
            AddSpeculumPoint(request, pointFullPos.Key, pointFullPos.Value);
        }
    }


    private void AddSpeculumPoint(PrimDirRequest request, ChartPoints point, FullPointPos pointPos)
    {
        switch (request.Method)
        {
            case PrimDirMethods.Regiomontanus:
                _speculumPoints.Add(point, new SpeculumPointReg(point, pointPos, request, Base));
                break;
            case PrimDirMethods.Placidus:
                _speculumPoints.Add(point, new SpeculumPointPlac(point, pointPos, request, Base));
                break;
            case PrimDirMethods.PlacidusPole:
                _speculumPoints.Add(point, new SpeculumPointPlacPole(point, pointPos, request, Base));
                break;
            case PrimDirMethods.Topocentric:
                _speculumPoints.Add(point, new SpeculumPointTopoc(point, pointPos, request, Base));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}