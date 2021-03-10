using System.Collections.Generic;
using VTubeMon.API;

namespace VTubeMon.Data.Objects
{
    public class Agency : DataObjectBase
    {
        public Agency()
        {
            IdAgency = new DataProperty<int>("id_agency", (r) => r.GetInt32);
            AgencyName = new StringDataProperty("agency_name");

            DataPropertyList = new List<IDataProperty>()
            {
                IdAgency, AgencyName
            };
        }

        public DataProperty<int> IdAgency { get; private set; }
        public DataProperty<string> AgencyName { get; private set; }

        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
