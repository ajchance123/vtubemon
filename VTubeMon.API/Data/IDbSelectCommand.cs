using System;
using System.Collections.Generic;

namespace VTubeMon.API
{
    public interface IDbSelectCommand
    {
        string Statement { get; }
    }

    public interface IDbSelectCommand<T> : IDbSelectCommand
    {
        IEnumerable<T> ReadData(INamedDataReader namedDataReader);
    }
}
