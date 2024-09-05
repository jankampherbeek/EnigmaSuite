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
    public Dictionary<ChartPoints, ISpeculumPoint> SpeculumPoints = new();
    public Dictionary<ChartPoints, ISpeculumPoint> SpeculumOppPoints = new();
    public SpeculumBase Base { get; set; }

    public Speculum(PrimDirRequest request)
    {
        Base = new SpeculumBase(request);

        foreach (KeyValuePair<ChartPoints, FullPointPos> pointFullPos
                 in request.Chart.Positions
                     .Where(pointFullPos => request.Promissors.Contains(pointFullPos.Key)
                                            || request.Significators.Contains(pointFullPos.Key)))
        {
            AddSpeculumPoints(request, pointFullPos.Key, pointFullPos.Value);
        }
    }


    private void AddSpeculumPoints(PrimDirRequest request, ChartPoints point, FullPointPos pointPos)
    {
        switch (request.Method)
        {
            case PrimDirMethods.Regiomontanus:
                SpeculumPoints.Add(point, new SpeculumPointReg(point, pointPos, request, Base, AspectTypes.Conjunction));
                SpeculumOppPoints.Add(point, new SpeculumPointReg(point, pointPos, request, Base, AspectTypes.Opposition));
                break;
            case PrimDirMethods.Placidus:
                SpeculumPoints.Add(point, new SpeculumPointPlac(point, pointPos, request, Base, AspectTypes.Conjunction));
                SpeculumOppPoints.Add(point, new SpeculumPointPlac(point, pointPos, request, Base, AspectTypes.Opposition));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

   
    
}