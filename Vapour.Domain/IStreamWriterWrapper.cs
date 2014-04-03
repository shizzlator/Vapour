using System;
using System.IO;

namespace Vapour.Domain
{
    public interface IStreamWriterWrapper
    {
        void WriteLine(string line);
        StreamWriter CreateFile(string path);
    }
}