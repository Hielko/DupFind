// See https://aka.ms/new-console-template for more information
using FilesCompare;

var x = args[0];


Console.WriteLine(Directory.GetCurrentDirectory());

Console.WriteLine("Hello, World!");
var y = new CompareBySize().Compare(new string[] { $"{Directory.GetCurrentDirectory()}" } );

foreach (var n in y)
{
    var si = string.Join(", ", n.Item2.Select(x => x.FullName));
    Console.WriteLine($"{n.Item1} is in {si}");
}
