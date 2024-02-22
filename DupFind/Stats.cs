using System.Text;
using static DupFind.Extentions;

namespace DupFind
{
    public class Stats
    {
        public long DuplicateCount { get; private set; } = 0;
        public long DuplicateSize { get; private set; } = 0;
        public long TotalFiles { get; private set; } = 0;
        public long OriginalsDuplicatedCount { get; private set; } = 0;


        public Stats(List<FileInfo> files, List<Duplicates> result)
        {
            var sb = new StringBuilder();
            TotalFiles = files.Count;
            OriginalsDuplicatedCount = result.Count;
            result.ForEach(x =>
                {
                    if (x.files.Count > 0)
                    {
                        DuplicateCount += x.files.Count;
                        DuplicateSize += (x.files.Count - 1) * x.files[0].Length;
                    }
                }
            );
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"files: {TotalFiles}");
            sb.AppendLine($"  orginals: {OriginalsDuplicatedCount}");
            sb.AppendLine($"  duplicates: {DuplicateCount}");
            sb.AppendLine($"  total bytes: {DuplicateSize} or {DuplicateSize.SizeSuffix()}");
            return sb.ToString();
        }
    }
}
