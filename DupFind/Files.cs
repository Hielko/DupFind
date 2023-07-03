using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DupFind
{
    public class Files
    {
        public List<FileInfo> GetFiles(string[] paths)
        {
            var files = new List<FileInfo>();

            foreach (var path in paths)
            {
                foreach (var file in Directory.EnumerateFiles(path, "*.*", new EnumerationOptions { RecurseSubdirectories = true }))
                {
                    files.Add(new FileInfo(file));
                }
            }

            return files;
        }
    }
}

