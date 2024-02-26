using System.Security.Cryptography;

namespace DupFind
{
    public class FindDuplicates
    {
        static string CalculateMD5(string filename)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filename);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public List<Duplicates> Find(List<FileInfo> files)
        {
            var filesWithSameSizesLookup = (Lookup<long, FileInfo>)files.ToLookup(f => f.Length, f => f);

            var result = new List<Duplicates>();

            Parallel.ForEach(filesWithSameSizesLookup, sizeAndFileinfo =>
            {
                if (sizeAndFileinfo.Count() > 1)
                {
                    var hashTable = new List<Tuple<string, FileInfo>>();

                    foreach (var fileInfo in sizeAndFileinfo)
                    {
                        hashTable.Add(Tuple.Create(CalculateMD5(fileInfo.FullName), fileInfo));
                    }

                    var grouped = hashTable.GroupBy(x => x.Item1);

                    foreach (var kvp in grouped)
                    {
                        var hashAndFileinfo = kvp.ToArray();
                        if (hashAndFileinfo.Length > 1)
                        {
                            var duplicates = new Duplicates();

                            foreach (var hashAndFileTuple in hashAndFileinfo)
                            {
                                duplicates.files.Add(hashAndFileTuple.Item2);
                            }

                            if (duplicates?.files.Count > 0)
                            {
                                result.Add(duplicates);
                            }
                        }
                    }
                }
            });

            return result;
        }
    }
}
