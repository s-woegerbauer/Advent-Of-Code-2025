namespace AdventOfCode2024;

public static class Day12
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "12");
        var input = InputOutputHelper.GetInput(false, "12");

        PartOne(true, testInput);
        PartOne(false, input);

        //PartTwo(true, testInput);
        //PartTwo(false, input);
    }

    private static void PartOne(bool isTest, string[] input)
    {
        var (shapes, regions) = ParseInput(input);
        var result = 0;

        foreach (var (width, height, counts) in regions)
        {
            int totalPresents = counts.Sum();
            int totalCells = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                totalCells += counts[i] * shapes[i].Count(c => c);
            }

            int availableSpace = width * height;
            
            int slots3x3 = (width / 3) * (height / 3);
            
            if (totalCells > availableSpace) continue;
            
            if (totalPresents <= slots3x3)
            {
                result++;
            }
        }

        InputOutputHelper.WriteOutput(isTest, result);
    }

    private static (List<bool[]> shapes, List<(int width, int height, int[] counts)> regions) ParseInput(string[] input)
    {
        var shapes = new List<bool[]>();
        var regions = new List<(int width, int height, int[] counts)>();
        
        int i = 0;
        while (i < input.Length && !string.IsNullOrEmpty(input[i]))
        {
            if (input[i].Split(':')[1].Length == 0)
            {
                var shape = new bool[9];
                for (int row = 0; row < 3 && i + 1 + row < input.Length; row++)
                {
                    var line = input[i + 1 + row];
                    for (int col = 0; col < 3 && col < line.Length; col++)
                    {
                        shape[row * 3 + col] = line[col] == '#';
                    }
                }
                shapes.Add(shape);
                i += 5;
            }
            else
            {
                break;
            }
        }
        
        while (i < input.Length)
        {
            var line = input[i];
            
            var parts = line.Split(": ");
            var dims = parts[0].Split('x');
            int width = int.Parse(dims[0]);
            int height = int.Parse(dims[1]);
            var counts = parts[1].Split(' ').Select(int.Parse).ToArray();
            
            regions.Add((width, height, counts));
            i++;
        }
        
        return (shapes, regions);
    }

    private static void PartTwo(bool isTest, string[] input)
    {
        var result = 0;
        InputOutputHelper.WriteOutput(isTest, result);
    }
}