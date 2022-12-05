static class Program
{
    static void Main()
    {
        Solution1();
        Solution2();
    }

    static void Solution1()
    {
        var count = (from line in File.ReadAllLines("input.txt") select line.Split(",") into lineData let ld1 = lineData[0].Split("-").Select(int.Parse)?.ToList() let ld2 = lineData[1].Split("-").Select(int.Parse)?.ToList() where HasFullOverlap(ld1, ld2) select ld1).Count();
        Console.WriteLine(count);
    }

    static void Solution2()
    {
        var count = (from line in File.ReadAllLines("input.txt") select line.Split(",") into lineData let ld1 = lineData[0].Split("-").Select(int.Parse)?.ToList() let ld2 = lineData[1].Split("-").Select(int.Parse)?.ToList() where HasPartialOverlap(ld1, ld2) select ld1).Count();
        Console.WriteLine(count);
    }

    static bool HasPartialOverlap(List<int> range1, List<int> range2)
    {
        var allList1 = new List<int>();
        var allList2 = new List<int>();

        for(var idx=range1[0]; idx<= range1[1];idx++)
        {
            allList1.Add(idx);
        }

        for(var idx=range2[0]; idx<= range2[1]; idx++)
        {
            allList2.Add(idx);
        }

        return allList1.Count > allList2.Count ? allList2.Intersect(allList1).Any() : allList1.Intersect(allList2).Any();
    }

    static bool HasFullOverlap(List<int> range1, List<int> range2)
    {
        var allList1 = new List<int>();
        var allList2 = new List<int>();

        for (var idx = range1[0]; idx <= range1[1]; idx++)
        {
            allList1.Add(idx);
        }

        for (var idx = range2[0]; idx <= range2[1]; idx++)
        {
            allList2.Add(idx);
        }

        if(allList1.Count > allList2.Count)
        {
            return allList2.Intersect(allList1).Count() == allList2.Count;
        }
        else
        {
            return allList1.Intersect(allList2).Count() == allList1.Count;
        }
    }
}