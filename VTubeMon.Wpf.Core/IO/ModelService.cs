using System;
using System.Collections.Generic;
using VTubeMon.API;

namespace VTubeMon.Wpf.Core.IO
{
    public class ModelService : IModelService
    {
        public ModelService(IIOService ioService)
        {
            _ioService = ioService;
        }

        private Dictionary<Type, object> _modelCache = new Dictionary<Type, object>();

        private IIOService _ioService;

        public T LoadModel<T>(string fileName, bool useCache = true)
            where T : class
        {
            var key = typeof(T);
            if (_modelCache.ContainsKey(key))
            {
                if (useCache)
                {
                    return _modelCache[key] as T;
                }

                //update
                var model = _ioService.DeserializeFileToJson<T>(fileName);
                _modelCache[key] = model;
                return model;
            }

            //add
            var newModel = _ioService.DeserializeFileToJson<T>(fileName);
            _modelCache.Add(key, newModel);
            return newModel;
        }

        public T LoadModel<T>()
            where T : class
        {
            var key = typeof(T);
            return _modelCache[key] as T;
        }

        public void SaveModel<T>(string fileName, T model)
        {
            _ioService.SerializeJsonToFile(fileName, model);

            var key = typeof(T);
            if (_modelCache.ContainsKey(key))
            {
                _modelCache[key] = model;
                return;
            }

            _modelCache.Add(key, model);
        }
    }

}
