using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode2024;

public static class Day10
{
    private record Machine(HashSet<int> Goal, List<HashSet<int>> Buttons, List<int> Counters);

    public static void Solve()
    {
        var testInput = InputOutputHelper.GetInput(true, "10");
        var input = InputOutputHelper.GetInput(false, "10");

        PartOne(true, testInput);
        PartOne(false, input);

        PartTwo(true, testInput);
        PartTwo(false, input);
    }

    private static List<Machine> ParseInput(string[] input)
    {
        var result = new List<Machine>();
        var bracketRegex = new Regex(@"\[([^\]]*)\]");
        var parenRegex = new Regex(@"\(([^)]*)\)");
        var braceRegex = new Regex(@"\{([^}]*)\}");

        foreach (var line in input)
        {
            var mBracket = bracketRegex.Match(line);
            if (!mBracket.Success) continue;

            var patternStr = mBracket.Groups[1].Value;
            var goal = new HashSet<int>();
            for (int i = 0; i < patternStr.Length; i++)
            {
                if (patternStr[i] == '#') goal.Add(i);
            }

            var buttons = new List<HashSet<int>>();
            foreach (Match m in parenRegex.Matches(line))
            {
                var inner = m.Groups[1].Value.Trim();
                if (string.IsNullOrEmpty(inner))
                {
                    buttons.Add(new HashSet<int>());
                    continue;
                }

                var buttonSet = inner
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s.Trim()))
                    .ToHashSet();
                buttons.Add(buttonSet);
            }

            var counters = new List<int>();
            var mBrace = braceRegex.Match(line);
            if (mBrace.Success)
            {
                var inner = mBrace.Groups[1].Value.Trim();
                if (!string.IsNullOrEmpty(inner))
                {
                    counters = inner
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => int.Parse(s.Trim()))
                        .ToList();
                }
            }

            result.Add(new Machine(goal, buttons, counters));
        }

        return result;
    }

    private static void PartOne(bool isTest, string[] input)
    {
        long total = 0;
        var machines = ParseInput(input);

        foreach (var machine in machines)
        {
            var result = FindFewestButtonPressesIndicatorLights(machine.Goal, machine.Buttons);
            total += result;
        }

        InputOutputHelper.WriteOutput(isTest, total);
    }

    private static int FindFewestButtonPressesIndicatorLights(HashSet<int> goal, List<HashSet<int>> buttons)
    {
        var q = new Queue<(HashSet<int> state, int steps)>();
        q.Enqueue((new HashSet<int>(), 0));
        var seen = new HashSet<string>();
        seen.Add(SetToString(new HashSet<int>()));

        while (q.Count > 0)
        {
            var (curr, steps) = q.Dequeue();
            if (curr.SetEquals(goal))
                return steps;

            foreach (var button in buttons)
            {
                var newSet = new HashSet<int>(curr);
                newSet.SymmetricExceptWith(button);

                var key = SetToString(newSet);
                if (seen.Contains(key)) continue;

                seen.Add(key);
                q.Enqueue((newSet, steps + 1));
            }
        }

        return -1;
    }

    private static string SetToString(HashSet<int> set)
    {
        return string.Join(",", set.OrderBy(x => x));
    }

    private static void PartTwo(bool isTest, string[] input)
    {
        long total = 0;
        var machines = ParseInput(input);

        foreach (var machine in machines)
        {
            var result = FindFewestButtonPressesJoltageLevel(machine.Counters, machine.Buttons);
            total += result;
        }

        InputOutputHelper.WriteOutput(isTest, total);
    }

    private static long FindFewestButtonPressesJoltageLevel(List<int> counters, List<HashSet<int>> buttons)
    {
        using var ctx = new Microsoft.Z3.Context();
        var opt = ctx.MkOptimize();

        var buttonVars = new Microsoft.Z3.IntExpr[buttons.Count];
        for (int i = 0; i < buttons.Count; i++)
        {
            buttonVars[i] = ctx.MkIntConst($"B{i}");
            opt.Add(ctx.MkGe(buttonVars[i], ctx.MkInt(0)));
        }

        for (int pos = 0; pos < counters.Count; pos++)
        {
            var terms = new List<Microsoft.Z3.ArithExpr>();
            for (int j = 0; j < buttons.Count; j++)
            {
                if (buttons[j].Contains(pos))
                {
                    terms.Add(buttonVars[j]);
                }
            }

            if (terms.Count > 0)
            {
                var sum = ctx.MkAdd(terms.ToArray());
                opt.Add(ctx.MkEq(sum, ctx.MkInt(counters[pos])));
            }
            else if (counters[pos] != 0)
            {
                return -1;
            }
        }

        var totalPresses = ctx.MkAdd(buttonVars);
        opt.MkMinimize(totalPresses);

        if (opt.Check() == Microsoft.Z3.Status.SATISFIABLE)
        {
            var model = opt.Model;
            long result = 0;
            foreach (var v in buttonVars)
            {
                var val = model.Evaluate(v);
                result += ((Microsoft.Z3.IntNum)val).Int64;
            }
            return result;
        }

        return -1;
    }

}


