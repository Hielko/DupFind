using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DupFind
{
    public class WriteBatchfile
    {

       public WriteBatchfile(DirectoryInfo[] paths, List<Duplicates> duplicates)
        {
            StringBuilder stringBuilder = new();

            foreach (var dup in duplicates)
            {
                var orginalFile = dup.GetOrignal(paths);
                var duplicateFiles = dup.GetDuplicates(paths);
                Console.WriteLine($"\"{orginalFile?.FullName}\": duplicates");
                stringBuilder.Append($"@rem orginal  \"{orginalFile?.FullName}\"\n");
                foreach (var file in duplicateFiles)
                {
                    Console.WriteLine($"  -  \"{file.FullName}\"");
                    stringBuilder.Append($"del  \"{file.FullName}\"\n");
                }
                Console.WriteLine();
            }

            File.WriteAllText("deldups.bat", stringBuilder.ToString());

        }
    }
}
