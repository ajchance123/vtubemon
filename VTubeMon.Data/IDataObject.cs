using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace VTubeMon.Data
{
    public interface IDataObject
    {
        void InitializeFromReader(MySqlDataReader reader);
    }
}
