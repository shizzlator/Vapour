﻿using System.IO;

namespace Vapour.Domain.TestRunner
{
    public class StreamWriterWrapper : IStreamWriterWrapper
    {
        private StreamWriter _streamWriter;

        public void WriteLine(string line)
        {
            _streamWriter.WriteLine(line);
        }

        public StreamWriter CreateFile(string path)
        {
            return _streamWriter = new StreamWriter(path);
        }
    }
}