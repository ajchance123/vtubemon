namespace VTubeMon.API
{
    public interface IIOService
    {
        T DeserializeFileToJson<T>(string filename)
            where T : class;

        void SerializeJsonToFile(string filename, object o);
        void SetPath(string path);
    }
}