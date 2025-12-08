namespace AdventOfCode2024;

public static class Day08
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "08");
        var input = InputOutputHelper.GetInput(false, "08");
        
        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }
    
    private static double EuclideanDistance((int, int, int) point1, (int, int, int) point2)
    {
        return Math.Sqrt(Math.Pow(point2.Item1 - point1.Item1, 2) +
                         Math.Pow(point2.Item2 - point1.Item2, 2) +
                         Math.Pow(point2.Item3 - point1.Item3, 2));
    }
    
    private static List<(int, int, int)> ParseInput(string[] input)
    {
        var result = new List<(int, int, int)>();
        
        foreach(var line in input)
        {
            var parts = line.Split(',');
            result.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
        }

        return result;
    }
    
    private static List<(int, int, double)> GetPairs(List<(int, int, int)> coords)
    {
        int n = coords.Count;
        var pairs = new List<(int a, int b, double distance)>(n * (n - 1) / 2);

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                pairs.Add((i, j, EuclideanDistance(coords[i], coords[j])));
            }
        }

        pairs.Sort((x, y) => x.distance.CompareTo(y.distance));
        return pairs;
    }
    
    private static long ComputeProductAfterKConnections(List<(int, int, int)> coords, int k)
    {
        int n = coords.Count;
        List<(int a, int b, double distance)> pairs = GetPairs(coords);

        var uf = new UnionFind(n);
        int limit = Math.Min(k, pairs.Count);
        for (int idx = 0; idx < limit; idx++)
        {
            uf.Union(pairs[idx].a, pairs[idx].b);
        }

        var sizeCounts = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            int root = uf.Find(i);
            if (!sizeCounts.TryGetValue(root, out var count)) count = 0;
            sizeCounts[root] = count + 1;
        }

        var topThree = sizeCounts.Values.OrderByDescending(v => v).Take(3).ToArray();
        long product = 1;
        foreach (var v in topThree) product *= v;
        return product;
    }
    
    private static long ComputeProductUntilSingleComponent(List<(int, int, int)> coords)
    {
        int n = coords.Count;
        List<(int a, int b, double distance)> pairs = GetPairs(coords);

        var uf = new UnionFind(n);
        int components = n;

        foreach (var pair in pairs)
        {
            if (uf.Union(pair.a, pair.b))
            {
                components--;
                if (components == 1)
                {
                    return (long)coords[pair.a].Item1 * coords[pair.b].Item1;
                }
            }
        }

        return 0;
    }

    private static void PartOne(bool isTest, string[] input)
    {
        var coords = ParseInput(input);
        long result = ComputeProductAfterKConnections(coords, 1000);
        InputOutputHelper.WriteOutput(isTest, result);
    }
    
    private static void PartTwo(bool isTest, string[] input)
    {
        var coords = ParseInput(input);
        long result = ComputeProductUntilSingleComponent(coords);
        InputOutputHelper.WriteOutput(isTest, result);
    }
}

class UnionFind
{
    private readonly int[] _parent;
    private readonly int[] _rank;

    public UnionFind(int n)
    {
        _parent = new int[n];
        _rank = new int[n];
        for (int i = 0; i < n; i++) _parent[i] = i;
    }

    public int Find(int x)
    {
        if (_parent[x] != x) _parent[x] = Find(_parent[x]);
        return _parent[x];
    }

    public bool Union(int x, int y)
    {
        int rx = Find(x);
        int ry = Find(y);
        if (rx == ry) return false;
        if (_rank[rx] < _rank[ry]) _parent[rx] = ry;
        else if (_rank[ry] < _rank[rx]) _parent[ry] = rx;
        else
        {
            _parent[ry] = rx;
            _rank[rx]++;
        }
        return true;
    }
}