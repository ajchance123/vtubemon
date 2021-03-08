using System;
using System.Collections.Generic;

namespace VTubeMon.API
{
    public interface IDbSelectCommand<T>
    {
        string Statement { get; }

        IEnumerable<T> ReadData(INamedDataReader namedDataReader);
    }
}
