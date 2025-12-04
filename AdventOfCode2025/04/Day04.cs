namespace AdventOfCode2024;

public static class Day04
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "04");
        var input = InputOutputHelper.GetInput(false, "04");
        
        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }
    
    static (int, int)[] directions = new (int rowDelta, int colDelta)[]
    {
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1), (0, 1),
        (1, -1), (1, 0), (1, 1)
    };
        
    static List<(int, int)> GetForklifts(char[][] charArray)
    {
        var forklifts = new List<(int, int)>();
            
        for (int i = 0; i < charArray.Length; i++)
        {
            for (int j = 0; j < charArray[i].Length; j++)
            {
                if (charArray[i][j] != '@')
                    continue;

                var adjacent = 0;
                foreach (var (rowDelta, colDelta) in directions)
                {
                    var ni = i + rowDelta;
                    var nj = j + colDelta;
                    if (ni < 0 || ni >= charArray.Length) continue;
                    if (nj < 0 || nj >= charArray[ni].Length) continue;
                    if (charArray[ni][nj] == '@')
                    {
                        adjacent++;
                    }
                }

                if (adjacent < 4)
                {
                    forklifts.Add((i, j));
                }
            }
        }
        
        return forklifts;
    }

    private static void PartOne(bool isTest, string[] input)
    {
        var result = 0;
        var charArray = input.Select(line => line.ToCharArray()).ToArray();
        
        var forklifts = GetForklifts(charArray);
        result = forklifts.Count;
        
        InputOutputHelper.WriteOutput(isTest, result);
    }
    
    private static void PartTwo(bool isTest, string[] input)
    {
        var result = 0;
        
        var charArray = input.Select(line => line.ToCharArray()).ToArray();
        var forklifts = GetForklifts(charArray);

        while (forklifts.Count != 0)
        {
            foreach(var (forkliftRow, forkliftCol) in forklifts)
            {
                charArray[forkliftRow][forkliftCol] = 'x';
                result++;
            }
            
            forklifts = GetForklifts(charArray);
        }
        
        
        InputOutputHelper.WriteOutput(isTest, result);
    }
}