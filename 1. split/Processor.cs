using System;
using System.IO;
using System.Threading.Tasks;

namespace Split
{
    public static class Processor
    {
        public static Account GetInfo(string text)
        {
            var leftVec = text.Split(':');
            var rightVec = leftVec[1].Split("->");

            var id = leftVec[0].Trim();
            var hash = rightVec[0].Trim();
            var mail = rightVec.Length > 1 ? rightVec[1].Trim() : string.Empty;

            hash = hash != "xxx" ? hash : string.Empty;
            id = id != "null" ? id : string.Empty;
            if (!Helper.IsNumber(id))
            {
                var sw = id;
                id = mail;
                mail = sw;
            }

            return new Account(id, hash, mail);
        }
        public static Task FormatPass(string input, string output, int numFiles){
            return Task.Run(() =>
            {
                var block = (int) Math.Ceiling((double) (0xf+1) /  numFiles);

                using (var outputFile = new WriterSet(numFiles, output))
                {
                    foreach (var line in File.ReadLines(input))
                    {
                        var index = Convert.ToInt32(line[0].ToString(), 16) / block;
                        try {
                        outputFile.Write(index, line.Substring(0, 40));
                        outputFile.Write(index, ',');
                        outputFile.WriteLine(index, Helper.FormatCVSstring(line.Substring(41, line.Length - 41)));
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(index);
                            Console.WriteLine(line);
                            throw e;
                        }
                    }
                }
            });
        }

        public static Task ProcessHash(string input, string output, int numFiles){
            return Task.Run(() => {
                var block = (int)Math.Ceiling((double)(0xf + 1) / numFiles);

                using (var outputFile = new WriterSet(numFiles, output))
                {
                    foreach (var line in File.ReadLines(input))
                    {
                        var account = GetInfo(line);
                        if (string.IsNullOrEmpty(account.Mail) || string.IsNullOrEmpty(account.Hash))
                            continue;
                        try{
                        var index = Convert.ToInt32(account.Hash[0].ToString(), 16) / block;
                        outputFile.Write(index, account.Hash);
                        outputFile.Write(index, ',');
                        outputFile.WriteLine(index, account.Mail);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(line);
                            throw e;
                        }
                    }
                }
            });
        }
    }
}