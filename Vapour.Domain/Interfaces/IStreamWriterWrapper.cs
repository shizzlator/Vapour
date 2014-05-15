using System.IO;

namespace Vapour.Domain.Interfaces
{
    public interface IStreamWriterWrapper
    {
        void WriteLine(string line);
        StreamWriter CreateFile(string path);
    }
}