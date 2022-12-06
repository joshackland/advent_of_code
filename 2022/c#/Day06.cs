var inputFile = File.ReadAllLines("../input/input_06.txt");
var input = new List<string>(inputFile);


void Part1(){
    bool markerFound = false;

    foreach(var line in input){
        for(int i = 0; i < line.Length - 4; i++){
            var characters = new List<char>();
            for (int j = i; j < i+4; j++){
                if(!characters.Contains(line[j])){
                    characters.Add(line[j]);

                    if (characters.Count == 4){
                        markerFound = true;
                        break;
                    }
                }
                else{
                    break;
                }
            }

            if(markerFound){
                Console.WriteLine((i+4).ToString());
                Console.WriteLine(line.Substring(i,4));
                break;
            }
        }
    }
}

void Part2(){
    bool messageFound = false;

    foreach(var line in input){
        for(int i = 0; i < line.Length - 14; i++){
            var characters = new List<char>();
            for (int j = i; j < i+14; j++){
                if(!characters.Contains(line[j])){
                    characters.Add(line[j]);

                    if (characters.Count == 14){
                        messageFound = true;
                        break;
                    }
                }
                else{
                    break;
                }
            }

            if(messageFound){
                Console.WriteLine((i+14).ToString());
                Console.WriteLine(line.Substring(i,14));
                break;
            }
        }
    }

}

Part2();