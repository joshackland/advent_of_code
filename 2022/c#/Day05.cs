var inputFile = File.ReadAllLines("../input/input_05.txt");
var input = new List<string>(inputFile);

/*
[T]     [D]         [L]            
[R]     [S] [G]     [P]         [H]
[G]     [H] [W]     [R] [L]     [P]
[W]     [G] [F] [H] [S] [M]     [L]
[Q]     [V] [B] [J] [H] [N] [R] [N]
[M] [R] [R] [P] [M] [T] [H] [Q] [C]
[F] [F] [Z] [H] [S] [Z] [T] [D] [S]
[P] [H] [P] [Q] [P] [M] [P] [F] [D]
 1   2   3   4   5   6   7   8   9 
*/


var stacks = new List<string>[9]
{
    new List<string>(){"P","F","M","Q","W","G","R","T"},
    new List<string>(){"H","F","R"},
    new List<string>(){"P","Z","R","V","G","H","S","D"},
    new List<string>(){"Q","H","P","B","F","W","G"},
    new List<string>(){"P","S","M","J","H"},
    new List<string>(){"M","Z","T","H","S","R","P","L"},
    new List<string>(){"P","T","H","N","M","L"},
    new List<string>(){"F","D","Q","R"},
    new List<string>(){"D","S","C","N","L","P","H"},
};

//move 3 from 8 to 9
void Part1(){
    foreach (var line in input){
        var vals = line.Split(" ");

        int move = Int32.Parse(vals[0]);
        int from = Int32.Parse(vals[1]) - 1;
        int to = Int32.Parse(vals[2]) - 1;

        var currentStack = stacks[from];

        for (int i = 1; i <= move; i++){
            var crate = currentStack[currentStack.Count - 1];
            stacks[to].Add(crate);
            currentStack.RemoveAt(currentStack.Count - 1);
        }
    }

    string output = "";

    foreach(var stack in stacks){
        output += stack[stack.Count - 1];
    }

    Console.WriteLine(output);
}

void Part2(){
    foreach (var line in input){
        var vals = line.Split(" ");

        int move = Int32.Parse(vals[0]);
        int from = Int32.Parse(vals[1]) - 1;
        int to = Int32.Parse(vals[2]) - 1;

        var currentStack = stacks[from];

        var newStack = new List<string>();

        for (int i = 1; i <= move; i++){
            var crate = currentStack[currentStack.Count - 1];
            newStack.Add(crate);
            currentStack.RemoveAt(currentStack.Count - 1);
        }

        newStack.Reverse();

        foreach (var crate in newStack){
            stacks[to].Add(crate);
        }
    }

    string output = "";

    foreach(var stack in stacks){
        output += stack[stack.Count - 1];
    }

    Console.WriteLine(output);

}

Part2();