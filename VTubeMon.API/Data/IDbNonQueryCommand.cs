using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API
{
    public interface IDbNonQueryCommand : IDbCommand
    {
        string Table { get; }
    }

    public interface IDbNonQueryCommand<T> : IDbNonQueryCommand
        where T : IDataObject
    {
        IEnumerable<T> DataObjects { get; }
    }
}
