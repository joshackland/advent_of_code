using System.Diagnostics;

namespace AoC2025;

class Day03
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day03.txt"));


    public void Part1()
    {
        int total = 0;

        foreach (var line in input)
        {
            int bestLine = 0;

            int length = line.Length;

            int first = 0;
            int last = 0;

            for (int i = 0; i < length; i++)
            {
                int num;
                
                int.TryParse(line[i].ToString(), out num);

                if (i != length - 1 && num > first)
                {
                    first = num;
                    last = 0;
                }
                else if (num > last)
                {
                    last = num;
                }
            }

            bestLine = (first * 10) + last;
            total += bestLine;
        }

        Console.WriteLine($"Day 3 Part 1: {total}");
    }

    public void Part2()
    {
        double total = 0;

        foreach (var line in input)
        {
            double[] bestLine = new double[12];

            int length = line.Length;
            

            for (int i = 0; i < length; i++)
            {
                double num;
                
                double.TryParse(line[i].ToString(), out num);


                for (int arrayIndex = 0; arrayIndex < bestLine.Length; arrayIndex++)
                {
                    int lastPossibleIndex = length - 12 + arrayIndex;
                    if (i <= lastPossibleIndex && num > bestLine[arrayIndex])
                    {
                        bestLine[arrayIndex] = num;
                        
                        for (int arrayChange = arrayIndex+1; arrayChange < bestLine.Length; arrayChange++)
                        {                            
                            bestLine[arrayChange] = 0;
                        }

                        break;
                    }
                }
            }

            double lineTotal = 0;
            
            for (int arrayIndex = 0; arrayIndex < bestLine.Length; arrayIndex++)
            {
                lineTotal += bestLine[arrayIndex] * Math.Pow(10, 11 - arrayIndex);
            }

            Console.WriteLine($"{lineTotal}");

            total += lineTotal;
        }

        Console.WriteLine($"Day 3 Part 2: {total}");
    }
}