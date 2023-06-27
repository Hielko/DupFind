using FilesCompare;

var currentDir = Directory.GetCurrentDirectory();

Console.WriteLine();
Console.WriteLine("Compare");

string[] paths;

if (args.Length > 0)
{
    paths = args;
//    paths = new string[] { "d:\\martini\\" };
}
else
{
    paths = new string[] { currentDir };
}

var cursor = Console.GetCursorPosition();

using (var spinner = new Spinner(cursor.Left,cursor.Top))
{
    spinner.Start();
    var result = new CompareBySize().Compare(paths);
    spinner.Stop();

    foreach (var dup in result)
    {
        Console.WriteLine($"{dup.Item1}: duplicates");
        foreach (var r in dup.Item2)
        {
            Console.WriteLine($"  -  {r.FullName}");
        }
        Console.WriteLine();
    }
}