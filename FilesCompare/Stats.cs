using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FilesCompare.Extentions;

namespace FilesCompare
{
    public class Stats
    {
        public long DuplicateCount { get; private set; } = 0;
        public long DuplicateSize { get; private set; } = 0;
        public long TotalFiles { get; private set; } = 0;
        public long OriginalsDuplicatedCount { get; private set; } = 0;


        public Stats(List<FileInfo> files, List<Tuple<FileInfo, List<FileInfo>>> result)
        {
            var sb = new StringBuilder();
            TotalFiles = files.Count;
            OriginalsDuplicatedCount = result.Count;
            result.ForEach(x =>
                {
                    DuplicateCount += x.Item2.Count;
                    x.Item2.ForEach(y => { DuplicateSize += y.Length; });
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
