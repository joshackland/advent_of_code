var inputFile = File.ReadAllLines("../input/input_13_test.txt");
var input = new List<string>(inputFile);

var packetPairs = new List<string[]>();

{
    var pair = new string[2];

    foreach (var packet in input){

        if (packet == "") {
            packetPairs.Add(pair);
            pair = new string[2];
        }
        else if (pair[0] == null){
            pair[0] = packet;
        }
        else {
            pair[1] = packet;
        }
    }
    packetPairs.Add(pair);
}

List<object> CreatePacket (string packetString){
    //Console.WriteLine($"Creating packet: {packetString}");
    var packet = new List<object>();

    var currentPosition = new Dictionary<int,int>(){{0,0}};

    int levelsDeep = 0;

    List<object> CurrentList (){
        //Console.WriteLine($"Levels Deep : {levelsDeep}");
        if (levelsDeep == 0) return packet;

        var currentList = packet;

        for (int i = 1; i < levelsDeep - 1; i++){
            //Console.WriteLine($"i : {i}");
            //Console.WriteLine($"currentList : {currentList.Count}");
            // Console.WriteLine($"position : {currentPosition[i]}");
            currentList = (List<object>)currentList[currentPosition[i]];
        }

        return currentList;
    }
    
    string num = "";

    foreach (var c in packetString){
        //Console.WriteLine($"Current Char: {c}");
        //Console.Write(c);
        if (c == '['){
            //Console.WriteLine($"Levels Deep: {levelsDeep}");
            if (levelsDeep > 0){
                CurrentList().Add(new List<object>());
            }
            levelsDeep++;
            int tempDeep = levelsDeep;
            if (!currentPosition.ContainsKey(tempDeep))
            {
                currentPosition[tempDeep] = 0;
            }
            else 
            {
                while (currentPosition.ContainsKey(tempDeep)){
                    currentPosition[tempDeep] = 0;
                    tempDeep++;
                }
            }
            
        }
        else if (c == ']')
        {
            if (num != ""){
                foreach(KeyValuePair<int, int> pos in currentPosition)
                {
                    //Console.WriteLine($"{pos.Key} - {pos.Value}");
                }
                CurrentList().Add(Convert.ToInt32(num));
                num = "";
            }
            levelsDeep--;
        }
        else if (char.IsDigit(c)){
            //Console.WriteLine($"Adding {c.ToString()} to list");
            //Console.WriteLine($"Levels Deep: {levelsDeep}");
            //Console.WriteLine($"position : {currentPosition[levelsDeep]}");
            num += c.ToString();
        }
        else if (c == ',')
        {
            if (num != ""){
                foreach(KeyValuePair<int, int> pos in currentPosition)
                {
                    //Console.WriteLine($"{pos.Key} - {pos.Value}");
                }
                CurrentList().Add(Convert.ToInt32(num));
                num = "";
            }
            //Console.WriteLine($"Increase - {levelsDeep}");
            //Console.WriteLine($"Before - {currentPosition[levelsDeep]}");
            
            currentPosition[levelsDeep]++;
            //Console.WriteLine($"After - {currentPosition[levelsDeep]}");
        }
    }

    if (num != "") {
        CurrentList().Add(Convert.ToInt32(num));
        num = "";
    }
    //Console.WriteLine("Packet Created");

    return packet;
}

Dictionary<string,int> GetCurrentNum(List<object> objectList, int index, int round){
    var details = new Dictionary<string,int>(){
        {"Found",0},
        {"Index", index},
        {"CurrentNum", -1}
    };
    foreach (var obj in objectList){
        if (obj is int){
            if (index == round)
            {
                details["Found"] = 1;
                details["CurrentNum"] = (int)(obj);
                return details;
            }
            index++;
        }
        if (obj is List<object>)
        {
            details = GetCurrentNum((List<object>)obj, index, round);

            if (details["Found"] == 1) return details;
        }
    }
    return details;
}

void Part1()
{
    int total = 0;
    int pairIndex = 0;
    foreach (var pair in packetPairs){        
        pairIndex++;

        var packetLeft = CreatePacket(pair[0]);
        var packetRight = CreatePacket(pair[1]);
        int round = 0;

        while (true)
        {
            var leftRound = GetCurrentNum(packetLeft, 0, round);
            var rightRound = GetCurrentNum(packetRight, 0, round);

            //Console.WriteLine(details["Found"]);
            //Console.WriteLine(details["CurrentNum"]);
            //Console.WriteLine(details["Index"]);

            if (leftRound["Found"] == 1 && rightRound["Found"] == 1)
            {
                if (leftRound["CurrentNum"] < rightRound["CurrentNum"])
                {
                    total += pairIndex;
                    break;
                }
                if (leftRound["CurrentNum"] > rightRound["CurrentNum"]) break;
            }
            else {
                if (rightRound["Found"] == 1){
                    total += pairIndex;
                }
                break;
            }

            round++;
        }
    }

    Console.WriteLine(total);
}

void Part2()
{
}

Part1();