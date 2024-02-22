using System.Text;

namespace DupFind
{
    public class Stats
    {
        public long DuplicateCount { get; private set; }
        public long DuplicateSize { get; private set; }
        public long TotalFilesCount { get; private set; }
        public long OriginalsDuplicatedCount { get; private set; }

        public Stats(List<FileInfo> files, List<Duplicates> result)
        {
            TotalFilesCount = files.Count;
            OriginalsDuplicatedCount = result.Count;
            result.ForEach(duplicates =>
                {
                    if (duplicates.files.Count > 0)
                    {
                        DuplicateCount += duplicates.files.Count;
                        DuplicateSize += (duplicates.files.Count - 1) * duplicates.files[0].Length;
                    }
                }
            );
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"files: {TotalFilesCount}");
            sb.AppendLine($"  orginals: {OriginalsDuplicatedCount}");
            sb.AppendLine($"  duplicates: {DuplicateCount}");
            sb.AppendLine($"  total bytes: {DuplicateSize} or {DuplicateSize.SizeSuffix()}");
            return sb.ToString();
        }
    }
}
