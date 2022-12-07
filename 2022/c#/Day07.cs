var inputFile = File.ReadAllLines("../input/input_07.txt");
var input = new List<string>(inputFile);


    var newInput = new List<string>(){
"$ cd /",
"$ ls",
"dir a",
"14848514 b.txt",
"8504156 c.dat",
"dir d",
"$ cd a",
"$ ls",
"dir e",
"29116 f",
"2557 g",
"62596 h.lst",
"$ cd e",
"$ ls",
"584 i",
"$ cd ..",
"$ cd ..",
"$ cd d",
"$ ls",
"4060174 j",
"8033020 d.log",
"5626152 d.ext",
"7214296 k"
    };

void Part1(){
    var currentDirectory = new List<string>();

    string createCurrentDirectory(){
        string dir = string.Join("/",currentDirectory);
        dir = dir.Replace("//", "/");
        return dir;
    }

    var directorySize = new Dictionary<string,int>();

    foreach (var line in input){
        if (line.Substring(0,1) == "$"){
            var command = line.Split(" ");

            if (command[1] == "cd"){
                if (command[2] == ".."){
                    currentDirectory.RemoveAt(currentDirectory.Count-1);
                }
                else {
                    currentDirectory.Add(command[2]);
                }
            }
        }
        else if (line.Substring(0,3) != "dir"){
            var file = line.Split(" ");
            if (!directorySize.ContainsKey(createCurrentDirectory())){
                directorySize[createCurrentDirectory()] = 0;
            }
            string dir = createCurrentDirectory();
            while (dir.Contains("/")){
                if (!directorySize.ContainsKey(dir)){
                    directorySize[dir] = 0;
                }
                directorySize[dir] += Convert.ToInt32(file[0]);

                if (dir == "/") break;

                var dirSplit = dir.Split("/").ToList();
                dirSplit.RemoveAt(dirSplit.Count-1);
                dir = string.Join("/",dirSplit);
            }
        }
    }

    int output = 0;

    foreach(var item in directorySize)
    {
        if (item.Value <= 100000){
            output += item.Value;
        }
    }

    Console.WriteLine(output.ToString());
}

void Part2(){
    var currentDirectory = new List<string>();

    string createCurrentDirectory(){
        string dir = string.Join("/",currentDirectory);
        dir = dir.Replace("//", "/");
        return dir;
    }

    var directorySize = new Dictionary<string,int>(){
        {"/",0}
    };

    foreach (var line in input){
        if (line.Substring(0,1) == "$"){
            var command = line.Split(" ");

            if (command[1] == "cd"){
                if (command[2] == ".."){
                    currentDirectory.RemoveAt(currentDirectory.Count-1);
                }
                else {
                    currentDirectory.Add(command[2]);
                }
            }
        }
        else if (line.Substring(0,3) != "dir"){
            var file = line.Split(" ");
            if (!directorySize.ContainsKey(createCurrentDirectory())){
                directorySize[createCurrentDirectory()] = 0;
            }
            string dir = createCurrentDirectory();
            directorySize["/"] += Convert.ToInt32(file[0]);
            while (dir.Contains("/")){
                if (!directorySize.ContainsKey(dir)){
                    directorySize[dir] = 0;
                }
                directorySize[dir] += Convert.ToInt32(file[0]);

                if (dir == "/") break;

                var dirSplit = dir.Split("/").ToList();
                dirSplit.RemoveAt(dirSplit.Count-1);
                dir = string.Join("/",dirSplit);
            }
        }
    }

    int output = 0;

    int maxSpace = 70000000;
    int minUnused = 30000000;
    int currentUnused = maxSpace - directorySize["/"];
    int requiredSpace =  minUnused - currentUnused;

    int smallestSize = Int32.MaxValue;

    foreach(var item in directorySize)
    {
        if (requiredSpace < item.Value && item.Value < smallestSize){
            smallestSize = item.Value;
        }
    }

    Console.WriteLine(smallestSize);
}

Part2();