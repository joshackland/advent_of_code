namespace AoC2025;

class Day12
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day12.txt"));

    public void Part1()
    {
        var blocks = new List<List<string>>();
        var cur = new List<string>();

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (cur.Count > 0)
                {
                    blocks.Add(cur);
                    cur = new List<string>();
                }
            }
            else
            {
                cur.Add(line);
            }
        }
        if (cur.Count > 0)
            blocks.Add(cur);

        int shapeBlockCount = blocks.Count - 1;

        var shapeTransforms = new List<List<(int[] xs, int[] ys, int w, int h)>>();

        // Rotate 90° clockwise
        static bool[,] Rot90(bool[,] g)
        {
            int n = g.GetLength(0);
            var r = new bool[n, n];
            for (int y = 0; y < n; y++)
                for (int x = 0; x < n; x++)
                    r[n - 1 - y, x] = g[x, y];
            return r;
        }

        static bool[,] FlipX(bool[,] g)
        {
            int n = g.GetLength(0);
            var r = new bool[n, n];
            for (int y = 0; y < n; y++)
                for (int x = 0; x < n; x++)
                    r[n - 1 - x, y] = g[x, y];
            return r;
        }

        static string Key(bool[,] g)
        {
            int n = g.GetLength(0);
            var chars = new char[n * n];
            int k = 0;
            for (int y = 0; y < n; y++)
                for (int x = 0; x < n; x++)
                    chars[k++] = g[x, y] ? '#' : '.';
            return new string(chars);
        }

        for (int b = 0; b < shapeBlockCount; b++)
        {
            var block = blocks[b];
            int shapeIndex = int.Parse(block[0].TrimEnd(':'));

            var baseGrid = new bool[3, 3];
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                    baseGrid[x, y] = block[1 + y][x] == '#';

            var seen = new HashSet<string>();
            var transforms = new List<(int[] xs, int[] ys, int w, int h)>();

            foreach (bool flip in new[] { false, true })
            {
                bool[,] g = flip ? FlipX(baseGrid) : baseGrid;

                for (int r = 0; r < 4; r++)
                {
                    if (r > 0) g = Rot90(g);

                    string key = Key(g);
                    if (!seen.Add(key)) continue;

                    var pts = new List<(int x, int y)>();
                    for (int yy = 0; yy < 3; yy++)
                        for (int xx = 0; xx < 3; xx++)
                            if (g[xx, yy]) pts.Add((xx, yy));

                    int minX = pts.Min(p => p.x);
                    int minY = pts.Min(p => p.y);
                    for (int i = 0; i < pts.Count; i++)
                        pts[i] = (pts[i].x - minX, pts[i].y - minY);

                    int w = pts.Max(p => p.x) + 1;
                    int h = pts.Max(p => p.y) + 1;

                    transforms.Add((
                        pts.Select(p => p.x).ToArray(),
                        pts.Select(p => p.y).ToArray(),
                        w,
                        h
                    ));
                }
            }

            while (shapeTransforms.Count <= shapeIndex)
                shapeTransforms.Add(new());

            shapeTransforms[shapeIndex] = transforms;
        }

        int answer = 0;

        foreach (var line in blocks[^1])
        {
            var parts = line.Split(':', 2);
            var dims = parts[0].Split('x');

            int W = int.Parse(dims[0]);
            int H = int.Parse(dims[1]);

            int[] need = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int totalFilled = 0;
            for (int i = 0; i < need.Length; i++)
                totalFilled += need[i] * shapeTransforms[i][0].xs.Length;

            if (totalFilled > W * H)
                continue;

            var rows = new ulong[H];

            var placementsByShape = new List<(int y, ulong[] masks)>[need.Length];
            for (int s = 0; s < need.Length; s++)
            {
                placementsByShape[s] = new();

                foreach (var tr in shapeTransforms[s])
                {
                    for (int oy = 0; oy <= H - tr.h; oy++)
                        for (int ox = 0; ox <= W - tr.w; ox++)
                        {
                            var masks = new ulong[tr.h];
                            for (int i = 0; i < tr.xs.Length; i++)
                                masks[tr.ys[i]] |= 1UL << (tr.xs[i] + ox);

                            placementsByShape[s].Add((oy, masks));
                        }
                }
            }

            var memo = new Dictionary<string, bool>();

            bool Fits(int y, ulong[] m)
            {
                for (int i = 0; i < m.Length; i++)
                    if ((rows[y + i] & m[i]) != 0) return false;
                return true;
            }

            void Apply(int y, ulong[] m)
            {
                for (int i = 0; i < m.Length; i++)
                    rows[y + i] |= m[i];
            }

            void Undo(int y, ulong[] m)
            {
                for (int i = 0; i < m.Length; i++)
                    rows[y + i] ^= m[i];
            }

            bool Solve()
            {
                string key =
                    string.Join(",", rows) + "|" +
                    string.Join(",", need);

                if (memo.TryGetValue(key, out bool v))
                    return v;

                bool anyLeft = false;
                for (int i = 0; i < need.Length; i++)
                    if (need[i] > 0) { anyLeft = true; break; }

                if (!anyLeft)
                    return memo[key] = true;

                int chosen = -1, best = int.MaxValue;
                for (int s = 0; s < need.Length; s++)
                    if (need[s] > 0 && placementsByShape[s].Count < best)
                    {
                        best = placementsByShape[s].Count;
                        chosen = s;
                    }

                foreach (var (y, m) in placementsByShape[chosen])
                {
                    if (!Fits(y, m)) continue;

                    need[chosen]--;
                    Apply(y, m);

                    if (Solve())
                    {
                        Undo(y, m);
                        need[chosen]++;
                        return memo[key] = true;
                    }

                    Undo(y, m);
                    need[chosen]++;
                }

                return memo[key] = false;
            }

            if (Solve())
                answer++;
        }

        Console.WriteLine($"Day 12 Part 1: {answer}");
    }

    public void Part2()
    {
        Console.WriteLine($"Day 12 Part 2: ");
    }
}
