 var inputFile = File.ReadAllLines("./input/input_01.txt");
 var input = new List<string>(inputFile);

/*
input = new List<string>(){
    "1000",
    "2000",
    "3000",
    "",
    "4000",
    "",
    "5000",
    "6000",
    "",
    "7000",
    "8000",
    "9000",
    "",
    "10000"
};
*/

void Part1(){
    int elfCalorie = 0;

    int currentCalorie = 0;

    foreach (var line in input){
        if (line == "")
        {
            if (currentCalorie > elfCalorie){
                elfCalorie = currentCalorie;
            }

            currentCalorie = 0;
        }
        else{
            currentCalorie += Int32.Parse(line);
        }
    }

    Console.WriteLine(elfCalorie);
}


void Part2(){
    int[] elfCalorie = new int[3];

    int currentCalorie = 0;

    foreach (var line in input){
        if (line == "")
        {
            for (int i = 2; i >= 0; i--){
                if (currentCalorie > elfCalorie[i]){
                    if (i != 2){
                        elfCalorie[i+1] = elfCalorie[i];
                    }
                    elfCalorie[i] = currentCalorie;
                }
            }

            currentCalorie = 0;
        }
        else{
            currentCalorie += Int32.Parse(line);
        }
    }

    int totalCalorie = 0;

    foreach (int num in elfCalorie){
        totalCalorie += num;
    }
    Console.WriteLine(totalCalorie.ToString());
}


Part2();