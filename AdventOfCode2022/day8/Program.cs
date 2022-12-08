static class Program
{
    static void Main()
    {
        //Solution1();
        Solution2();
    }

    static void Solution1()
    {
        var treeCount = 0;
        var lines = ConvertToList(File.ReadAllLines("input.txt"));
        var pnts = new List<Point>();
        for(var line=1;line<lines.GetLength(0) -1;line++)
        {
            for(var c = 1; c < lines.GetLength(1) -1; c++)
            {
                var valToCheck = lines[line, c];
                //Console.Write(valToCheck);

                //left
                var visible = Enumerable.Range(0, c)
                .Select(x => lines[line, x])
                .ToArray().Where(item => item >= valToCheck).Count() == 0;

                if (visible)
                {
                    pnts.Add(new Point { X = line, Y = c, Value = valToCheck,VisibleTo="LEFT" });
                    treeCount++;
                    continue;
                }

                //right
                var count = lines.GetLength(1) - 1 - c;
                var lst = Enumerable.Range(c, count)
               .Select(x => lines[line, x])
               .ToArray();

                visible = Enumerable.Range(c + 1, count)
               .Select(x => lines[line, x])
               .ToArray().Where(item => item >= valToCheck).Count() == 0;

                if (visible)
                {
                    pnts.Add(new Point { X = line, Y = c, Value = valToCheck, VisibleTo="RIGHT" });
                    treeCount++;
                    continue;
                }

                //top
                visible = Enumerable.Range(0, line)
                .Select(x => lines[x, c])
                .ToArray().Where(item => item >= valToCheck).Count() == 0;

                if (visible)
                {
                    pnts.Add(new Point { X = line, Y = c, Value = valToCheck, VisibleTo ="TOP" });
                    treeCount++;
                    continue;
                }

                //bottom
                visible = Enumerable.Range(line + 1, lines.GetLength(0) - 1 - line)
                .Select(x => lines[x, c])
                .ToArray().Where(item => item >= valToCheck).Count() == 0;

                if (visible)
                {
                    pnts.Add(new Point { X = line, Y = c, Value = valToCheck,VisibleTo="BOTTOM" });
                    treeCount++;
                    continue;
                }

            }
            Console.Write(Environment.NewLine);
        }
        var total = (lines.GetLength(1) * 2 + (lines.GetLength(0) - 2) * 2) + treeCount;
        Console.WriteLine("yup");

    }
    static void Solution2()
    {
        var treeCount = 0;
        var lines = ConvertToList(File.ReadAllLines("input.txt"));
        var viewingDistances = new List<int>();
        for (var line = 1; line < lines.GetLength(0) - 1; line++)
        {
            for (var c = 1; c < lines.GetLength(1) - 1; c++)
            {
                var valToCheck = lines[line, c];
                //Console.Write(valToCheck);
                var currdistance = 1;
                //left
                var viewingDistance = Enumerable.Range(0, c)
                .Select(x => lines[line, x])
                .ToArray().Reverse().MagicTakeWhile(item => item < valToCheck).Count();

                if (viewingDistance == 0) viewingDistance = 1;
                currdistance *= viewingDistance;
                //right
                viewingDistance = Enumerable.Range(c + 1, lines.GetLength(1) - 1 - c)
               .Select(x => lines[line, x])
               .MagicTakeWhile(item => item < valToCheck)            
               .ToArray().Count();

                if (viewingDistance == 0) viewingDistance = 1;
                currdistance *= viewingDistance;

                //top
                viewingDistance = Enumerable.Range(0, line)
                .Select(x => lines[x, c])
                .ToArray().Reverse().MagicTakeWhile(item => item < valToCheck).Count();

                if (viewingDistance == 0) viewingDistance = 1;
                currdistance *= viewingDistance;

                //bottom
                viewingDistance = Enumerable.Range(line + 1, lines.GetLength(0) - 1 - line)
                .Select(x => lines[x, c])
                .MagicTakeWhile(item => item < valToCheck)
                .ToArray().Count();

                if (viewingDistance == 0) viewingDistance = 1;
                currdistance *= viewingDistance;

                if (currdistance > 0)
                    viewingDistances.Add(currdistance);

                viewingDistance = 0;

            }
            //Console.Write(Environment.NewLine);
        }
        viewingDistances.Sort();
        Console.WriteLine($"{viewingDistances[viewingDistances.Count() -1]}");

    }

    static bool IsTreeVisible(Point treeLocation)
    {
        return false;
    }

    static int[,] ConvertToList(string[] array)
    {
        var ret = new int[array.Length, array[0].Length];

        for (var lineIdx = 0; lineIdx < array.Length; lineIdx++)
        {
            for(var charIdx = 0; charIdx < array[lineIdx].Length;charIdx++)
            {
                ret[lineIdx, charIdx] = Convert.ToInt32(array[lineIdx][charIdx].ToString());
            }
        }

        return ret;
    }
}


class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Value { get; set; }
    public string VisibleTo { get; set; }
}

public static class LinqEx
{
    public static IEnumerable<T> MagicTakeWhile<T>(this IEnumerable<T> data, Func<T, bool> predicate)
    {
        foreach (var item in data)
        {
            yield return item;
            if (!predicate(item))
                break;
        }
    }
}