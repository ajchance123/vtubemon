using System;
using System.Collections.Generic;

namespace VTubeMon.API
{
    public interface IDbQueryCommand<T> : IDbCommand
    {
        IEnumerable<T> ReadData(INamedDataReader namedDataReader);
    }
}
