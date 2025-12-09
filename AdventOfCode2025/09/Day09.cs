using NetTopologySuite.Geometries;

namespace AdventOfCode2024;

public static class Day09
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "09");
        var input = InputOutputHelper.GetInput(false, "09");
        
        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }

    private static void PartOne(bool isTest, string[] input)
    {
        long result = 0;
        
        List<(long row, long col)> points = input.Select(line =>
        {
            var parts = line.Split(',');
            return (long.Parse(parts[0]), long.Parse(parts[1]));
        }).ToList();

        foreach (var coord1 in points)
        {
            foreach (var coord2 in points)
            {
                if(coord1 == coord2) continue;
                
                long area = Math.Abs(coord1.row - coord2.row + 1) * Math.Abs(coord1.col - coord2.col + 1);
                result = Math.Max(result, area);
            }
        }
        
        InputOutputHelper.WriteOutput(isTest, result);
    }
    
    private static void PartTwo(bool isTest, string[] input)
    {
        List<(long row, long col)> points = input.Select(line =>
        {
            var parts = line.Split(',');
            return (long.Parse(parts[0]), long.Parse(parts[1]));
        }).ToList();

        var coords = new Coordinate[points.Count + 1];
        for (int i = 0; i < points.Count; i++)
        {
            coords[i] = new Coordinate(points[i].col, points[i].row);
        }
        coords[points.Count] = coords[0];

        long res = 0;
        var factory = new GeometryFactory();
        var poly = factory.CreatePolygon(coords);

        var ring = coords.Take(coords.Length - 1).ToArray();
        foreach (var (a, ai) in ring.Select((c, idx) => (c, idx)))
        {
            foreach (var (b, bi) in ring.Select((c, idx) => (c, idx)))
            {
                if (ai == bi) continue;
                var envelope = new Envelope(a, b);
                var subset = factory.ToGeometry(envelope);
                if (subset.Within(poly))
                {
                    var area = (long)((Math.Abs(b.X - a.X) + 1) * (Math.Abs(b.Y - a.Y) + 1));
                    if (area > res) res = area;
                }
            }
        }

        InputOutputHelper.WriteOutput(isTest, res);
    }
}