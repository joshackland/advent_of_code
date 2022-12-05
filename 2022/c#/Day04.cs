var inputFile = File.ReadAllLines("../input/input_04.txt");
var input = new List<string>(inputFile);

 
void Part1(){
    int count = 0;
    foreach (var line in input){
        var assignements = line.Split(",");

        var elfOneNums = assignements[0].Split("-");
        var elfTwoNums = assignements[1].Split("-");

        //if elf two does all of elf one's
        if (Int32.Parse(elfTwoNums[0]) <= Int32.Parse(elfOneNums[0]) &&
            Int32.Parse(elfTwoNums[1]) >= Int32.Parse(elfOneNums[1]) ||
        //if elf one does all of elf two's
            Int32.Parse(elfTwoNums[0]) >= Int32.Parse(elfOneNums[0]) &&
            Int32.Parse(elfTwoNums[1]) <= Int32.Parse(elfOneNums[1])){
            count++;
        }
    }

    Console.WriteLine(count.ToString());
}

void Part2(){
    int count = 0;
    foreach (var line in input){
        var assignements = line.Split(",");

        var elfOneNums = assignements[0].Split("-");
        var elfTwoNums = assignements[1].Split("-");

        if (Int32.Parse(elfOneNums[0]) <= Int32.Parse(elfTwoNums[1]) &&
            Int32.Parse(elfTwoNums[0]) <= Int32.Parse(elfOneNums[1])){
            count++;
        }
    }

    Console.WriteLine(count.ToString());

}

Part2();