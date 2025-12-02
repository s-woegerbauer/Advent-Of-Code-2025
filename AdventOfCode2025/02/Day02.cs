using System.Numerics;

namespace AdventOfCode2024;

public static class Day02
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "02");
        var input = InputOutputHelper.GetInput(false, "02");
        
        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }

    private static bool IsRepeated(long number, bool requireExactTwo)
    {
        var numberString = number.ToString();
        int length = numberString.Length;
        if (length < 2) return false;
        if (numberString[0] == '0') return false;

        if (requireExactTwo)
        {
            if (length % 2 != 0) return false;
            int half = length / 2;
            return numberString.Substring(0, half).SequenceEqual(numberString.Substring(half, half));
        }

        for (int partLength = 1; partLength <= length / 2; partLength++)
        {
            if (length % partLength != 0) continue;
            int repeats = length / partLength;
            if (repeats < 2) continue;

            var first = numberString.Substring(0, partLength);
            bool allMatch = true;
            for (int i = 1; i < repeats; i++)
            {
                if (!first.SequenceEqual(numberString.Substring(i * partLength, partLength)))
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch) return true;
        }

        return false;
    }

    private static void PartOne(bool isTest, string[] input)
    {
        long total = 0;

        foreach (var line in input)
        {
            var ranges = line.Split(',');
            foreach (var range in ranges)
            {
                var parts = range.Split('-');
                if (parts.Length != 2) continue;
                if (!long.TryParse(parts[0], out var start)) continue;
                if (!long.TryParse(parts[1], out var end)) continue;

                for (long i = start; i <= end; i++)
                {
                    if (IsRepeated(i, requireExactTwo: true)) total += i;
                }
            }
        }

        InputOutputHelper.WriteOutput(isTest, total.ToString());
    }

    private static void PartTwo(bool isTest, string[] input)
    {
        long total = 0;

        foreach (var line in input)
        {
            var ranges = line.Split(',');
            foreach (var range in ranges)
            {
                var parts = range.Split('-');
                if (parts.Length != 2) continue;
                if (!long.TryParse(parts[0], out var start)) continue;
                if (!long.TryParse(parts[1], out var end)) continue;

                for (long i = start; i <= end; i++)
                {
                    if (IsRepeated(i, requireExactTwo: false)) total += i;
                }
            }
        }

        InputOutputHelper.WriteOutput(isTest, total.ToString());
    }
}