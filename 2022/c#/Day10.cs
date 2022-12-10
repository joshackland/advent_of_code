using System.Linq;

var inputFile = File.ReadAllLines("../input/input_10.txt");
var input = new List<string>(inputFile);

int sumSignalStrength(int cycles, int registerX, string instruction){
    if ((cycles - 20) % 40 == 0){
        Console.WriteLine(cycles.ToString() + " " + instruction + " " + (registerX * cycles).ToString());
        return registerX * cycles;
    }
    return 0;
}

void Part1()
{
    int cycles = 0;
    int registerX = 1;
    int signalSum = 0;

    foreach(var instruction in input){

        if (instruction == "noop"){
            cycles++;
            signalSum += sumSignalStrength(cycles, registerX, instruction);
            continue;
        }

        for (int i = 0; i < 2; i++){
            cycles++;
            signalSum += sumSignalStrength(cycles, registerX, instruction);
            if (i == 1){
                registerX += Convert.ToInt32(instruction.Split(" ")[1]);
            }
        }
    }

    Console.WriteLine(signalSum);
}

int[] GetSpritePosition(int registerX, int cycles){
    registerX += (cycles/40) * 40;
    return new int[]{
        registerX,
        registerX+1,
        registerX+2
    };
}

string[,] DrawCRT(int cycles, string[,] crtScreen, int registerX){
    var character = GetSpritePosition(registerX, cycles).Contains(cycles) ? "#" : ".";

    Console.WriteLine($"{cycles}: {registerX} - [{(cycles/40).ToString()}] - [{(cycles%40).ToString()}]");

    crtScreen[cycles/40,cycles%40] = character;
    Console.WriteLine(crtScreen[cycles/40,cycles%40]);

    return crtScreen;
}

void Part2()
{
    int cycles = 0;

    int registerX = 0;
    
    var crtScreen = new string[6,40];

    foreach(var instruction in input){
        if (cycles == (6 * 40) - 1) break;

        if (instruction == "noop"){
            crtScreen = DrawCRT(cycles, crtScreen, registerX);
            cycles++;
            continue;
        }

        for (int i = 0; i < 2; i++){
            crtScreen = DrawCRT(cycles, crtScreen, registerX);
            cycles++;
            if (i == 1){
                registerX += Convert.ToInt32(instruction.Split(" ")[1]);
            }
        }
    }

    for (int row = 0; row < 6; row++){
        string rowBuild = "";
        for (int col = 0; col < 40; col++){
            rowBuild += crtScreen[row,col];
        }
        Console.WriteLine(rowBuild);
    }
}

Part2();