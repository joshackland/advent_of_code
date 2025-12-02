using System.Diagnostics;

namespace AoC2025;

class Day02
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day02.txt"));


    public void Part1()
    {
        var ranges = input[0]
        .Split(',')
        .Select(r => r.Split('-').Select(n => Convert.ToDouble(n)).ToArray())
        .ToArray();

        double output = 0;

        foreach (var range in ranges)
        {
            double start = range[0];
            double end = range[1];

            for (double id = start; id <= end; id++)
            {
                string idString = id.ToString();

                if (idString.Length % 2 != 0)
                {
                    continue;
                }

                int halfLength = idString.Length / 2;

                if (idString.Substring(0, halfLength) == idString.Substring(halfLength))
                {
                    output += id;
                }
            }
        }

        Console.WriteLine($"Part One: {output}");
    }

    public void Part2()
    {
        var ranges = input[0]
        .Split(',')
        .Select(r => r.Split('-').Select(n => Convert.ToDouble(n)).ToArray())
        .ToArray();

        double output = 0;

        foreach (var range in ranges)
        {
            double start = range[0];
            double end = range[1];

            for (double id = start; id <= end; id++)
            {
                string idString = id.ToString();
                int length = idString.Length;

                for (int patternLength = 1; patternLength <= length/2; patternLength++)
                {
                    if (length % patternLength != 0)
                    {
                        continue;
                    }

                    string pattern = idString.Substring(0,patternLength);

                    bool isMatch = true;
                    for (int repeat = 1; repeat < length / patternLength; repeat++)
                    {
                        if (pattern != idString.Substring(repeat * patternLength, patternLength))
                        {
                            isMatch = false;
                            break;
                        }
                    }

                    if (isMatch)
                    {
                        output += id;
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"Part Two: {output}");        
    }
}