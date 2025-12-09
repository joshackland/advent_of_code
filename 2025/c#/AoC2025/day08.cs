using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

namespace AoC2025;

class Day08
{
    private List<string> input = new List<string>(File.ReadAllLines("../../input/day08.txt"));

    public struct Point3d
    {
        public int X;
        public int Y;
        public int Z;

        public Point3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public struct PointDistance
    {
        public double Distance;
        public int IndexP;
        public int IndexQ;

        public PointDistance(double distance, int indexP, int indexQ)
        {
            Distance = distance;
            IndexP = indexP;
            IndexQ = indexQ;
        }
    }


    public void Part1()
    {
        var positions = new List<Point3d>();

        foreach (var line in input)
        {
            var parts = line.Split(',');

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            int z = int.Parse(parts[2]);

            positions.Add(new(x,y,z));
        }

        var pointDistances = new List<PointDistance>();

        for (int i = 0; i < positions.Count - 1; i++)
        {
            var p = positions[i];
            for (int j = i + 1; j < positions.Count; j++)
            {
                var q = positions[j];

                double dx = p.X - q.X;
                double dy = p.Y - q.Y;
                double dz = p.Z - q.Z;

                double distance = Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));

                pointDistances.Add(new(distance, i, j));
            }
        }

        pointDistances.Sort((d1, d2) => d1.Distance.CompareTo(d2.Distance));


        // int totalPairs = 10;
        int totalPairs = 1000;

        var circuits = new List<HashSet<int>>();

        for (int i = 0; i < positions.Count; i++)
        {
            circuits.Add(new HashSet<int>{ i });
        }

        for (int i = 0; i < totalPairs; i++)
        {
            var pd = pointDistances[i];
            int pIndex = pd.IndexP;
            int qIndex = pd.IndexQ;

            HashSet<int>? circuitP = null;
            HashSet<int>? circuitQ = null;

            foreach(var circuit in circuits)
            {
                if (circuit.Contains(pIndex))
                {
                    circuitP = circuit;
                }

                if (circuit.Contains(qIndex))
                {
                    circuitQ = circuit;
                }

                if (circuitP is not null && circuitQ is not null)
                {
                    break;
                }
            }

            if (circuitP is not null && circuitQ is not null && circuitP != circuitQ)
            {
                circuitP.UnionWith(circuitQ);
                circuits.Remove(circuitQ);                
            }
        }

        var sizes = new List<int>();

        foreach(var circuit in circuits)
        {
            sizes.Add(circuit.Count);
        }

        sizes.Sort();
        sizes.Reverse();

        int output = 1;
        for (int i = 0; i < 3; i++)
        {
            output *= sizes[i];
        }


        Console.WriteLine($"Day 8 Part 1: {output}");
    }

    public void Part2()
    {
        var positions = new List<Point3d>();

        foreach (var line in input)
        {
            var parts = line.Split(',');

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            int z = int.Parse(parts[2]);

            positions.Add(new(x,y,z));
        }

        var pointDistances = new List<PointDistance>();

        for (int i = 0; i < positions.Count - 1; i++)
        {
            var p = positions[i];
            for (int j = i + 1; j < positions.Count; j++)
            {
                var q = positions[j];

                double dx = p.X - q.X;
                double dy = p.Y - q.Y;
                double dz = p.Z - q.Z;

                double distance = Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));

                pointDistances.Add(new(distance, i, j));
            }
        }

        pointDistances.Sort((d1, d2) => d1.Distance.CompareTo(d2.Distance));

        var circuits = new List<HashSet<int>>();

        for (int i = 0; i < positions.Count; i++)
        {
            circuits.Add(new HashSet<int>{ i });
        }

        var finalPointDistance = pointDistances[0];

        for (int i = 0; circuits.Count > 1; i++)
        {
            var pd = pointDistances[i];
            int pIndex = pd.IndexP;
            int qIndex = pd.IndexQ;

            HashSet<int>? circuitP = null;
            HashSet<int>? circuitQ = null;

            foreach(var circuit in circuits)
            {
                if (circuit.Contains(pIndex))
                {
                    circuitP = circuit;
                }

                if (circuit.Contains(qIndex))
                {
                    circuitQ = circuit;
                }

                if (circuitP is not null && circuitQ is not null)
                {
                    break;
                }
            }

            if (circuitP is not null && circuitQ is not null && circuitP != circuitQ)
            {
                circuitP.UnionWith(circuitQ);
                circuits.Remove(circuitQ);
                finalPointDistance = pd;         
            }
        }

        int finalPIndex = finalPointDistance.IndexP;
        int finalQIndex = finalPointDistance.IndexQ;

        Point3d finalP = positions[finalPIndex];
        Point3d finalQ = positions[finalQIndex];

        int output = finalP.X * finalQ.X;


        Console.WriteLine($"Day 8 Part 2: {output}");
    }
}