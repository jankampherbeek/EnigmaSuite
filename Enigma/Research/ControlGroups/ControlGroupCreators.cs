// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Domain.Research;
using Enigma.Domain.Messages;


namespace Enigma.Research.ControlGroups;

public interface IControlGroupCreator
{
    public ResultMessage DefineControlGroup(string dataName, ControlGroupTypes controlGroupType);

}

public class StandardShiftControlGroupCreator : IControlGroupCreator
{
    public ResultMessage DefineControlGroup(string dataName, ControlGroupTypes controlGroupType)
    {
        // read data
        // shift data
        // write result
        // return message

        throw new NotImplementedException();
    }


}