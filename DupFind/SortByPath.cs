using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DupFind
{
    public class SortByPath
    {

        public List<Duplicates> Sort(string[] paths, List<Duplicates> duplicates)
        {
            var result = new List<Duplicates>();
            var p = new List<DirectoryInfo>();
            foreach (var path in paths)
            {
                p.Add(new DirectoryInfo(path));
            }

            //duplicates.Sort((a, b) =>
            //{
            //    a.files.
            //    if (a.files.)


            //    return 1;
            //});


            return result;

        }
    }
}
