namespace AdventOfCode2024;

public static class Day07
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "07");
        var input = InputOutputHelper.GetInput(false, "07");

        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }

    private static long[,] ParseInput(string[] input)
    {
        int rows = input.Length;
        int cols = input[0].Length;

        var map = new long[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            var line = input[row];
            for (int col = 0; col < cols; col++)
            {
                char ch = col < line.Length ? line[col] : '.';
                if (ch == '^')
                    map[row, col] = -1;
                if (ch == 'S')
                {
                    map[row, col] = -2;
                }
            }
        }

        return (map);
    }
    
    private static void PrintMap(long[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                long val = map[row, col];
                string s = val switch
                {
                    -2 => "S",
                    -1 => "^",
                    0 => ".",
                    _ => val.ToString()
                };
                Console.Write(s);
            }
            Console.WriteLine();
        }
    }
    
    private static bool InBounds(long[,] map, int r, int c)
    {
        return r >= 0 && r < map.GetLength(0) && c >= 0 && c < map.GetLength(1);
    }

    private static bool IsPositiveAt(long[,] map, int r, int c)
    {
        return InBounds(map, r, c) && map[r, c] > 0;
    }

    private static bool IsSplitterAt(long[,] map, int r, int c)
    {
        return InBounds(map, r, c) && map[r, c] == -1;
    }

    private static int ApplyBeam(long[,] map)
    {
        int result = 0;
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (map[row, col] == -2)
                {
                    if (InBounds(map, row + 1, col)) map[row + 1, col]++;
                }
                else if (IsSplitterAt(map, row, col) && IsPositiveAt(map, row - 1, col))
                {
                    if (InBounds(map, row, col - 1)) map[row, col - 1] += map[row - 1, col];
                    if (InBounds(map, row, col + 1)) map[row, col + 1] += map[row - 1, col];

                    result++;
                }
                else if (IsPositiveAt(map, row - 1, col))
                {
                    bool aboveAbovePositive = IsPositiveAt(map, row - 2, col);
                    bool sideSplitter = IsSplitterAt(map, row - 1, col - 1) || IsSplitterAt(map, row - 1, col + 1);

                    if (aboveAbovePositive || sideSplitter)
                    {
                        map[row, col] += map[row - 1, col];
                    }
                }
            }
        }

        return result;
    }

    private static void PartOne(bool isTest, string[] input)
    {
        var map = ParseInput(input);
        var result = ApplyBeam(map);
        PrintMap(map);
        InputOutputHelper.WriteOutput(isTest, result);
    }

    private static void PartTwo(bool isTest, string[] input)
    {
        long result = 0;
        var map = ParseInput(input);
        ApplyBeam(map);
        PrintMap(map);

        for (int col = 0; col < map.GetLength(1); col++)
        {
            result += map[map.GetLength(0) - 1, col];
        }
        
        InputOutputHelper.WriteOutput(isTest, result);
    }
}