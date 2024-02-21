using DupFind;
using System.Text;

Console.WriteLine("DupFind");

string[] tmppaths;

if (args.Length > 0)
{
    tmppaths = args;
}
else
{
    tmppaths = new string[] { Directory.GetCurrentDirectory() };
}

var paths = tmppaths.Select(x => new DirectoryInfo(x)).ToArray();

Console.WriteLine("Paths: ");
foreach (var path in paths) { Console.Write(path + " "); }
Console.WriteLine();

var files = new Files().GetFiles(paths);
Console.WriteLine($"Files: {files.Count}");

StringBuilder stringBuilder = new();

var compareResult = new CompareBySize().CompareList(files);

Console.WriteLine("Stats: " + new Stats(files, compareResult));

foreach (var dup in compareResult)
{
    var orginal = dup.GetOrignal(paths);
    var duplicates = dup.GetDuplicates(paths);
    Console.WriteLine($"\"{orginal?.FullName}\": duplicates");
    stringBuilder.Append($"@rem orginal  \"{orginal?.FullName}\"\n");
    foreach (var file in duplicates)
    {
        Console.WriteLine($"  -  \"{file.FullName}\"");
        stringBuilder.Append($"del  \"{file.FullName}\"\n");
    }
    Console.WriteLine();
}

File.WriteAllText("deldups.bat", stringBuilder.ToString());
