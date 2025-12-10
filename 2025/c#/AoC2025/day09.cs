using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

namespace AoC2025;

class Day09
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day09.txt"));
   

    public void Part1()
    {
        var points = new List<(int x, int y)>();

        foreach (var line in input)
        {
            var parts = line.Split(',');

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);

            points.Add((x, y));
        }

        long maxArea = 0;

        for (int i = 0; i < points.Count; i++)
        {
            var p1 = points[i];
            for (int j = i + 1; j < points.Count; j++)
            {
                var p2 = points[j];

                long dx = Math.Abs(p1.x - p2.x);
                long dy = Math.Abs(p1.y - p2.y);

                long area = (dx + 1) * (dy + 1);

                maxArea = Math.Max(maxArea, area);
            }
        }

        Console.WriteLine($"Day 9 Part 1: {maxArea}");
    }

    public void Part2()
    {
        var points = new List<(int x, int y)>();

        foreach (var line in input)
        {
            var parts = line.Split(',');

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);

            points.Add((x, y));
        }

        var xs = points.Select(p => p.x).Distinct().OrderBy(v => v).ToList();
        var ys = points.Select(p => p.y).Distinct().OrderBy(v => v).ToList();

        var xIndex = new Dictionary<int, int>();
        var yIndex = new Dictionary<int, int>();
        
        for (int i = 0; i < xs.Count; i++)
        {
            xIndex[xs[i]] = i;
        }
        
        for (int i = 0; i < ys.Count; i++)
        {
            yIndex[ys[i]] = i;
        }

        int width = xs.Count + 2;
        int height = ys.Count + 2;

        bool[,] wall = new bool[width, height];

        var redTileCells = new List<(int offsetX, int offsetY)>();

        for (int i = 0; i < points.Count; i++)
        {
            var p = points[i];
            int offsetX = xIndex[p.x] + 1;
            int offsetY = yIndex[p.y] + 1;

            wall[offsetX, offsetY] = true;
            redTileCells.Add((offsetX, offsetY));
        }


        for (int i = 0; i < points.Count; i++)
        {
            var a = points[i];
            var b = points[(i+1) % points.Count];

            int ax = xIndex[a.x] + 1;
            int ay = yIndex[a.y] + 1;
            int bx = xIndex[b.x] + 1;
            int by = yIndex[b.y] + 1;

            if (ax == bx)
            {
                int y1 = Math.Min(ay, by);
                int y2 = Math.Max(ay, by);

                for (int gy = y1; gy <= y2; gy++)
                {
                    wall[ax, gy] = true;
                }
            }
            else
            {
                int x1 = Math.Min(ax, bx);
                int x2 = Math.Max(ax, bx);

                for (int gx = x1; gx <= x2; gx++)
                {
                    wall[gx, ay] = true;
                }             
            }
        }


        bool[,] outside = new bool[width, height];
        var q = new Queue<(int x, int y)>();

        q.Enqueue((0,0));
        outside[0,0] = true;

        int[] dx = { 1, -1, 0, 0};
        int[] dy = { 0, 0, 1, -1};

        while (q.Count > 0)
        {
            var cell = q.Dequeue();

            for (int d = 0; d < 4; d++)
            {
                int nx = cell.x + dx[d];
                int ny = cell.y + dy[d];

                if (nx < 0 || nx >= width || ny < 0 || ny >= height)
                {
                    continue;
                }

                if (outside[nx, ny])
                {
                    continue;
                }

                if (wall[nx, ny])
                {
                    continue;
                }

                outside[nx, ny] = true;
                q.Enqueue((nx, ny));
            }
        }

        bool[,] allowed = new bool[width, height];

        for (int gx = 0; gx < width; gx++)
        {
            for (int gy = 0; gy < height; gy++)
            {
                if (!outside[gx,gy])
                {
                    allowed[gx,gy] = true;
                }
            }
        }

        long maxArea = 0;

        for (int i = 0; i < redTileCells.Count; i++)
        {
            var p1 = points[i];
            var c1 = redTileCells[i];
            
            for (int j = i + 1; j < redTileCells.Count; j++)
            {
                var p2 = points[j];
                var c2 = redTileCells[j];

                long dxOrig = Math.Abs((long)p1.x - p2.x);
                long dyOrig = Math.Abs((long)p1.y - p2.y);
                long area = (dxOrig + 1) * (dyOrig + 1);

                if (area < maxArea)
                {
                    continue;
                }

                int x1 = Math.Min(c1.offsetX, c2.offsetX);
                int x2 = Math.Max(c1.offsetX, c2.offsetX);
                int y1 = Math.Min(c1.offsetY, c2.offsetY);
                int y2 = Math.Max(c1.offsetY, c2.offsetY);

                bool ok = true;

                for (long gx = x1; gx <= x2 && ok; gx++)
                {
                    if (!allowed[gx, y1] || !allowed[gx, y2])
                    {
                        ok = false;
                    }
                }

                for (long gy = y1; gy <= y2 && ok; gy++)
                {
                    if (!allowed[x1, gy] || !allowed[x2, gy])
                    {
                        ok = false;
                    }
                }

                if (ok)
                {
                    maxArea = area;
                }
            }
        }

        Console.WriteLine($"Day 9 Part 2: {maxArea}");
    }
}