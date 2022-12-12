var inputFile = File.ReadAllLines("../input/input_11.txt");
var input = new List<string>(inputFile);

struct Monkey
{
    public Monkey(long[] items, int num){
        Items = new List<long>();
        MonkeyNum = num;
        foreach(long item in items){
            Items.Add(item);
        }
    }
    public List<long> Items;
    public int InspectCount = 0;
    public int MonkeyNum;
    public int TrueMonkey;
    public int FalseMonkey;
}

void Part1()
{
    var Monkeys = new Monkey[]{
        
        new Monkey(new long[]{79L, 98L}, 0),
        new Monkey(new long[]{54L, 65L, 75L, 74L}, 1),
        new Monkey(new long[]{79L, 60L, 97L}, 2),
        new Monkey(new long[]{74L}, 3),
        /*
        new Monkey(new long[]{57}, 0),
        new Monkey(new long[]{ 58, 93, 88, 81, 72, 73, 65}, 1),
        new Monkey(new long[]{65, 95}, 2),
        new Monkey(new long[]{ 58, 80, 81, 83}, 3),
        
        new Monkey(new long[]{58, 89, 90, 96, 55}, 4),
        new Monkey(new long[]{66, 73, 87, 58, 62, 67}, 5),
        new Monkey(new long[]{85, 55, 89}, 6),
        new Monkey(new long[]{73, 80, 54, 94, 90, 52, 69, 58}, 7)*/
    };
    
    Monkeys[0].TrueMonkey = 2;
    Monkeys[0].FalseMonkey = 3;

    Monkeys[1].TrueMonkey = 2;
    Monkeys[1].FalseMonkey = 0;

    Monkeys[2].TrueMonkey = 1;
    Monkeys[2].FalseMonkey = 3;

    Monkeys[3].TrueMonkey = 0;
    Monkeys[3].FalseMonkey = 1;
/*
    Monkeys[0].TrueMonkey = 3;
    Monkeys[0].FalseMonkey = 2;

    Monkeys[1].TrueMonkey = 6;
    Monkeys[1].FalseMonkey = 7;

    Monkeys[2].TrueMonkey = 3;
    Monkeys[2].FalseMonkey = 5;

    Monkeys[3].TrueMonkey = 4;
    Monkeys[3].FalseMonkey = 5;

    Monkeys[4].TrueMonkey = 1;
    Monkeys[4].FalseMonkey = 7;

    Monkeys[5].TrueMonkey = 4;
    Monkeys[5].FalseMonkey = 1;

    Monkeys[6].TrueMonkey = 2;
    Monkeys[6].FalseMonkey = 0;

    Monkeys[7].TrueMonkey = 6;
    Monkeys[7].FalseMonkey = 0;
*/
    for (int round = 1; round <= 20; round++){
        for (int i = 0; i < Monkeys.Length; i++){
            long worry = 0;
            bool divisible = false;

            Monkeys[i].InspectCount += Monkeys[i].Items.Count;

            foreach (var item in Monkeys[i].Items){
                if (i == 0){
                    worry = (item * 19L) / 3L;
                    divisible = worry % 23L == 0L;
                    /*worry = (item * 13) / 3;
                    divisible = worry % 11 == 0;*/
                }
                else if (i == 1){
                    worry = (item + 6L) / 3L;
                    divisible = worry % 19L == 0L;
                    /*worry = (item + 2) / 3;
                    divisible = worry % 7 == 0;*/
                }
                else if (i == 2){
                    worry = (item * item) / 3L;
                    //worry = (item + 6) / 3;
                    divisible = worry % 13L == 0L;
                }
                else if (i == 3){
                    worry = (item + 3L) / 3L;
                    divisible = worry % 17L == 0L;
                    /*worry = (item * item) / 3;
                    divisible = worry % 5 == 0;*/
                }
                else if (i == 4){
                    worry = (item + 3) / 3;
                    divisible = worry % 3 == 0;
                }
                else if (i == 5){
                    worry = (item * 7) / 3;
                    divisible = worry % 17 == 0;
                }
                else if (i == 6){
                    worry = (item + 4) / 3;
                    divisible = worry % 2 == 0;
                }
                else if (i == 7){
                    worry = (item + 7) / 3;
                    divisible = worry % 19 == 0;
                }

                if (divisible){
                    Monkeys[Monkeys[i].TrueMonkey].Items.Add(worry);
                }
                else {
                    Monkeys[Monkeys[i].FalseMonkey].Items.Add(worry);
                }
                //Console.WriteLine($"Monkey {Monkeys[i].MonkeyNum}: {item}, {worry}, {divisible} ");
            }

            Monkeys[i].Items = new List<long>();
        }
    }

    var highestCounts = new int[2];

    foreach(var currentMonkey in Monkeys){
        Console.WriteLine(currentMonkey.InspectCount);
        for (int count = 0; count < 2; count++)
        {
            if (highestCounts[count] < currentMonkey.InspectCount){
                if (count == 0){
                    highestCounts[1] = highestCounts[0];
                }
                highestCounts[count] = currentMonkey.InspectCount;
                break;
            }
        }
    }

    Console.WriteLine(highestCounts[0] * highestCounts[1]);
}


void Part2()
{    
    var Monkeys = new Monkey[]{     /*
        new Monkey(new long[]{79L, 98L}, 0),
        new Monkey(new long[]{54L, 65L, 75L, 74L}, 1),
        new Monkey(new long[]{79L, 60L, 97L}, 2),
        new Monkey(new long[]{74L}, 3)*/
        
        new Monkey(new long[]{57}, 0),
        new Monkey(new long[]{58, 93, 88, 81, 72, 73, 65}, 1),
        new Monkey(new long[]{65, 95}, 2),
        new Monkey(new long[]{ 58, 80, 81, 83}, 3),
        
        new Monkey(new long[]{58, 89, 90, 96, 55}, 4),
        new Monkey(new long[]{66, 73, 87, 58, 62, 67}, 5),
        new Monkey(new long[]{85, 55, 89}, 6),
        new Monkey(new long[]{73, 80, 54, 94, 90, 52, 69, 58}, 7)
    };
    /*
    Monkeys[0].TrueMonkey = 2;
    Monkeys[0].FalseMonkey = 3;

    Monkeys[1].TrueMonkey = 2;
    Monkeys[1].FalseMonkey = 0;

    Monkeys[2].TrueMonkey = 1;
    Monkeys[2].FalseMonkey = 3;

    Monkeys[3].TrueMonkey = 0;
    Monkeys[3].FalseMonkey = 1;*/

    Monkeys[0].TrueMonkey = 3;
    Monkeys[0].FalseMonkey = 2;

    Monkeys[1].TrueMonkey = 6;
    Monkeys[1].FalseMonkey = 7;

    Monkeys[2].TrueMonkey = 3;
    Monkeys[2].FalseMonkey = 5;

    Monkeys[3].TrueMonkey = 4;
    Monkeys[3].FalseMonkey = 5;

    Monkeys[4].TrueMonkey = 1;
    Monkeys[4].FalseMonkey = 7;

    Monkeys[5].TrueMonkey = 4;
    Monkeys[5].FalseMonkey = 1;

    Monkeys[6].TrueMonkey = 2;
    Monkeys[6].FalseMonkey = 0;

    Monkeys[7].TrueMonkey = 6;
    Monkeys[7].FalseMonkey = 0;

    //long m = 23 * 19 * 13 * 17;
    long m = 11 * 7 * 13 * 5 * 3 * 17 * 2 * 19;

    for (int round = 1; round <= 10000; round++){
        for (int i = 0; i < Monkeys.Length; i++){
            long worry = 0;
            bool divisible = false;

            Monkeys[i].InspectCount += Monkeys[i].Items.Count;

            foreach (var item in Monkeys[i].Items){
                if (i == 0){/*
                    worry = (item * 19) % m;
                    divisible = worry % 23L == 0L;*/
                    worry = (item * 13L) % m;
                    divisible = worry % 11L == 0L;
                }
                else if (i == 1){/*
                    worry = (item + 6L) % m;
                    divisible = worry % 19L == 0L;*/
                    worry = (item + 2L) % m;
                    divisible = worry % 7L == 0L;
                }
                else if (i == 2){/*
                    worry = (item * item) % m;*/
                    worry = (item + 6L) % m;
                    divisible = worry % 13 == 0L;
                }
                else if (i == 3){/*
                    worry = (item + 3L) % m;
                    divisible = worry % 17L == 0L;*/
                    worry = (item * item) % m;
                    divisible = worry % 5L == 0L;
                }
                else if (i == 4){
                    worry = (item + 3L) % m;
                    divisible = worry % 3L == 0L;
                }
                else if (i == 5){
                    worry = (item * 7L) % m;
                    divisible = worry % 17L == 0L;
                }
                else if (i == 6){
                    worry = (item + 4L) % m;
                    divisible = worry % 2L == 0L;
                }
                else if (i == 7){
                    worry = (item + 7L) % m;
                    divisible = worry % 19L == 0L;
                }

                if (divisible){
                    Monkeys[Monkeys[i].TrueMonkey].Items.Add(worry);
                }
                else {
                    Monkeys[Monkeys[i].FalseMonkey].Items.Add(worry);
                }
            }
            Monkeys[i].Items = new List<long>();
        }
    }

    var highestCounts = new int[2];

    foreach(var currentMonkey in Monkeys){
        Console.WriteLine(currentMonkey.InspectCount);
        for (int count = 0; count < 2; count++)
        {
            if (highestCounts[count] < currentMonkey.InspectCount){
                if (count == 0){
                    highestCounts[1] = highestCounts[0];
                }
                highestCounts[count] = currentMonkey.InspectCount;
                break;
            }
        }
    }

    Console.WriteLine(Convert.ToDouble(highestCounts[0]) * Convert.ToDouble(highestCounts[1]));
}

Part2();