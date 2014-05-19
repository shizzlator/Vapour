using System.IO;

namespace Vapour.Domain.TestRunner
{
    public interface IStreamWriterWrapper
    {
        void WriteLine(string line);
        StreamWriter CreateFile(string path);
    }
}