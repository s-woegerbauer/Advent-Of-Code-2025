namespace AdventOfCode2024;

public static class Day03
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "03");
        var input = InputOutputHelper.GetInput(false, "03");
        
        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }
    
    static long MaxSubsequenceValue(string sequence, int digits)
    {
        int length = sequence.Length;
        var stack = new Stack<char>();

        for (int i = 0; i < length; i++)
        {
            char c = sequence[i];
            while (stack.Count > 0 && stack.Peek() < c && stack.Count - 1 + (length - i) >= digits)
            {
                stack.Pop();
            }

            if (stack.Count < digits)
            {
                stack.Push(c);
            }
        }

        long result = 0;
        long factor = 1;
        while (stack.Count > 0)
        {
            result += int.Parse(stack.Pop().ToString()) * factor;
            factor *= 10;
        }

        return result;
    }



    private static void PartOne(bool isTest, string[] input)
    {
        long result = 0;

        foreach (var line in input)
        {
            result += MaxSubsequenceValue(line, 2);
        }
        
        InputOutputHelper.WriteOutput(isTest, result);
    }
    
    private static void PartTwo(bool isTest, string[] input)
    {
        long result = 0;

        foreach (var line in input)
        {
            result += MaxSubsequenceValue(line, 12);
        }
        
        InputOutputHelper.WriteOutput(isTest, result);
    }
}