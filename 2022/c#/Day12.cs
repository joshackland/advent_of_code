using System.Linq;
 
var inputFile = File.ReadAllLines("../input/input_12.txt");
var input = new List<string>(inputFile);

//solution based on https://github.com/bradwilson/advent-2022/blob/main/day12/Program.cs

var start = (x: -1, y: -1);
var end = (x: -1, y: -1);

var map = new List<List<char>>();

var distanceCosts = new Dictionary<(int, int), int>();

void explorePaths(int x, int y){
    int currentCost = distanceCosts[(x,y)];

    void neighbourCost(int newX, int newY)
    {
        if (newX < 0 || newX >= map[0].Count) return;
        if (newY < 0 || newY >= map.Count) return;


        if (map[newY][newX] + 1 >= map[y][x])
        {
            if (!distanceCosts.ContainsKey((newX, newY)) || distanceCosts[(newX, newY)] > currentCost + 1)
            {
                distanceCosts[(newX, newY)] = currentCost + 1;
                explorePaths(newX, newY);
            }
        }
    }

    neighbourCost(x+1,y);
    neighbourCost(x-1,y);
    neighbourCost(x,y+1);
    neighbourCost(x,y-1);
}

foreach (var line in input){
    var charList = new List<char>();
    foreach (char c in line){
        if (c == 'S'){
            start = (charList.Count, map.Count);
            charList.Add('a');
        }
        else if (c == 'E'){
            end = (charList.Count, map.Count);
            distanceCosts[end] = 0;
            charList.Add('z');
        }
        else charList.Add(c);
        
    }
    map.Add(charList);
}
 
explorePaths(end.x, end.y);

void Part1(){
    Console.WriteLine(distanceCosts[(start.x, start.y)]);
}

void Part2(){
    var startinA = new List<(int,int)>();

    for(int row = 0; row < map.Count; row++){
        for (int coll = 0; coll < map[row].Count; coll++){
            if (map[row][coll] == 'a'){
                startinA.Add((coll,row));
            }
        }
    }

    int lowest = Int32.MaxValue;

    foreach (var coord in startinA){
        if (!distanceCosts.ContainsKey(coord)) continue;
        
        var count = distanceCosts[coord];
        if (count < lowest) lowest = count;
    }
    Console.WriteLine(lowest);
}

Part2();

