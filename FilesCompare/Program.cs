using FilesCompare;

var currentDir = Directory.GetCurrentDirectory();

Console.WriteLine();
Console.WriteLine("Compare");

string[] paths;

if (args.Length > 0)
{
    paths = args;

    paths = new string[] { "d:\\martini\\" };
}
else
{
    paths = new string[] { currentDir };
}

var x = Console.GetCursorPosition();
using (var spinner = new Spinner(x.Left,x.Top))
{
    spinner.Start();
    var result = new CompareBySize().Compare(paths);
    spinner.Stop();

    foreach (var n in result)
    {
        Console.WriteLine($"{n.Item1}: duplicates");
        foreach (var r in n.Item2)
        {
            Console.WriteLine($"  -  {r.FullName}");
        }
        Console.WriteLine();
    }
}