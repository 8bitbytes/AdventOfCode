using System.Text;

static class Program
{
    const int MAX_SIZE = 100000;
    const int MAX_DELETE_SIZE = 8381165;
    static string CurrentDirectory = "";
    static Dictionary<string, AocDirectory> tree = new Dictionary<string, AocDirectory>();
    static AocDirectory Root = new AocDirectory();
    static AocDirectory CurrentDirectoryObject = new AocDirectory();
    static void Main()
    {
        Solution1();
        Solution2();
    }
    static void Solution1()
    {
        var lines = File.ReadAllLines("input1.txt");
        Root = new AocDirectory
        {
            Directories = new List<AocDirectory>(),
            DirectoryName = "/",
            Files = new List<AocFile>(),
            ParentDirectory = null
        };
        var index = 0;
        foreach (var line in lines)
        {
            Log($"Processing line {index } {line}");
            index++;
            var inCommand = IsCommand(line);
            if (inCommand)
            {
                ProcessCommand(line);
            }
            else
            {
                if (IsDirectoryPath(line))
                {
                    CurrentDirectoryObject.Directories.Add(ParseDirectory(line));
                }
                else
                {
                    CurrentDirectoryObject.Files.Add(ParseFile(line));
                }
            }
        }
        var res = GetAllDirectories(Root, new List<AocDirectory>());
        var totals = new List<KeyValuePair<string, int>>();
        foreach(var item in res)
        {   
                var size = CalculateDirectoryStorage(item, 0);
                if(size <= MAX_SIZE)
                  totals.Add(new KeyValuePair<string,int>(item.DirectoryName, size));
        }
        var sum = 0;
        foreach(var total in totals)
        {
            sum += total.Value;
        }
        Console.Write($"Total {sum}");
    }

    static void Solution2()
    {
        var lines = File.ReadAllLines("input1.txt");
        Root = new AocDirectory
        {
            Directories = new List<AocDirectory>(),
            DirectoryName = "/",
            Files = new List<AocFile>(),
            ParentDirectory = null
        };
        var index = 0;
        foreach (var line in lines)
        {
            Log($"Processing line {index } {line}");
            index++;
            var inCommand = IsCommand(line);
            if (inCommand)
            {
                ProcessCommand(line);
            }
            else
            {
                if (IsDirectoryPath(line))
                {
                    CurrentDirectoryObject.Directories.Add(ParseDirectory(line));
                }
                else
                {
                    CurrentDirectoryObject.Files.Add(ParseFile(line));
                }
            }
        }
        var res = GetAllDirectories(Root, new List<AocDirectory>());
        var totals = new List<int>();
        foreach (var item in res)
        {
            var size = CalculateDirectoryStorage(item, 0);
            if (size >= MAX_DELETE_SIZE)
                totals.Add(size);
        }
        totals.Sort();
      
        Console.Write($"Total {totals[0]}");
        Console.ReadLine();

    }

    static int CalculateDirectoryStorage(AocDirectory dir,int size)
    {
        foreach(var file in dir.Files)
        {
            size += file.Size;
        }

        foreach(var d in dir.Directories)
        {
            size = CalculateDirectoryStorage(d, size);
        }
        return size;
    }
  static List<AocDirectory> GetAllDirectories(AocDirectory dir, List<AocDirectory> directories)
    {
        directories.Add(dir);
     
        foreach(var d in dir.Directories)
        {
            directories = GetAllDirectories(d, directories);
        }
        return directories;
    }
    static AocDirectory GetDirectoryByName(AocDirectory dir, string name)
    {
        foreach(var entry in dir.Directories)
        {
            if(entry.DirectoryName == name)
            {
                return entry;
            }
        }
        return null;
    }
   
    static int CalculateDirectorySize(List<AocDirectory> directories, int size)
    {
        foreach(var dir in directories)
        {
            foreach(var file in dir.Files)
            {
                size += file.Size;
            }

            size += CalculateDirectorySize(dir.Directories, size);
        }

        return size;
    }
    static bool IsCommand(string line)
    {
        return line[0].ToString() == "$";
    }

    static void ProcessCommand(string command)
    {
        var cmd = command.Substring(2);
        var cmdParts = cmd.Split(" ");

        switch (cmdParts[0])
        {
            case "cd":
                {
                    if (cmdParts[1] == "/")
                    {
                        CurrentDirectory = "/";
                        CurrentDirectoryObject = Root;
                        Log("Change dir to root");
                        GenerateBreadCrumb(CurrentDirectoryObject, new List<string>());
                        return;
                    }
                    if (cmdParts[1] == "..")
                    {
                        if (CurrentDirectoryObject.ParentDirectory == null)
                        {
                            throw new Exception("At top level");
                        }
                        Log($"Switching from {CurrentDirectoryObject.DirectoryName} to {CurrentDirectoryObject.ParentDirectory.DirectoryName}");
                        
                        CurrentDirectory =  CurrentDirectoryObject.ParentDirectory.DirectoryName;
                        CurrentDirectoryObject = CurrentDirectoryObject.ParentDirectory;
                        if (CurrentDirectoryObject == null)
                        {
                            Console.WriteLine("Yikes");
                        }
                        GenerateBreadCrumb(CurrentDirectoryObject, new List<string>());
                        return;
                    }
                    else
                    {
                        Log($"Switching from {CurrentDirectoryObject.DirectoryName} to {cmdParts[1]}");
                        CurrentDirectory = cmdParts[1];
                        CurrentDirectoryObject = GetDirectoryByName(CurrentDirectoryObject, CurrentDirectory);
                        if(CurrentDirectoryObject == null)
                        {
                            Console.WriteLine("Yikes");
                        }
                        GenerateBreadCrumb(CurrentDirectoryObject, new List<string>());
                        return;
                    }
                    break;
                }
        }
        
    }

    static void GenerateBreadCrumb(AocDirectory current, List<string> path)
    {
        
        path.Add(current.DirectoryName);
        if (current.ParentDirectory == null)
        {
            var sb = new StringBuilder();
            path.Reverse();
            foreach(var s in path)
            {
                if(s != "/")
                {
                    sb.Append($"{s}/");
                }
                else
                {
                    sb.Append(s);
                }
            }
            Log(sb.ToString());
        }
        else
        {
            GenerateBreadCrumb(current.ParentDirectory, path);
        }

    }
    static AocFile ParseFile(string input)
    {
        var fileData = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return new AocFile
        {
            Name = fileData[1],
            Size = Convert.ToInt32(fileData[0])
        };
    }
    static AocDirectory ParseDirectory(string input)
    {
        var folderName = input.Replace("dir", "").Trim();
        return new AocDirectory
        {
            Directories = new List<AocDirectory>(),
            DirectoryName = folderName,
            ParentDirectory = CurrentDirectoryObject,
            Files = new List<AocFile>()
        };
    }
    static bool IsDirectoryPath(string path)
    {
        return path.IndexOf("dir") > -1;
    }
    static void Log(string message)
    {
        Console.WriteLine(message);
    }
}
class AocDirectory
{
    public AocDirectory? ParentDirectory { get; set; }
    public string DirectoryName { get; set; }
    public List<AocDirectory> Directories { get; set; }
    public List<AocFile> Files { get; set; }
}

class AocFile
{
    public string Name { get; set; }
    public string Path { get; set; }
    public int Size { get; set; }
}