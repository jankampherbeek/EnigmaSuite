﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Parent for dates that are used in progressive techniques.</summary>
/// <param name="Id">Unique Id</param>
/// <param name="Description">Description for the date(s).</param>
public abstract record ProgDates(int Id, string Description)
{ 
    public int Id { get; set; } = Id;
}



/// <summary>Event for progressive analysis. </summary>
/// <param name="Id">Unique id.</param>
/// <param name="Description">Description for the event.</param>
/// <param name="LocationName">Name for the location.</param>
/// <param name="Location">Datails for the location.</param>
/// <param name="DateTime">Date and time for the event.</param>
public record ProgEvent(int Id, string Description, string LocationName, Location Location,
    FullDateTime DateTime) : ProgDates(Id, Description);

/// <summary>Period for progressive analysis. </summary>
/// <param name="Id">Unique id</param>
/// <param name="Description">Description for the period.</param>
/// <param name="StartDate">First date of the period.</param>
/// <param name="EndDate">Last date of the period.</param>
public record ProgPeriod(int Id, string Description, FullDateTime StartDate, FullDateTime EndDate) : ProgDates(Id,
    Description);