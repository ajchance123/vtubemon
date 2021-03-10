using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core
{
    public class VTuberViewModel
    {
        public VTuberViewModel(VTuber vTuber, string agency)
        {
            IdVtubers = vTuber.IdVtubers.Value;
            EnName = vTuber.EnName.Value;
            JpName = vTuber.JpName.Value;
            ChannelLink = vTuber.ChannelLink.Value;
            DebutDatetimeUtc = vTuber.DebutDatetimeUtc.Value;
            Agency = agency;
            IsIndependent = vTuber.IsIndependent.Value;
            Generation = vTuber.Generation.Value;
        }

        public int IdVtubers { get; private set; }
        public string EnName { get; private set; }
        public string JpName { get; private set; }
        public string ChannelLink { get; private set; }
        public DateTime DebutDatetimeUtc { get; private set; }
        public string Agency { get; private set; }
        public bool IsIndependent { get; private set; }
        public int Generation { get; private set; }

    }
}
