namespace AoC2025;

class Day01
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day01.txt"));


    public void Part1()
    {
        int position = 50;
        int zeroCount = 0;

        foreach (var line in input)
        {
            char direction = line[0];
            int distance = int.Parse(line.Substring(1));

            distance = distance % 100;

            if (direction == 'R')
            {
                position = (position + distance) % 100;
            }
            else
            {
                position = (position - distance) % 100;
                if (position < 0)
                {
                    position += 100;
                }
            }

            if (position == 0)
            {
                zeroCount++;
            }
        }

        Console.WriteLine(zeroCount);    
    }

    public void Part2()
    {
        int position = 50;
        int zeroCount = 0;

        foreach (var line in input)
        {
            int previousPosition = position;
            char direction = line[0];
            int distance = int.Parse(line.Substring(1));

            int fullRotations = distance / 100;
            zeroCount += fullRotations;
            distance %= 100;

            if (direction == 'R')
            {
                position += distance;
            }
            else
            {
                position -= distance;
                if (position < 0)
                {
                    position += 100;
                }
            }

            position %= 100;

            if (position == 0 || previousPosition != 0 && (
            (previousPosition > position && direction == 'R') ||
            (previousPosition < position && direction == 'L'))
            )
            {
                zeroCount++;
            }
        }

        Console.WriteLine(zeroCount);
    }
}