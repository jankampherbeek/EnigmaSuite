// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.PreNatal;

public static class PreNatalOrchestrator
{


    public static List<PreNatalParent> ConstructPreNatalMoments(double jdStart, double jdEnd)
    {
        var preNatalMoments = new List<PreNatalParent>();
        var eclipses = EclipseMoments.FindEclipses(jdStart, jdEnd);
        preNatalMoments.AddRange(eclipses);
        
        
        
        return preNatalMoments;
    }



}