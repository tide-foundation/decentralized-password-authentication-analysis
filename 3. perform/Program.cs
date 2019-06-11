using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Perform
{
    class Program
    {
        static void Main(string[] args)
        {
            var bits = 8;
            var threads = 8;
            int numFiles = 4;
            var threadsBlock = 1000;

            string passPath = GetPath("../data/split/joined-{0}.txt");
            string shrPath = GetPath("../data/processedPasswords.txt");

            File.Delete(shrPath);
            using (var procesor = new Procesor<SHA256Managed>(bits, threads, threadsBlock, shrPath))
            {
                for (int i = 0; i < numFiles; i++)
                {
                    var window = new List<(string User, string Pass)>();
                    foreach (var cells in Helper.ReadCsv(string.Format(passPath, i)))
                    {
                        window.Add((cells[0], cells[1]));
                        if (window.Count < procesor.WindowBlock)
                            continue;

                        procesor.Run(window);
                        window.Clear();
                    }
                    procesor.Run(window);
                }
            }
        }

        private static string GetPath(string path)
        {
            var project = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            return Path.GetFullPath(Path.Combine(project, path));
        }
    }
}