using System.Text;

namespace DupFind
{
    public class WriteToConsole
    {
        public WriteToConsole(DirectoryInfo[] paths, List<Duplicates> duplicates)
        {
            StringBuilder stringBuilder = new();

            foreach (var dup in duplicates)
            {
                var orginalFile = dup.GetOrignal(paths);
                var duplicateFiles = dup.GetDuplicates(paths);
                Console.WriteLine($"\"{orginalFile?.FullName}\": duplicates");
                foreach (var file in duplicateFiles)
                {
                    Console.WriteLine($"  -  \"{file.FullName}\"");
                }
                Console.WriteLine();
            }
        }
    }
}
