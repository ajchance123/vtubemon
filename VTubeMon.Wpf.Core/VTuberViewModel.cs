using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.Data.Objects;

namespace VTubeMon.Wpf.Core
{
    public class VTuberViewModel
    {
        public VTuberViewModel(VTuber vTuber, string affiliation)
        {
            IdVtubers = vTuber.IdVtubers;
            EnName = vTuber.EnName;
            JpName = vTuber.JpName;
            ChannelLink = vTuber.ChannelLink;
            DebutDatetimeUtc = vTuber.DebutDatetimeUtc;
            Affiliation = affiliation;
            IsIndependent = vTuber.IsIndependent;
            Generation = vTuber.Generation;
        }

        public int IdVtubers { get; private set; }
        public string EnName { get; private set; }
        public string JpName { get; private set; }
        public string ChannelLink { get; private set; }
        public DateTime DebutDatetimeUtc { get; private set; }
        public string Affiliation { get; private set; }
        public bool IsIndependent { get; private set; }
        public int Generation { get; private set; }

    }
}
