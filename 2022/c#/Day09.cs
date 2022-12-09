var inputFile = File.ReadAllLines("../input/input_09.txt");
var input = new List<string>(inputFile);

var testInput = new List<string>(){
"R 5",
"U 8",
"L 8",
"D 3",
"R 17",
"D 10",
"L 25",
"U 20"
};

void Part1(){
    int xHead = 0;
    int yHead = 0;

    int xTail = 0;
    int yTail = 0;

    var positions = new HashSet<string>();
    positions.Add(position(xTail,yTail));

    foreach (var line in testInput){
        var instruction = line.Split(" ");

        for (int i = Convert.ToInt32(instruction[1]); i > 0; i--){
            int prevXHead = xHead;
            int prevYHead = yHead;

            if (instruction[0] == "U"){
                yHead++;
            }
            else if (instruction[0] == "D"){
                yHead--;
            }
            else if (instruction[0] == "L"){
                xHead--;
            }
            else if (instruction[0] == "R"){
                xHead++;
            }

            if (xHead == xTail && (yHead == yTail || Math.Abs(yHead - yTail) == 1) || yHead == yTail && Math.Abs(xHead - xTail) == 1) continue;
            if (Math.Abs(yHead - yTail) == 1 && Math.Abs(xHead - xTail) == 1) continue;

            xTail = prevXHead;
            yTail = prevYHead;
            
            positions.Add(position(xTail,yTail));
        }        
    }

    Console.WriteLine(positions.Count);
}

void Part2(){
    int xHead = 0;
    int yHead = 0;

    var xTails = new int[9];
    var yTails =  new int[9];

    var positions = new HashSet<string>();
    positions.Add(position(xHead,yHead));

    foreach (var line in input){
        var instruction = line.Split(" ");

        for (int i = Convert.ToInt32(instruction[1]); i > 0; i--){
            int prevX = xHead;
            int prevY = yHead;

            if (instruction[0] == "U"){
                yHead++;
            }
            else if (instruction[0] == "D"){
                yHead--;
            }
            else if (instruction[0] == "L"){
                xHead--;
            }
            else if (instruction[0] == "R"){
                xHead++;
            }

            for (int tail = 0; tail < 9; tail++){
                int xTail = xTails[tail];
                int yTail = yTails[tail];

                int xCompare = tail != 0 ? xTails[tail-1] : xHead;
                int yCompare = tail != 0 ? yTails[tail-1] : yHead;

                if (xCompare == xTail && (yCompare == yTail || Math.Abs(yCompare - yTail) == 1) || yCompare == yTail && Math.Abs(xCompare - xTail) == 1) continue;
                if (Math.Abs(yCompare - yTail) == 1 && Math.Abs(xCompare - xTail) == 1) continue;

                if (xCompare == xTail){
                    yTails[tail] += yCompare > yTail ? 1 : -1;
                }
                else if (yCompare == yTail){
                    xTails[tail] += xCompare > xTail ? 1 : -1;
                }                
                else {
                    yTails[tail] += yCompare > yTail ? 1 : -1;
                    xTails[tail] += xCompare > xTail ? 1 : -1;
                }
                
                    
                if (tail == 8){
                    positions.Add(position(xTails[tail],yTails[tail]));
                }
            }
        }        
    }

    Console.WriteLine(positions.Count);
}

string position(int x, int y){
    return $"{x.ToString()},{y.ToString()}";
}

Part2();