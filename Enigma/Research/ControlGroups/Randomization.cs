// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Research.Interfaces;
using System.Security.Cryptography;

namespace Enigma.Research.ControlGroups;




static class ExtensionsClass
{
    private static readonly IControlGroupRng controlGroupRng = new ControlGroupRng();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        List<int> randomNumbers = controlGroupRng.GetIntegers(n - 1, n);

        int index = 0;
        while (n > 1)
        {
            n--;
            int k = randomNumbers[index++];
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}


/// <inheritdoc/>
public class ControlGroupRng : IControlGroupRng
{

    /// <inheritdoc/>
    public List<int> GetIntegers(int minInclusive, int maxExclusive, int count)
    {
        List<int> randomNumbers = new();
        if (minInclusive >= maxExclusive || count <= 0) return randomNumbers;
        for (int i = 0; i < count; i++)
        {
            randomNumbers.Add(RandomNumberGenerator.GetInt32(minInclusive, maxExclusive));
        }
        return randomNumbers;
    }

    /// <inheritdoc/>
    public List<int> GetIntegers(int maxExclusive, int count)
    {
        return GetIntegers(0, maxExclusive, count);
    }


    public List<int> ShuffleList(List<int> data)
    {
        data.Shuffle();
        return data;
    }

    public List<double> ShuffleList(List<double> data)
    {
        data.Shuffle();
        return data;
    }


}