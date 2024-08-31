// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support.Parsers;

namespace Enigma.Frontend.Ui.Charts.Prog.PrimDir;

public class PrimDirInputModel
{
    private readonly IDateInputParser _dateInputParser;

    public PrimDirInputModel(IDateInputParser dateInputParser)
    {
        _dateInputParser = dateInputParser;
    }
    
    public bool IsDateValid(string date, Calendars cal, YearCounts yc)
    {
        return _dateInputParser.HandleDate(date, cal, yc, out _);
    }
    
    
}