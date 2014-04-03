using System;
using System.IO;

namespace Vapour.Domain
{
    public interface IStreamWriterWrapper
    {
        void WriteLine(string line);
        StreamWriter WriteFileTo(string path);
    }

    public class StreamWriterWrapper : IStreamWriterWrapper, IDisposable
    {
        private StreamWriter _streamWriter;

        public void WriteLine(string line)
        {
            _streamWriter.WriteLine(line);
        }

        public StreamWriter WriteFileTo(string path)
        {
            return _streamWriter = new StreamWriter(path);
        }

        public void Dispose()
        {
            _streamWriter.Dispose();
        }
    }
}