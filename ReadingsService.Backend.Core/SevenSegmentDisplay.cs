using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ReadingsService.Backend.Core;

public static class SevenSegmentDisplay
{
    private static readonly Dictionary<byte, BitArray> _dict = new()
    {
        { 0, new BitArray(new[] { true, true, true, false, true, true, true }) },
        { 1, new BitArray(new[] { false, false, true, false, false, true, false }) },
        { 2, new BitArray(new[] { true, false, true, true, true, false, true }) },
        { 3, new BitArray(new[] { true, false, true, true, false, true, true }) },
        { 4, new BitArray(new[] { false, true, true, true, false, true, false }) },
        { 5, new BitArray(new[] { true, true, false, true, false, true, true }) },
        { 6, new BitArray(new[] { true, true, false, true, true, true, true }) },
        { 7, new BitArray(new[] { true, false, true, false, false, true, false }) },
        { 8, new BitArray(new[] { true, true, true, true, true, true, true }) },
        { 9, new BitArray(new[] { true, true, true, true, false, true, true }) }
    };

    public const int PatternLength = 7;

    public static IEnumerable<byte> GetPossibleValuesByPattern(BitArray pattern)
    {
        if (pattern.Length != PatternLength)
            throw new ArgumentException("Invalid value", nameof(pattern));

        // Ресурсы не экономим, тупой перебор
        foreach (var item in _dict)
        {
            var possible = true;
            for (var i = 0; i < PatternLength; i++)
                if (pattern[i] && !item.Value[i])
                {
                    possible = false;
                    break;
                }

            if (possible)
                yield return item.Key;
        }
    }

    public static BitArray GetFromString(string value)
    {
        if (value.Length != PatternLength)
            throw new ArgumentException("Invalid value", nameof(value));

        var result = new BitArray(PatternLength);

        for (var i = 0; i < PatternLength; i++)
        {
            var chr = value[i];
            if (chr != '0' && chr != '1')
                throw new ArgumentException("Invalid value", nameof(value));

            result[i] = chr == '1';
        }

        return result;
    }

    public static string WriteToString(BitArray value)
    {
        var result = new StringBuilder(PatternLength);

        for (var i = 0; i < PatternLength; i++)
            result.Append(value[i] ? "1" : "0");

        return result.ToString();
    }
}