
static class Program
{
    static void Main()
    {
        var totals = new List<int>();
        var currTotal = 0;
        foreach(var line in File.ReadAllLines("input.txt"))
        {
            if (line == String.Empty)
            {
                totals.Add(currTotal);
                currTotal = 0;
            }
            else
            {
                currTotal += Convert.ToInt32(line);
            }
        }
        totals.Sort();
        totals.Reverse();
        Console.WriteLine(totals[0]);
        Console.WriteLine($"{totals[0] + totals[1] + totals[2]}");
    }
}
