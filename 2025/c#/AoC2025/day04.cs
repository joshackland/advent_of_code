using System.Data;
using System.Diagnostics;

namespace AoC2025;

class Day04
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day04.txt"));


    public void Part1()
    {
        int height = input.Count;
        int width = input[0].Length;
        int output = 0;

        for (int y = 0; y < height; y++)
        {
            string row = input[y];

            for (int x = 0; x < width; x++)
            {
                char cell = row[x];

                if (cell != '@')
                {
                    continue;
                }

                int neighbourCount = 0;

                for (int ny = -1; ny <= 1; ny++)
                {
                    int currentY = y + ny;

                    if (currentY < 0 || currentY >= height)
                    {
                        continue;
                    }

                    for (int nx = -1; nx <= 1; nx++)
                    {
                        int currentX = x + nx;

                        if (currentX < 0 || currentX >= width || (x == currentX && y == currentY))
                        {
                            continue;
                        }

                        if (input[currentY][currentX] == '@')
                        {
                            neighbourCount++;
                        }
                    }
                }

                if (neighbourCount < 4)
                {
                    output++;
                }
            }
        }


        Console.WriteLine($"Day 4 Part 1: {output}");
    }

    public void Part2()
    {
        int height = input.Count;
        int width = input[0].Length;
        
        var grid = new char[height, width];
        
        for (int y = 0; y < height; y++)
        {
            string row = input[y];

            for (int x = 0; x < width; x++)
            {
                grid[y, x] = row[x];
            }
        }

        int output = 0;

        int currentRemove = -1;

        while (currentRemove != 0)
        {
            currentRemove = 0;
            var toBeRemoved = new List<int[]>();
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char cell = grid[y, x];

                    if (cell != '@')
                    {
                        continue;
                    }

                    int neighbourCount = 0;

                    for (int ny = -1; ny <= 1; ny++)
                    {
                        int currentY = y + ny;

                        if (currentY < 0 || currentY >= height)
                        {
                            continue;
                        }

                        for (int nx = -1; nx <= 1; nx++)
                        {
                            int currentX = x + nx;

                            if (currentX < 0 || currentX >= width || (x == currentX && y == currentY))
                            {
                                continue;
                            }

                            if (grid[currentY, currentX] == '@')
                            {
                                neighbourCount++;
                            }
                        }
                    }

                    if (neighbourCount < 4)
                    {
                        toBeRemoved.Add([y, x]);
                        currentRemove++;
                    }
                }
            }

            output += currentRemove;

            foreach (var coord in toBeRemoved)
            {
                int y = coord[0];
                int x = coord[1];
                grid[y,x] = '.';  
            }
        }

        

        Console.WriteLine($"Day 4 Part 2: {output}");
    }
}