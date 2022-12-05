 var inputFile = File.ReadAllLines("../input/input_02.txt");
 var input = new List<string>(inputFile);

//A - Rock, B - Paper, C - Scissors
//X - Rock, Y - Paper, Z - Scissors
//1, 2 ,3


void Part1(){
    var inputScore = new Dictionary<string,int>(){
        {"X", 1},
        {"Y", 2},
        {"Z", 3},
    };

    int score = 0;

    foreach (var line in input){
        var lineChoices = line.Split(" ");
        var opponent = lineChoices[0];
        var mine = lineChoices[1];

        //draw
        if (opponent == "A" && mine == "X" ||
        opponent == "B" && mine == "Y" || 
        opponent == "C" && mine == "Z" ){
            score += 3 + inputScore[mine];
        }
        //win
        else if (opponent == "A" && mine == "Y" ||
        opponent == "B" && mine == "Z" || 
        opponent == "C" && mine == "X" ){
            score += 6 + inputScore[mine];
        }
        //loss
        else {
            score += inputScore[mine];
        }
    }

    Console.WriteLine(score.ToString());
}


//A - Rock, B - Paper, C - Scissors
//X - Loss, Y - Draw, Z - Win
//1, 2 ,3

void Part2(){
    var optionScore = new Dictionary<string,Dictionary<string,int>>(){
        {"A", new Dictionary<string,int>(){{"X",3},{"Y",1},{"Z",2}}},
        {"B", new Dictionary<string,int>(){{"X",1},{"Y",2},{"Z",3}}},
        {"C", new Dictionary<string,int>(){{"X",2},{"Y",3},{"Z",1}}}
    };

    var winScore = new Dictionary<string,int>(){
        {"X", 0},
        {"Y", 3},
        {"Z", 6},
    };

    int score = 0;

    foreach (var line in input){
        var lineChoices = line.Split(" ");
        var opponent = lineChoices[0];
        var mine = lineChoices[1];

        score += optionScore[opponent][mine] + winScore[mine];
    }

    Console.WriteLine(score.ToString());

}

Part2();