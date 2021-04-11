namespace VTubeMon.API
{
    public interface IModelService
    {
        T LoadModel<T>() where T : class;
        T LoadModel<T>(string fileName, bool useCache = true) where T : class;
        void SaveModel<T>(string fileName, T model);
    }
}