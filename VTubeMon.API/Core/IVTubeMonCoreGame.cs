using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API.Core
{
    public interface IVTubeMonCoreGame
    {
        int DailyCheckinValue { get; set; }
        int RegistrationValue { get; set; }

        DailyCheckinResult DailyCheckIn(ulong user, ulong guild);

        bool Register(ulong user, ulong guild);
    }
}
