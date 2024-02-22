namespace DupFind
{
    public class Duplicates
    {
        public List<FileInfo> files = new();

        public FileInfo? GetOrignal(DirectoryInfo[] paths)
        {
            if (files.Count <= 1) throw new Exception("Internal: file has no duplicates");

            if (paths.Length <= 1)
            {
                return files.Any() ? files.First() : null;
            }

            foreach (var file in files)
            {
                if (file.DirectoryName.StartsWith(paths[0].FullName))
                {
                    return file;
                }
            }

            return null;
        }

        public FileInfo[] GetDuplicates(DirectoryInfo[] paths)
        {
            var orginal = GetOrignal(paths);
            return files.Where(file => file != orginal).ToArray();
        }
    }
}
