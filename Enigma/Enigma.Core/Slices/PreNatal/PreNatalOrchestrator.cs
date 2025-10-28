// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.PreNatal;

public static class PreNatalOrchestrator
{

    public static List<PreNatalParent> ConstructPreNatalMoments(List<ChartPoints> factors, 
        List<AspectTypes> aspects,
        double jdStart, double jdEnd)
    {
        var preNatalMoments = new List<PreNatalParent>();
        var eclipses = EclipseMoments.FindEclipses(jdStart, jdEnd);
        var ingresses = IngressMoments.FindIngressMoments(factors, jdStart, jdEnd);
        var retroDirects = RetroDirectMoments.FindRetroDirectMoments(factors, jdStart, jdEnd);
        var mutualAspects = MutualAspectMoments.FindMutualAspectMoments(factors, aspects, jdStart, jdEnd);
        
        preNatalMoments.AddRange(eclipses);
        preNatalMoments.AddRange(ingresses);
        preNatalMoments.AddRange(retroDirects);
        preNatalMoments.AddRange(mutualAspects);
        
        preNatalMoments = preNatalMoments.OrderBy(x => x.PreNatalJd).ToList();

        return preNatalMoments;
    }

    public static double JdPreNatalToActual(double jdConception, double preNatalJd, double jdRadix)
    {
        var convertedDateTime = PreNatalToActualTimeConversion.ConvertToActualTime(preNatalJd, jdConception, jdRadix);
        return convertedDateTime;
    }

}