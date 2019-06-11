using System;
using System.Threading.Tasks;
using System.IO;

namespace Split
{
    class Program
    {
        static void Main()
        {
            int numFiles = 4;
            string accountInput = GetPath("../data/linkedin_all.txt");
            string accountOutput = GetPath("../data/split/accounts-{0}.txt");

            var passInput = GetPath("../data/68_linkedin_found_hash_plain.txt");
            var passOutput = GetPath("../data/split/passwords-{0}.txt");

            Task.WhenAll(Processor.FormatPass(passInput, passOutput, numFiles),
                         Processor.ProcessHash(accountInput, accountOutput, numFiles)).Wait();
        }
 
        private static string GetPath(string path)
        {
            var project = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            return Path.GetFullPath(Path.Combine(project, path));
        }
   }
}