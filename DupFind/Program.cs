using DupFind;

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
foreach (var path in paths)
{
    Console.WriteLine("   " + path);
}
Console.WriteLine();


var files = new Files().GetFiles(paths);
Console.WriteLine($"Files: {files.Count}");

var duplicates = new FindDuplicates().Find(files);

Console.WriteLine("Stats: " + new Stats(files, duplicates));

new WriteToConsole(paths, duplicates);

new WriteBatchfile(paths, duplicates);


