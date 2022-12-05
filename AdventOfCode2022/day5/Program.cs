static class Program
{
    static void Main()
    {
        Solution1();
        Solution2();
    }

    static void Solution1()
    {
        var crateDict = new SortedDictionary<int, List<string>>();
        var moveCommands = new List<string>();
        var processingMoves = false;

        foreach (var line in File.ReadAllLines("input.txt"))
        {
            if (line == String.Empty)
            {
                processingMoves = true;
                continue;
            }

            if (!processingMoves)
            {
                var tokenList = ParseLine(line);
                foreach (var stringToken in tokenList)
                {
                    if (!crateDict.ContainsKey(stringToken.Position))
                    {
                        crateDict.Add(stringToken.Position, new List<string>() { stringToken.Value });
                    }
                    else
                    {
                        crateDict[stringToken.Position].Add(stringToken.Value);
                    }
                }
            }
            else
            {
                if (line != String.Empty)
                {
                    moveCommands.Add(line);
                }
            }
        }

        foreach (var command in moveCommands.Select(ParseCommandLine))
        {
            for (var cnt = 0; cnt < command.TotalCount; cnt++)
            {
                var items = crateDict[command.SourceColumn];
                var value = items[0];
                crateDict[command.DestinationColumn].Insert(0, value);
                items.RemoveAt(0);
                crateDict[command.SourceColumn] = items;
            }
        }
        var result = crateDict.Aggregate("", (current, entry) => current + entry.Value[0].Replace("[", "").Replace("]", ""));
        Console.WriteLine(result);
    }

    static void Solution2()
    {
        var crateDict = new SortedDictionary<int, List<string>>();
        var moveCommands = new List<string>();
        var processingMoves = false;

        foreach (var line in File.ReadAllLines("input.txt"))
        {
            if(line == String.Empty)
            {
                processingMoves = true;
                continue;
            }

            if (!processingMoves)
            {
                var tokenList = ParseLine(line);
                foreach(var stringToken in tokenList)
                {
                    if (!crateDict.ContainsKey(stringToken.Position))
                    {
                        crateDict.Add(stringToken.Position, new List<string>() { stringToken.Value });
                    }
                    else
                    {
                        crateDict[stringToken.Position].Add(stringToken.Value);
                    }
                }
            }
            else
            {
                if(line != String.Empty)
                {
                    moveCommands.Add(line);
                }
            }
        }

        foreach(var command in moveCommands.Select(ParseCommandLine))
        {
            for(var cnt = 0; cnt < command.TotalCount; cnt++)
            {
                var items = crateDict[command.SourceColumn];
                var value = items[0];
                crateDict[command.DestinationColumn].Insert(cnt, value);
                items.RemoveAt(0);
                crateDict[command.SourceColumn] = items;
            }
        }
        var result = crateDict.Aggregate("", (current, entry) => current + entry.Value[0].Replace("[", "").Replace("]", ""));
        Console.WriteLine(result);
    }

    static CommandToken ParseCommandLine(string line)
    {
        var cleanLine = line.Replace("move", "").Replace("from", "").Replace("to", "").Trim();
        var lineArr = cleanLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        return new CommandToken
        {
            TotalCount = Convert.ToInt32(lineArr[0]),
            SourceColumn = Convert.ToInt32(lineArr[1]),
            DestinationColumn = Convert.ToInt32(lineArr[2])
        };
    }

    static List<StringToken> ParseLine(string line)
    {
        var tknList = new List<StringToken>();
        var currCol = 1;

        for(var idx =0; idx < line.Length;idx +=4)
        {
            if(line[idx] == '[')
            {
                var tkn = new StringToken
                {
                    Position = currCol,
                    Value = $"{line[idx]}{line[idx + 1]}{line[idx + 2]}"
                };
                tknList.Add(tkn);
            }
            currCol++;
        }
        return tknList;
    }

}

class StringToken
{
    public int Position { get; set; }
    public string Value { get; set; }
}

class CommandToken
{
    public int TotalCount { get; set; }
    public int SourceColumn { get; set; }
    public int DestinationColumn { get; set; }
}

