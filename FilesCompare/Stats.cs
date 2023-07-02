using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesCompare
{
    public class Stats
    {
        public long DuplicateCount { get; private set; } = 0;
        public long DuplicateSize { get; private set; } = 0;
        public long TotalFiles { get; private set; } = 0;
        public long OriginalsDuplicatedCount { get; private set; } = 0;

        static readonly string[] SizeSuffixes =
                  { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }


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
            sb.AppendLine($"  total bytes: {DuplicateSize} or {SizeSuffix(DuplicateSize)}");
            return sb.ToString();
        }
    }
}
