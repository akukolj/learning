using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\windows";
            ShowLargeFilesWithoutLinq(path);
            ShowLargeFilesWithLinq(path);
        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            var query = new DirectoryInfo(path).GetFiles().OrderByDescending(f => f.Length).Take(5);
                
            foreach (var file in query)
            {
                Console.WriteLine($"{file.Name} {file.Length}");
            }
        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());
            foreach (var file in files)
            {
                Console.WriteLine($"{file.Name} {file.Length}");
            }
           
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y) => y.Length.CompareTo(x.Length);
    }
}
