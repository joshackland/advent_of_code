namespace AoC2025;

class Day05
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day05.txt"));


    public void Part1()
    {
        int totalFreshIds = 0;
        var freshIdRanges = new List<(long start, long end)>();

        int blankIndex = input.FindIndex(string.IsNullOrWhiteSpace);

        for (int i = 0; i < blankIndex; i++)
        {
            string line = input[i];

            var parts = line.Split('-');
            long start = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);

            freshIdRanges.Add((start, end));
        }

        for (int i = blankIndex + 1; i < input.Count; i++)
        {
            long id = long.Parse(input[i]);

            foreach (var range in freshIdRanges)
            {
                if (id >= range.start && id <= range.end)
                {
                    totalFreshIds++;
                    break;             
                }
            }
        }


        Console.WriteLine($"Day 5 Part 1: {totalFreshIds}");
    }

    public void Part2()
    {
        long totalFreshIds = 0;
        var freshIdRanges = new List<(long start, long end)>();

        int blankIndex = input.FindIndex(string.IsNullOrWhiteSpace);

        for (int i = 0; i < blankIndex; i++)
        {
            string line = input[i];

            var parts = line.Split('-');
            long start = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);

            freshIdRanges.Add((start, end));
        }

        freshIdRanges.Sort((a, b) => a.start.CompareTo(b.start));
        var currentRange = freshIdRanges[0];

        var mergedRanges = new List<(long start, long end)>();

        for (int i = 1; i < freshIdRanges.Count; i++)
        {
            var next = freshIdRanges[i];

            if (next.start <= currentRange.end)
            {
                currentRange.end = Math.Max(next.end, currentRange.end);
            }
            else
            {
                mergedRanges.Add(currentRange);
                currentRange = next;
            }
        }

        mergedRanges.Add(currentRange);

        foreach (var range in mergedRanges)
        {
            totalFreshIds += range.end - range.start + 1;
        }


        Console.WriteLine($"Day 5 Part 2: {totalFreshIds}");
    }
}