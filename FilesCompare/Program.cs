using FilesCompare;
using System.Text;

var currentDir = Directory.GetCurrentDirectory();

Console.WriteLine("Compare");

string[] paths;

if (args.Length > 0)
{
    paths = args;
}
else
{
    paths = new string[] { currentDir };
}


Console.WriteLine("Paths: ");
foreach (string path in paths) { Console.Write(path + " "); }
Console.WriteLine();

var files = new Files().GetFiles(paths);
Console.WriteLine($"Files: {files.Count}");

StringBuilder stringBuilder = new();

var result = new CompareBySize().CompareList(files);

Console.WriteLine("Stats: " + new Stats(files, result));

foreach (var dup in result)
{
    Console.WriteLine($"\"{dup.Item1}\": duplicates");
    stringBuilder.Append($"@rem orginal  \"{dup.Item1.FullName}\"\n");
    //stringBuilder.Append($"@rem del  \"{dup.Item1.FullName}\"\n");
    foreach (var r in dup.Item2)
    {
        Console.WriteLine($"  -  \"{r.FullName}\"");
        stringBuilder.Append($"del  \"{r.FullName}\"\n");
    }
    Console.WriteLine();
}

File.WriteAllText("deldups.bat", stringBuilder.ToString());
