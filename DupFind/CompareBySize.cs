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
                    var duplicates = new Duplicates();

                    foreach (var fileInfo1 in group)
                    {
                        foreach (var fileInfo2 in group)
                        {
                            if ((fileInfo1.FullName != fileInfo2.FullName) && !(duplicates.files.Contains(fileInfo1) || duplicates.files.Contains(fileInfo2)))
                            {
                                if (AreFileContentsEqual(fileInfo1.FullName, fileInfo2.FullName) == true)
                                {
                                    duplicates.files.Add(fileInfo1);
                                    duplicates.files.Add(fileInfo2);
                                }
                            }
                        }
                    }
                    duplicates.files = duplicates.files.Distinct().ToList();
                    if (duplicates?.files.Count > 0)
                    {
                        result.Add(duplicates);
                    }

                }
            }
            return result;
        }

    }
}
