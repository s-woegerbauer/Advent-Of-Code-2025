namespace AdventOfCode2024;

public static class Day11
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "11");
        var input = InputOutputHelper.GetInput(false, "11");
        
        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }
    
    private static Dictionary<string, List<string>> ParseInput(string[] input)
    {
        var result = new Dictionary<string, List<string>>();

        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var key = parts[0];
            var values = parts[1].Split(" ").ToList();
            result.Add(key, values);
        }
        
        return result;
    }

    private static long CountPaths(
        Dictionary<string, List<string>> graph,
        string curr,
        string target,
        Dictionary<string, long> memo)
    {
        if (curr == target) return 1;

        if (memo.TryGetValue(curr, out var cached)) return cached;

        long total = 0;
        if (!graph.TryGetValue(curr, out var neighbors))
        {
            memo[curr] = 0;
            return 0;
        }

        foreach (var nxt in neighbors)
        {
            total += CountPaths(graph, nxt, target, memo);
        }

        memo[curr] = total;
        return total;
    }

    private static void PartOne(bool isTest, string[] input)
    {
        var graph = ParseInput(input);
        var result = CountPaths(graph, "you", "out", new Dictionary<string, long>());
        InputOutputHelper.WriteOutput(isTest, result);
    }
    
    private static void PartTwo(bool isTest, string[] input)
    {
        var graph = ParseInput(input);
        long svrToFft = CountPaths(graph, "svr", "fft", new Dictionary<string, long>());
        long svrToDac = CountPaths(graph, "svr", "dac", new Dictionary<string, long>());
        long fftToDac = CountPaths(graph, "fft", "dac", new Dictionary<string, long>());
        long dacToFft = CountPaths(graph, "dac", "fft", new Dictionary<string, long>());
        long dacToOut = CountPaths(graph, "dac", "out", new Dictionary<string, long>());
        long fftToOut = CountPaths(graph, "fft", "out", new Dictionary<string, long>());
        
        long result = svrToFft * fftToDac * dacToOut + svrToDac * dacToFft * fftToOut;
        InputOutputHelper.WriteOutput(isTest, result);
    }
}