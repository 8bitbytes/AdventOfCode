static class Program
{
    static void Main()
    {
        Solution1();
        Solution2();
    }

    static void Solution1v1()
    {
        var charList = new List<char>();
        var foundAt = -1;
        foreach(var line in File.ReadAllLines("input.txt"))
        {
            for(var idx=0; idx< line.Length; idx++)
            {
                if(idx > 3)
                {
                    if (charList.Contains(line[idx]))
                    {
                        foundAt = idx;
                        break;
                    }
                }
                charList.Add(line[idx]);
            }
        }
        Console.WriteLine($"Found at {foundAt}");
    }

    static void Solution1()
    {
        var foundAt = -1;
        var charsProcessed = 0;
        foreach (var line in File.ReadAllLines("input.txt"))
        {
            for (var idx = 0; idx < line.Length; idx++)
            {
                charsProcessed++;
                if (idx > 3)
                {
                    var charArr = new List<char> { line[idx], line[idx - 1], line[idx - 2], line[idx - 3] };
                    if (charArr.Distinct().Count() == charArr.Count())
                    {
                        foundAt = charsProcessed;
                        break;
                    }
                }
            }
        }
        Console.WriteLine($"Found at {foundAt}");
    }

    static void Solution2()
    {
        var foundAt = -1;
        var charsProcessed = 0;
        foreach (var line in File.ReadAllLines("input.txt"))
        {
            for (var idx = 0; idx < line.Length; idx++)
            {
                charsProcessed++;
                if (idx > 13)
                {
                    var charArr = new List<char> { line[idx], line[idx - 1], line[idx - 2], line[idx - 3], line[idx - 4], line[idx - 5], line[idx - 6], line[idx - 7], line[idx - 8], line[idx - 9], line[idx - 10], line[idx - 11], line[idx - 12], line[idx - 13] };
                    if (charArr.Distinct().Count() == charArr.Count())
                    {
                        foundAt = charsProcessed;
                        break;
                    }
                }
            }
        }
        Console.WriteLine($"Found at {foundAt}");
    }
}

