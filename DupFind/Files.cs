using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DupFind
{
    public class Files
    {
        public List<FileInfo> GetFiles(DirectoryInfo[] paths)
        {
            var files = new List<FileInfo>();

            foreach (var path in paths)
            {
                foreach (var file in Directory.EnumerateFiles(path.FullName, "*.*", new EnumerationOptions { RecurseSubdirectories = true }))
                {
                    var tmpFile = new FileInfo(file);
                    if (tmpFile.Length > 0)
                    {
                        files.Add(tmpFile);
                    }
                }
            }

            return files;
        }
    }
}

