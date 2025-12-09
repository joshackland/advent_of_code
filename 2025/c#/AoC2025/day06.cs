namespace AoC2025;

class Day06
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day06.txt"));


    public void Part1()
    {
        long output = 0;

        List<List<string>> grid = input
            .Select(row => row
                .Split(' ')
                .Where(str => !string.IsNullOrEmpty(str))
                .ToList()
            )
            .ToList();

        int rowCount = grid.Count;
        int columnCount = grid[0].Count;
        
        for (int col = 0; col < columnCount; col++)
        {
            string colOperator = grid[rowCount-1][col];

            long colTotal = long.Parse(grid[0][col]);

            for (int i = 1; i < rowCount - 1; i++)
            {
                if (colOperator == "+")
                {
                    colTotal += long.Parse(grid[i][col]);
                }
                else
                {
                    colTotal *= long.Parse(grid[i][col]);                    
                }
            }
            output += colTotal;
        }

        Console.WriteLine($"Day 6 Part 1: {output}");
    }

    public void Part2()
    {
        long output = 0;

        var columnOperators = input[input.Count - 1]
                .Split(' ')
                .Where(str => !string.IsNullOrEmpty(str))
                .ToList();

        var grid = input.SkipLast<string>(1).ToList();

        int operatorIndex = columnOperators.Count - 1;
        int columnIndex = grid[0].Length - 1;

        var columnValues = new List<string>();

        for (int col = columnIndex; col >= 0; col--)
        {
            bool valueExists = false;
            string value = "";
            

            foreach(var cell in grid)
            {
                string cellStr = cell[col].ToString();
                if (!string.IsNullOrWhiteSpace(cellStr))
                {
                    valueExists = true;
                    value += cellStr;
                }
            }

            if (valueExists)
            {
                columnValues.Add(value);
            }
            if (!valueExists || col == 0)
            {
                var colOperator = columnOperators[operatorIndex];
                long colTotal = long.Parse(columnValues[0]);

                for (int i = 1; i < columnValues.Count; i++)
                {
                    var colNumber = long.Parse(columnValues[i]);

                    if (colOperator == "+")
                    {
                        colTotal += colNumber;
                    }
                    else
                    {
                        colTotal *= colNumber;                    
                    }
                }
                output += colTotal;

                columnValues = new List<string>();
                value = "";
                operatorIndex--;
            }
        }

        Console.WriteLine($"Day 6 Part 2: {output}");
    }
}