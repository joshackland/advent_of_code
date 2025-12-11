using Microsoft.VisualBasic;

namespace AoC2025;

class Day11
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day11.txt"));


    public void Part1()
    {
        var graph = new Dictionary<string, List<string>>();

        foreach (var line in input)
        {
            var parts = line.Split(':');

            var source = parts[0];

            graph[source] = new();

            var targets = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var target in targets)
            {
                graph[source].Add(target);
            }
        }

        string start = "you";
        string end = "out";

        var serverCount = new Dictionary<string, int>();
        var visited = new HashSet<string>();

        int FindPaths(string current)
        {
            if (current == end)
            {
                return 1;
            }

            if (serverCount.TryGetValue(current, out int cached))
            {
                return cached;
            }

            if (visited.Contains(current))
            {
                return 0;
            }

            visited.Add(current);

            int total = 0;

            var connected = graph[current];

            foreach (var connection in connected)
            {
                total += FindPaths(connection);
            }

            visited.Remove(current);

            serverCount[current] = total;

            return total;
        }



        int totalPaths = FindPaths(start);

        Console.WriteLine($"Day 11 Part 1: {totalPaths}");
    }

    public void Part2()
    {     
        var graph = new Dictionary<string, List<string>>();

        foreach (var line in input)
        {
            var parts = line.Split(':');

            var source = parts[0];

            graph[source] = new();

            var targets = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var target in targets)
            {
                graph[source].Add(target);
            }
        }

        string start = "svr";
        string end = "out";

        var serverCount = new Dictionary<(string server, bool visitedDac, bool visitedFft), long>();
        var visited = new HashSet<string>();

        long FindPaths(string current, bool visitedDac, bool visitedFft)
        {
            if (current == "dac")
            {
                visitedDac = true;
            }
            if (current == "fft")
            {
                visitedFft = true;
            }

            if (current == end)
            {
                return (visitedDac && visitedFft) ? 1 : 0;
            }

            if (serverCount.TryGetValue((current, visitedDac, visitedFft), out long cached))
            {
                return cached;
            }

            if (visited.Contains(current))
            {
                return 0;
            }

            visited.Add(current);

            long total = 0;

            var connected = graph[current];

            foreach (var connection in connected)
            {
                total += FindPaths(connection, visitedDac, visitedFft);
            }

            visited.Remove(current);

            serverCount[(current, visitedDac, visitedFft)] = total;

            return total;
        }


        long totalPaths = FindPaths(start, false, false);

        Console.WriteLine($"Day 11 Part 2: {totalPaths}");
    }
}