﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace FilesCompare
{
    public class CompareBySize
    {

        public static bool AreFileContentsEqual(String path1, String path2) =>
              File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));

        // This method accepts two strings the represent two files to
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the
        // files are not the same.
        private static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is
            // equal to "file2byte" at this point only if the files are
            // the same.
            return ((file1byte - file2byte) == 0);
        }


        public List<Tuple<FileInfo, List<FileInfo>>> Compare(string[] paths)
        {
            List<string> f = new();
            var files = new List<FileInfo>();

            foreach (var path in paths)
            {
                foreach (var file in Directory.EnumerateFiles(path, "*.*", new EnumerationOptions { RecurseSubdirectories = true }))
                {
                    files.Add(new FileInfo(file));
                }
            }

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
                            if ((fileInfo.FullName != fileInfo2.FullName))
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
