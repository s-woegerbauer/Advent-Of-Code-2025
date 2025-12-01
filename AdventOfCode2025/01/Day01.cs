namespace AdventOfCode2024;

public static class Day01
{
    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "01");
        var input = InputOutputHelper.GetInput(false, "01");
        
        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }

    private static void PartOne(bool isTest, string[] input)
    {
        int position = 50;
        int count = 0;

        foreach (var line in input)
        {
            char direction = line[0];
            int distance = int.Parse(line.Substring(1));

            if (direction == 'L')
            {
                position = (position - distance) % 100;
                
                if (position < 0) {
                    position += 100;
                }
            }
            else
            {
                position = (position + distance) % 100;
            }

            if (position == 0)
            {
                count++;
            }
        }

        InputOutputHelper.WriteOutput(isTest, count);
    }

    
    private static void PartTwo(bool isTest, string[] input)
    {
        int position = 50;
        int count = 0;

        foreach (var line in input)
        {
            char direction = line[0];
            int distance = int.Parse(line.Substring(1));

            for (int i = 0; i < distance; i++)
            {
                if (direction == 'L')
                {
                    position = (99 + position) % 100;
                }
                else
                {
                    position = (position + 1) % 100;
                }

                if (position == 0)
                {
                    count++;
                }
            }
        }

        InputOutputHelper.WriteOutput(isTest, count);
    }

}