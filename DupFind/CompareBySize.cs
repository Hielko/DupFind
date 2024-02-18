using System;

namespace DupFind
{
    public class CompareBySize
    {
        public static bool AreFileContentsEqual(string path1, string path2) =>
              File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));

        public List<Duplicates> CompareList(List<FileInfo> files)
        {
            var filesWithSameSizesLookup = (Lookup<long, FileInfo>)files.ToLookup(f => f.Length, f => f);

            var result = new List<Duplicates>();

            foreach (var group in filesWithSameSizesLookup)
            {
                if (group.Count() > 1)
                {
                    foreach (var fileInfo1 in group)
                    {
                        foreach (var fileInfo2 in group)
                        {
                            if (fileInfo1.FullName != fileInfo2.FullName)
                            {
                                if (AreFileContentsEqual(fileInfo1.FullName, fileInfo2.FullName) == true)
                                {
                                    var found1 = result.Where(x => (x.files.Contains(fileInfo1)));
                                    if (!found1.Any())
                                    {
                                        result.Add(new Duplicates { files = new List<FileInfo> { fileInfo1, fileInfo2 } });
                                    }

                                    var found2 = result.Where(x => (x.files.Contains(fileInfo2)));
                                    if (!found2.Any())
                                    {
                                        result.Add(new Duplicates { files = new List<FileInfo> { fileInfo2, fileInfo1 } });
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

    }
}
