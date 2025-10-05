// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Core.Slices.BlaSchema;

namespace Enigma.Frontend.Ui.PresentationFactories;

public record PresentableBlaCycle(string ElemCross, string description);


public class BlaCyclesPresFactory
{

    public List<PresentableBlaCycle> CreateBlaCycles(BlaCyclesData cyclesData)
    {
        var allCycles = new List<PresentableBlaCycle>();
        allCycles.Add(ConstructCycle("Cardinal", cyclesData.Cardinal));
        allCycles.Add(ConstructCycle("Fixed", cyclesData.Fixed));
        allCycles.Add(ConstructCycle("Mutable", cyclesData.Mutable));
        allCycles.Add(ConstructCycle("Fire", cyclesData.Fire));
        allCycles.Add(ConstructCycle("Earth", cyclesData.Earth));
        allCycles.Add(ConstructCycle("Air", cyclesData.Air));
        allCycles.Add(ConstructCycle("Water", cyclesData.Water));
        return allCycles;
    }

    public List<PresentableBlaCycle> CreateShortenedCycles(BlaCyclesData cyclesData)
    {
        var allCycles = new List<PresentableBlaCycle>();
        allCycles.Add(ConstructShortenedCycle("Cardinal", cyclesData.Cardinal));
        allCycles.Add(ConstructShortenedCycle("Fixed", cyclesData.Fixed));
        allCycles.Add(ConstructShortenedCycle("Mutable", cyclesData.Mutable));
        allCycles.Add(ConstructShortenedCycle("Fire", cyclesData.Fire));
        allCycles.Add(ConstructShortenedCycle("Earth", cyclesData.Earth));
        allCycles.Add(ConstructShortenedCycle("Air", cyclesData.Air));
        allCycles.Add(ConstructShortenedCycle("Water", cyclesData.Water));
        return allCycles;
    }

    private static PresentableBlaCycle ConstructCycle(string cat, List<(int, int)> cycles)
    {
        var descr = "";
        var sep = "";
        foreach (var cycle in cycles)
        {
            descr += sep + cycle.Item1 + ">>" + cycle.Item2;
            sep = ", ";
        }
        return new PresentableBlaCycle(cat, descr);
    }

    private static PresentableBlaCycle ConstructShortenedCycle(string cat, List<(int, int)> cycles)
    {
        var descr = "";
        var sep = "";
        var lord = "L.";
        foreach (var cycle in cycles)
        {
            descr += sep + lord + cycle.Item1 + " = " + lord + cycle.Item2;
            sep = ", ";
        }
        return new PresentableBlaCycle(cat, descr);
    }
    
    
}