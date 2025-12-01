# AdventOfCode2024

## Overview
This project contains solutions for the Advent of Code 2025 challenges. Each day has its own class with methods to solve the respective puzzles.

## Prerequisites
- .NET 6.0 SDK or later
- An IDE such as JetBrains Rider or Visual Studio

## Project Structure
- `AdventOfCode2025/DayXX.cs`: Contains the solution for Day XX.
- `InputOutputHelper.cs`: Helper class for reading input and writing output.

## How to Use

1. **Clone the Repository**
   ```sh
   git clone https://github.com/s-woegerbauer/Advent-Of-Code-2025.git
   cd AdventOfCode2025
   ```

2. **Open the Project**
   Open the project in your preferred IDE (e.g., JetBrains Rider).

3. **Run the Solution**
   Each day's solution can be executed by running the `Solve` method in the respective `DayXX` class. For example, to run the solution for Day 1:
   ```csharp
   AdventOfCode2025.Day01.Solve();
   ```

4. **Input and Output**
   - The input files should be placed in the directory with the number of the current day with the naming convention `input.txt` for actual input and `testInput.txt` for test input.
   - The output will be written to the console as specified in the `InputOutputHelper` class.

## Adding Solutions for New Days
1. Create a new directory named by the day `XX`.
2. Create a new file `DayXX.cs` in the `XX` directory.
3. Implement the `Solve`, `PartOne`, and `PartTwo` methods similar to the existing days.
4. Add input files `input.txt` and `testInput.txt` to the `XX` directory.
