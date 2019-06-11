using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace Perform
{
    public class Procesor<THash> : IDisposable where THash : HashAlgorithm, new()
    {
        private readonly int _bits;
        private readonly int _threads;
        private readonly int _threadsBlock;
        private readonly bool _isHex;
        private readonly StreamWriter _file;

        public int WindowBlock { get; }
        private int Bytes { get => (int) Math.Ceiling(_bits / 8.0); }

        public Procesor(int bits, int threads, int threadsBlock, string outputPath): this(bits, threads, threadsBlock, false, outputPath)
        {
        }

        public Procesor(int bits, int threads, int threadsBlock, bool isHex, string outputPath)
        {
            _bits = bits;
            _threads = threads;
            _threadsBlock = threadsBlock;
            _isHex = isHex;
            _file = new StreamWriter(outputPath, true);
            WindowBlock = threads * threadsBlock;
        }

        private Task<string> Compute(IEnumerable<(string User, string Pass)> passwords)
        {
            return Task.Run(() => {
                var text = new StringBuilder();
                
                foreach (var (user, pwd) in passwords)
                {
                    if (string.IsNullOrEmpty(pwd))
                        throw new NotImplementedException($"password can not be null: {user}");

                    using (var sha = new THash())
                    {
                        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        var number = new BigInteger(hash.Take(Bytes).ToArray(), true, true);

                        text.Append(Helper.FormatCVSstring(user)).Append(",")
                            .Append(Helper.FormatCVSstring(pwd)).Append(",")
                            .Append(_isHex ? number.ToString("X") : number.ToString()).AppendLine();
                    }
                }

                return text.ToString(); 
            });
        }

        public void Run(IEnumerable<(string User, string Pass)> set)
        {
            var tasks = Enumerable.Range(0, _threads).Select(i => Compute(set.Skip(i * _threadsBlock).Take(_threadsBlock)));
            var data = Task.WhenAll(tasks).GetAwaiter().GetResult().ToList();

            data.ForEach(block => _file.Write(block));
            _file.Flush();
        }

        public void Dispose()
        {
            _file.Dispose();
        }
    }
}