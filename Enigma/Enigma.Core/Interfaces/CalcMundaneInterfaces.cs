﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;

namespace Enigma.Core.Interfaces;


/// <summary>Starts the calculations for mundane positions and cusps.</summary>
public interface IHousesHandler
{
    /// <summary>Calculates all mundane positions.</summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Dictionary<ChartPoints, FullPointPos> CalcHouses(FullHousesPosRequest request);

    /// <summary>Calculate the right ascension of the MC.</summary>
    /// <param name="jdUt">Julian day.</param>
    /// <param name="obliquity">Obliquity.</param>
    /// <param name="location">Actual location.</param>
    /// <returns>The ramc in decimal degrees.</returns>
    public double CalcArmc(double jdUt, double obliquity, Location location);
}


/// <summary>Calculations for houses and other mundane positions.</summary>
public interface IHousesCalc
{
    /// <summary>Calculate longitude for houses and other mundane positions.</summary>
    /// <param name="julianDayUt">Julian Day for UT.</param>
    /// <param name="obliquity"/>
    /// <param name="location"/>
    /// <param name="houseSystemId">Id for a housesystem as used by the SE.</param>
    /// <param name="flags"/>
    /// <returns>The calculated positions for the houses and other mundane points.</returns>
    public double[][] CalculateHouses(double julianDayUt, double obliquity, Location location, char houseSystemId, int flags);
}