 var inputFile = File.ReadAllLines("./input/input_03.txt");
 var input = new List<string>(inputFile);

 
void Part1(){
    var characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    int priorities = 0;

    foreach(var line in input){
        var compartment1 = line.Substring(0,line.Length/2);
        var compartment2 = line.Substring(line.Length/2,line.Length/2);


        foreach (var c in compartment2){
            if (compartment1.Contains(c)){
                priorities += Array.IndexOf(characters,c) + 1;
                break;
            }
        }
    }

    Console.WriteLine(priorities.ToString());
}


void Part2(){
    var characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    int priorities = 0;

    int lineCount = 0;
    var lines = new string[3];

    foreach(var line in input){
        if (lineCount < 3){
            lines[lineCount] = line;
            lineCount++;
        }

        if (lineCount == 3){
            var charMatches = new List<char>();
            foreach (var c in lines[1]){
                if (lines[0].Contains(c)){
                    charMatches.Add(c);
                }
            }

            foreach (var c in lines[2]){
                if (charMatches.Contains(c)){
                    priorities += Array.IndexOf(characters,c) + 1;
                    break;
                }
            }

            lines = new string[3];
            lineCount = 0;
        }
    }

    Console.WriteLine(priorities.ToString());
}

Part2();