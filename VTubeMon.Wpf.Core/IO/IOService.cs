using Newtonsoft.Json;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.IO
{
    public class IOService : IIOService
    {
        public IOService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void SetPath(string path)
        {
            _path = path;
        }

        private IFileService _fileService;
        private string _path;

        public void SerializeJsonToFile(string filename, object o)
        {
            var jsonString = JsonConvert.SerializeObject(o);
            var path = _fileService.PathCombine(_path, filename);
            _fileService.WriteAllText(path, jsonString);
        }

        public T DeserializeFileToJson<T>(string filename)
            where T : class
        {
            try
            {
                var jsonString = _fileService.ReadAllText(_fileService.PathCombine(_path, filename));
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return null;
            }
        }
    }
}
