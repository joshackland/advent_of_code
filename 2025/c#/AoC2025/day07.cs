namespace AoC2025;

class Day07
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day07.txt"));


    public void Part1()
    {
        int totalSplits = 0;

        int startRow = -1;
        int startCol = -1;

        int height = input.Count;
        int width = input[0].Length;

        for (int r = 0; r < height; r++)
        {
            int c = input[r].IndexOf('S');

            if (c != -1)
            {
                startRow = r;
                startCol = c;
                break;
            }
        }

        bool[] beams = new bool[width];
        beams[startCol] = true;

        for (int r = startRow + 1; r < height; r++)
        {
            string row = input[r];
            bool[] nextBeams = new bool[width];

            for (int c = 0; c < width; c++)
            {
                if (!beams[c])
                {
                    continue;
                }

                char cell = row[c];

                if (cell == '^')
                {
                    nextBeams[c - 1] = true;
                    nextBeams[c + 1] = true;
                    totalSplits++;
                }
                else
                {
                    nextBeams[c] = true;                    
                }
            }

            beams = nextBeams;
        }

        Console.WriteLine($"Day 7 Part 1: {totalSplits}");
    }

    public void Part2()
    {
        int startRow = -1;
        int startCol = -1;

        int height = input.Count;
        int width = input[0].Length;

        for (int r = 0; r < height; r++)
        {
            int c = input[r].IndexOf('S');

            if (c != -1)
            {
                startRow = r;
                startCol = c;
                break;
            }
        }

        long[] beams = new long[width];
        beams[startCol] = 1;

        for (int r = startRow + 1; r < height; r++)
        {
            string row = input[r];
            long[] nextBeams = new long[width];

            for (int c = 0; c < width; c++)
            {
                if (beams[c] == 0)
                {
                    continue;
                }

                char cell = row[c];

                if (cell == '^')
                {
                    nextBeams[c - 1] += beams[c];
                    nextBeams[c + 1] += beams[c];
                }
                else
                {
                    nextBeams[c] += beams[c];                    
                }
            }

            beams = nextBeams;
        }

        
        long totalSplits = 0;

        foreach (var beamCount in beams)
        {
            totalSplits += beamCount;
        }

        Console.WriteLine($"Day 7 Part 2: {totalSplits}");
    }
}