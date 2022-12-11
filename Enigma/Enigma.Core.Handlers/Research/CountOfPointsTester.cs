// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.Persistency;
using Enigma.Domain.Research;
using Enigma.Research.Domain;

namespace Enigma.Core.Handlers.Research;

public sealed class CountOfPointsTester
{

    private IInputDataConverter _inputDataConverter;


    public CountOfPointsTester(IInputDataConverter inputDataConverter)
    {
        _inputDataConverter = inputDataConverter;
    }

    public MethodResponse TestPointsInSigns(string projectName, bool useControlGroup, List<ResearchPoint> points)
    {
        // definieer configuratie


        StandardInput standardInput = ReadData(useControlGroup, projectName);
        int[,] resultingCounts = CalculateResults(standardInput.ChartData, points);

        // Construeer MethodResponse



        return null;

    }


    private StandardInput ReadData(bool useControlGroup, string projectName)
    {
        // define filename for json
        string jsonString = "";   // read jsonstring from file.
        
        StandardInput standardInput = _inputDataConverter.UnMarshallStandardInput(jsonString);

        return null;

    }

    private int[,] CalculateResults(List<StandardInputItem> standardInputs, List<ResearchPoint> points)
    {
        int nrOfPoints = points.Count;
        int nrOfSigns = 12;
        int[,] resultingCounts = new int[nrOfPoints, nrOfSigns];



        // converteer standard inputs van Json naar object.
        // voor elke waarde  in standardInputs
        //      bereken positie
        //      bereken nummer voor teken
        //      voeg toe aan totalen



        return resultingCounts;
    }




}
