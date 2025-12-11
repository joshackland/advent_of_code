using Microsoft.Z3;

namespace AoC2025;

class Day10
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day10.txt"));


    public void Part1()
    {
        var lights = new List<bool[]>();
        var buttons = new List<List<int[]>>();
        var joltages = new List<int[]>();

        foreach (var line in input)
        {
            var linePart = line.Split(' ');

            var lineButtons = new List<int[]>();

            foreach (var part in linePart)
            {
                if (part.StartsWith('['))
                {
                    string lightPart = part.Substring(1, part.Length - 2);
                    var light = new bool[lightPart.Length];

                    for (int i = 0; i < lightPart.Length; i++)
                    {
                        light[i] = lightPart[i] == '#' ? true : false;
                    }
                    lights.Add(light);
                }
                else if  (part.StartsWith('('))
                {
                    string buttonPart = part.Substring(1, part.Length - 2);
                    var buttonParts = buttonPart.Split(',');

                    var buttonArr = new int[buttonParts.Length];
                    for (int i = 0; i < buttonParts.Length; i++)
                    {
                        buttonArr[i] = int.Parse(buttonParts[i]);
                    }
                    lineButtons.Add(buttonArr);
                }
                else
                {
                    string joltagePart = part.Substring(1, part.Length - 2);
                    var joltageArr = new int[joltagePart.Length];
                    var joltageParts = joltagePart.Split(',');

                    for (int i = 0; i < joltageParts.Length; i++)
                    {
                        joltageArr[i] = int.Parse(joltageParts[i].ToString());
                    }    
                    joltages.Add(joltageArr);
                }
            }
        
            buttons.Add(lineButtons);
        }

        int totalPresses = 0;

        for (int i = 0; i < lights.Count; i++)
        {
            var light = lights[i];
            var button = buttons[i];

            int targetMask = 0;
            for (int bit = 0; bit < light.Length; bit++)
            {
                if (light[bit])
                {
                    targetMask |= 1 << bit;
                }
            }

            var buttonMasks = new List<int>();

            foreach (var btn in button)
            {
                int mask = 0;
                foreach(int index in btn)
                {
                    mask |= 1 << index;
                }

                buttonMasks.Add(mask);
            }


            int maxStates = 1 << light.Length;
            int[] dist = new int[maxStates];

            for (int s = 0; s < maxStates; s++)
            {
                dist[s] = -1;
            }

            var queue = new Queue<int>();
            
            dist[0] = 0;
            queue.Enqueue(0);

            while (queue.Count > 0)
            {
                int state = queue.Dequeue();
                int currentDist = dist[state];

                if (state == targetMask)
                {
                    totalPresses += currentDist;
                    break;
                }

                foreach (int bm in buttonMasks)
                {
                    int nextState = state ^ bm;

                    if (dist[nextState] == -1)
                    {
                        dist[nextState] = currentDist + 1;
                        queue.Enqueue(nextState);
                    }
                }
            }
        }

        Console.WriteLine($"Day 10 Part 1: {totalPresses}");
    }

    public void Part2()
    {        
        var lights = new List<bool[]>();
        var buttons = new List<List<int[]>>();
        var joltages = new List<int[]>();

        foreach (var line in input)
        {
            var linePart = line.Split(' ');

            var lineButtons = new List<int[]>();

            foreach (var part in linePart)
            {
                if (part.StartsWith('['))
                {
                    string lightPart = part.Substring(1, part.Length - 2);
                    var light = new bool[lightPart.Length];

                    for (int i = 0; i < lightPart.Length; i++)
                    {
                        light[i] = lightPart[i] == '#' ? true : false;
                    }
                    lights.Add(light);
                }
                else if  (part.StartsWith('('))
                {
                    string buttonPart = part.Substring(1, part.Length - 2);
                    var buttonParts = buttonPart.Split(',');

                    var buttonArr = new int[buttonParts.Length];
                    for (int i = 0; i < buttonParts.Length; i++)
                    {
                        buttonArr[i] = int.Parse(buttonParts[i]);
                    }
                    lineButtons.Add(buttonArr);
                }
                else
                {
                    string joltagePart = part.Substring(1, part.Length - 2);
                    var joltageParts = joltagePart.Split(',');
                    var joltageArr = new int[joltageParts.Length];

                    for (int i = 0; i < joltageParts.Length; i++)
                    {
                        joltageArr[i] = int.Parse(joltageParts[i].ToString());
                    }    
                    joltages.Add(joltageArr);
                }
            }
        
            buttons.Add(lineButtons);
        }

        int totalPresses = 0;

        for (int i = 0; i < joltages.Count; i++)
        {
            int[] target = joltages[i];
            var joltageButtons = buttons[i];

            int counterCount = target.Length;
            int buttonCount = joltageButtons.Count;

            using (var ctx = new Context())
            {
                Optimize opt = ctx.MkOptimize();

                IntExpr[] x = new IntExpr[buttonCount];

                for (int b = 0; b < buttonCount; b++)
                {
                    x[b] = (IntExpr)ctx.MkIntConst($"x_{i}_{b}");
                    opt.Add(ctx.MkGe(x[b], ctx.MkInt(0)));
                }

                for (int r = 0; r < counterCount; r++)
                {
                    var terms = new List<ArithExpr>();

                    for (int b = 0; b < buttonCount; b++)
                    {
                        int[] button = joltageButtons[b];

                        bool affects = false;
                        for (int k = 0; k < button.Length; k++)
                        {
                            if (button[k] == r)
                            {
                                affects = true;
                                break;
                            }
                        }

                        if (affects)
                        {
                            terms.Add(x[b]);
                        }
                    }

                    ArithExpr lhs;
                    if (terms.Count == 0)
                    {
                        lhs = ctx.MkInt(0);
                    }
                    else if (terms.Count == 1)
                    {
                        lhs = terms[0];
                    }
                    else
                    {
                        lhs = ctx.MkAdd(terms.ToArray());
                    }

                    opt.Add(ctx.MkEq(lhs, ctx.MkInt(target[r])));
                }

                ArithExpr totalExpr;
                if (buttonCount == 1)
                {
                    totalExpr = x[0];
                }
                else
                {
                    totalExpr = ctx.MkAdd(x);
                }

                opt.MkMinimize(totalExpr);

                if (opt.Check() != Status.SATISFIABLE)
                {
                    Console.WriteLine("Failed :)");
                    return;
                }
                
                Model model = opt.Model;

                int bestForMachine = 0;
                for (int b = 0; b < buttonCount; b++)
                {
                    IntNum val = (IntNum)model.Evaluate(x[b], true);
                    bestForMachine += val.Int;
                }

                totalPresses += bestForMachine;
            }
        }

        Console.WriteLine($"Day 10 Part 2: {totalPresses}");
    }
}