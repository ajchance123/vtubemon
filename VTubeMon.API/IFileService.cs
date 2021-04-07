using System.Collections.Generic;

namespace VTubeMon.API
{
    public interface IFileService
    {
        void WriteAllLines(string path, IEnumerable<string> contents);
        void WriteAllLines(string path, string[] contents);
        void WriteAllText(string path, string text);
        string ReadAllText(string path);

        string PathCombine(params string[] paths);
    }
}
