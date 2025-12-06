namespace AdventOfCode2024;

public static class Day06
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "06");
        var input = InputOutputHelper.GetInput(false, "06");

        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }

    private static void PartOne(bool isTest, string[] input)
    {
        var problems = ParseProblems(input, partTwo: false);

        long grandTotal = 0;
        foreach (var (op, nums) in problems)
        {
            long value = op == '+' ? nums.Sum() : nums.Aggregate(1L, (a, b) => a * b);
            grandTotal += value;
        }

        InputOutputHelper.WriteOutput(isTest, grandTotal);
    }

    private static void PartTwo(bool isTest, string[] input)
    {
        var problems = ParseProblems(input, partTwo: true);

        long result = 0;
        foreach (var (op, nums) in problems)
        {
            long value = op == '+' ? nums.Sum() : nums.Aggregate(1L, (a, b) => a * b);
            result += value;
        }

        InputOutputHelper.WriteOutput(isTest, result);
    }


    private static List<(char op, List<long> nums)> ParseProblems(string[] lines, bool partTwo = false)
    {
        var result = new List<(char op, List<long> nums)>();

        int rows = lines.Length;
        int cols = lines.Max(l => l?.Length ?? 0);

        var padded = new string[rows];
        for (int r = 0; r < rows; r++)
        {
            padded[r] = lines[r].PadRight(cols);
        }
        
        var isSeparator = new bool[cols];
        int col;
        for (col = 0; col < cols; col++)
        {
            bool seperator = true;
            for (int row = 0; row < rows; row++)
            {
                if (padded[row][col] != ' ')
                {
                    seperator = false;
                    break;
                }
            }

            isSeparator[col] = seperator;
        }

        col = 0;
        while (col < cols)
        {
            if (isSeparator[col])
            {
                col++;
                continue;
            }

            int start = col;
            
            while (col < cols && !isSeparator[col])
            {
                col++;
            }
            
            int end = col - 1;
            int width = end - start + 1;

            var numbers = new List<long>();

            if (!partTwo)
            {
                for (int r = 0; r < rows - 1; r++)
                {
                    var token = padded[r].Substring(start, width).Trim();
                    if (!string.IsNullOrEmpty(token))
                    {
                        if (long.TryParse(token, out var number))
                        {
                            numbers.Add(number);
                        }
                    }
                }
            }
            else
            {
                for (int c = end; c >= start; c--)
                {
                    var sb = new System.Text.StringBuilder();
                    for (int r = 0; r < rows - 1; r++)
                    {
                        sb.Append(padded[r][c]);
                    }

                    var token = sb.ToString().Trim();
                    if (!string.IsNullOrEmpty(token))
                    {
                        if (long.TryParse(token, out var number))
                        {
                            numbers.Add(number);
                        }
                    }
                }
            }

            var op = padded[^1].Substring(start, width).Trim()[0];
            
            result.Add((op, numbers));
        }

        return result;
    }
}