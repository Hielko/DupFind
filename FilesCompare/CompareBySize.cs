namespace FilesCompare
{
    public class CompareBySize
    {
        public static bool AreFileContentsEqual(string path1, string path2) =>
              File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));

        public List<Tuple<FileInfo, List<FileInfo>>> CompareList(List<FileInfo> files)
        {
            var filesWithSameSizesLookup = (Lookup<long, FileInfo>)files.ToLookup(f => f.Length, f => f);

            var result = new List<Tuple<FileInfo, List<FileInfo>>>();

            foreach (var group in filesWithSameSizesLookup)
            {
                if (group.Count() > 1)
                {
                    foreach (var fileInfo in group)
                    {
                        foreach (var fileInfo2 in group)
                        {
                            if (fileInfo.FullName != fileInfo2.FullName)
                            {
                                if (AreFileContentsEqual(fileInfo.FullName, fileInfo2.FullName) == true)
                                {
                                    var found = result.Where(x => (x.Item1 == fileInfo) || x.Item2.Contains(fileInfo2) ||
                                                                  (x.Item1 == fileInfo2) || x.Item2.Contains(fileInfo));
                                    if (!found.Any())
                                    {
                                        result.Add(Tuple.Create(fileInfo, new List<FileInfo>() { fileInfo2 }));
                                    }
                                    else
                                    {
                                        if (!found.First().Item2.Contains(fileInfo2) && found.First().Item1 != fileInfo2)
                                        {
                                            found.First().Item2.Add(fileInfo2);
                                        }
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
