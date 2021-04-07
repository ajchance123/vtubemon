using System;
using System.Collections.Generic;
using System.IO;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.IO
{
    public class FileService : IFileService
    {
        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            File.WriteAllLines(path, contents);
        }

        public void WriteAllLines(string path, string[] contents)
        {
            File.WriteAllLines(path, contents);
        }
        public void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public string PathCombine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public string GetRelativePath(string relativeTo, string path)
        {
            return Path.GetRelativePath(relativeTo, path);
        }
    }
}
