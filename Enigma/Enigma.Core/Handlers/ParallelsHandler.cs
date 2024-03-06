// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;

namespace Enigma.Core.Handlers;


/// <summary>Handler for aspects.</summary>
public interface IParallelsHandler
{

    /// <summary>Find parallels between chart points.</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Parallels found.</returns>
    public IEnumerable<DefinedParallel> ParallelsForChartPoints(ParallelRequest request);
}



////////// Implementation //////////


/// <inheritdoc/>
public sealed class ParallelsHandler: IParallelsHandler
{

    /// <inheritdoc/>
    public IEnumerable<DefinedParallel> ParallelsForChartPoints(ParallelRequest request)
    {
        // double orb = request.Config.BaseOrbParallels;     TODO use orb from config for parallels
        double maxOrb = 1.0;
        List<Tuple<ChartPoints, double>> chartPointDeclinations = new();
        foreach (var posPoint in request.CalcChart.Positions)
        {
            PointCats cat = posPoint.Key.GetDetails().PointCat;
            if (cat is PointCats.Angle or PointCats.Common or PointCats.Lots)
            {
                chartPointDeclinations.Add(new Tuple<ChartPoints, double>(posPoint.Key, posPoint.Value.Equatorial.DeviationPosSpeed.Position));                
            }
        }
        List<DefinedParallel> parallelsFound = new();
        for (int i = 0; i < chartPointDeclinations.Count; i++)
        {
            ChartPoints point1 = chartPointDeclinations[i].Item1;
            double declPos1 = chartPointDeclinations[i].Item2;
            for (int j = i + 1; j < chartPointDeclinations.Count; j++)
            {
                ChartPoints point2 = chartPointDeclinations[j].Item1;
                double declPos2 = chartPointDeclinations[j].Item2;
                double distance = Math.Abs(Math.Abs(declPos1) - Math.Abs(declPos2));
                if (!(distance <= maxOrb)) continue;
                bool oppParallel = (declPos1 >= 0.0 && declPos2 < 0.0) || (declPos1 < 0.0 && declPos2 >= 0.0);
                parallelsFound.Add(new DefinedParallel(point1, point2, oppParallel, maxOrb, distance));
            }
        }

        return parallelsFound;
    }
    
}







