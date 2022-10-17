// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Security.Cryptography;

namespace Enigma.Research.ControlGroups;

/// <summary>Create true random numbers to be used in the construction of a control group.</summary>
public interface IControlGroupRng
{
    /// <summary>Create a list of random integers.</summary>
    /// <param name="minInclusive">Minimum value of range (inclusive).</param>
    /// <param name="maxExclusive">Maximum value of range (exclusive).</param>
    /// <param name="count">Number of random values that should be returned.</param>
    /// <returns>The generated values.If the sequence of minInclusive/maxExclusive is wrong, or if count is invalid, an empty list is returned.</returns>
    List<int> GetIntegers(int minInclusive, int maxExclusive, int count);

    /// <summary>Create a list of random integers.</summary>
    /// <remarks>Uses 0 as the minimum inclusive value.</remarks>
    /// <param name="maxExclusive">Maximum value of range (exclusive).</param>
    /// <param name="count">Number of random values that should be returned.</param>
    /// <returns>The generated values. If not maxExclusive > 0, or if count is invalid, an empty list is returned.</returns>
    List<int> GetIntegers(int maxExclusive, int count);
}


/// <inheritdoc/>
public class ControlGroupRng: IControlGroupRng 
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
}