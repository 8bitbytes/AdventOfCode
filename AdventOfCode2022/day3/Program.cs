static class Program
{
    static void Main()
    {
        var round1 = CaclulateRound1();
        var round2 = CalculateRound2();

        Console.WriteLine($"done 1: {round1}");
        Console.WriteLine($"done 2: {round2}");
        Console.ReadLine();
    }

    static int CalculateRound2()
    {
        var lines = File.ReadAllLines("input.txt");
        var total = 0;
        for (int index = 0; index < lines.Length - 1; index+=3){
            var list1 = new List<string>(lines[index].Select(c => c.ToString()));
            var list2 = new List<string>(lines[index +1].Select(c => c.ToString()));
            var list3 = new List<string>(lines[index + 2].Select(c => c.ToString()));
            var common = list1.Intersect(list2).ToList();
            common = list3.Intersect(common).ToList();

            foreach (var item in common)
            {
                if (item != null)
                {
                    var val = CalculatePriority(item);
                    total += val;
                    Console.WriteLine($"{item}: {val}");
                }
            }
        }

        return total;
    }

    static int CaclulateRound1()
    {
        var total = 0;

        foreach (var line in File.ReadAllLines("input.txt"))
        {
            var len = line.Length / 2;
            var list1 = new List<string>(line.Substring(0, len).Select(c => c.ToString()));
            var list2 = new List<string>(line.Substring(len).Select(c => c.ToString()));
            var common = list1.Intersect(list2).ToList();
            foreach (var item in common)
            {
                if (item != null)
                {
                    var val = CalculatePriority(item);
                    total += val;
                    Console.WriteLine($"{item}: {val}");
                }
            }
        }
        return total;
    }
    static bool isUpper(string val)
    {
        return val.ToUpper() == val;
    }

    static int CalculatePriority(string item)
    {
        var val = (int)item.ToCharArray()[0] % 32;
        if (isUpper(item))
        {
            val += 26;
        }
        return val;
    }
}
