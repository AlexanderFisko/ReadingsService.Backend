using System;
using System.Collections.Generic;

namespace ReadingsService.Backend.Core;

public static class SevenSegmentDisplay
{
    private static readonly Dictionary<byte, bool[]> _dict = new()
    {
        { 0, new[] { true, true, true, false, true, true, true } },
        { 1, new[] { false, false, true, false, false, true, false } },
        { 2, new[] { true, false, true, true, true, false, true } },
        { 3, new[] { true, false, true, true, false, true, true } },
        { 4, new[] { false, true, true, true, false, true, false } },
        { 5, new[] { true, true, false, true, false, true, true } },
        { 6, new[] { true, true, false, true, true, true, true } },
        { 7, new[] { true, false, true, false, false, true, false } },
        { 8, new[] { true, true, true, true, true, true, true } },
        { 9, new[] { true, true, true, true, false, true, true } }
    };

    public static IEnumerable<byte> GetPossibleValuesByPattern(bool[] pattern)
    {
        const int patternLength = 7;

        if (pattern.Length != patternLength)
            throw new ArgumentException("Invalid value", nameof(pattern));

        // Ресурсы не экономим, тупой перебор
        foreach (var item in _dict)
        {
            var possible = true;
            for (var i = 0; i < patternLength; i++)
                if (pattern[i] && !item.Value[i])
                {
                    possible = false;
                    break;
                }

            if (possible)
                yield return item.Key;
        }
    }
}