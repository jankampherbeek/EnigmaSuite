// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Persistables;


public record PersistableDate(int Year, int Month, int Day, string Calendar);

public record PersistableTime(int Hour, int Minute, int Second, double ZoneOffset, double Dst);
