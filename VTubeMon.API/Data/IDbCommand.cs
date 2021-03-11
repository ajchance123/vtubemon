using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API
{
    public interface IDbCommand
    {
        string Statement { get; }
    }
}
