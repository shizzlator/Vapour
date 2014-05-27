using System.IO;

namespace Vapour.API
{
    public static class ExtensionMethods
    {
        public static void Empty(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }
}