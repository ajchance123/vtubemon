using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.API;

namespace VTubeMon.Data.Objects
{
    public class Agency : IDataObject
    {
        public int IdAgency { get; private set; }
        public string Name { get; private set; }

        public void InitializeFromReader(INamedDataReader reader)
        {
            IdAgency = reader.GetInt32("id_agency");
            Name = reader.GetString("name");
        }
    }
}
