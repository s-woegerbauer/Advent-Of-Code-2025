namespace AdventOfCode2024;

public static class Day05
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "05");
        var input = InputOutputHelper.GetInput(false, "05");
        
        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }
    
    public static (List<(long Start, long End)> Ranges, List<long> Ids) Parse(string[] lines)
    {
        var ranges = new List<(long Start, long End)>();
        var ids = new List<long>();
        long i = 0;

        for (; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                i++; 
                break;
            }
            var parts = line.Split('-');
            if (parts.Length == 2 && long.TryParse(parts[0], out var a) && long.TryParse(parts[1], out var b))
            {
                ranges.Add((a, b));
            }
        }
        
        for (; i < lines.Length; i++)
        {
            var line = lines[i];
            if (long.TryParse(line, out var id)) ids.Add(id);
        }

        return (ranges, ids);
    }

    private static void PartOne(bool isTest, string[] input)
    {
        long result = 0;
        
        var (ranges, ids) = Parse(input);

        foreach (var id in ids)
        {
            if(ranges.Any(r => id >= r.Start && id <= r.End)) result++;
        }
        
        InputOutputHelper.WriteOutput(isTest, result);
    }
    
    private static void PartTwo(bool isTest, string[] input)
    {
        long result = 0;
        
        var (ranges, _) = Parse(input);

        var mergedRanges = new List<(long Start, long End)>();
        foreach (var range in ranges.OrderBy(r => r.Start))
        {
            if (mergedRanges.Count == 0 || mergedRanges.Last().End < range.Start - 1)
            {
                mergedRanges.Add(range);
            }
            else
            {
                var last = mergedRanges.Last();
                mergedRanges[^1] = (last.Start, Math.Max(last.End, range.End));
            }
        }
        
        result = mergedRanges.Sum(mr => mr.End - mr.Start + 1);

        InputOutputHelper.WriteOutput(isTest, result);
    }
}