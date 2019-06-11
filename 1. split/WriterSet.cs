using System;
using System.Collections.Generic;
using System.IO;

namespace Split
{
    public class WriterSet : IDisposable
    {
        readonly List<StreamWriter> streams;
        public WriterSet(int number, string path) {
            streams = new List<StreamWriter>(number);
            for (int i = 0; i < number; i++)
                streams.Add(new StreamWriter(string.Format(path, i), false));
        }

        public void Write(int index, string data)
        {
            streams[index].Write(data);
        }

        public void Write(int index, char data)
        {
            streams[index].Write(data);
        }

        public void WriteLine(int index, string data)
        {
            streams[index].WriteLine(data);
        }

        public void WriteLine(int index, char data)
        {
            streams[index].WriteLine(data);
        }

        public void Dispose()
        {
            foreach (var str in streams)
                str.Dispose();
        }
    }
}