using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API.Core
{
    public struct DailyCheckinResult
    {
        public bool CheckInSuccessfull { get; set; }
        public int TotalDailyPoints { get; set;  }
    }
}
