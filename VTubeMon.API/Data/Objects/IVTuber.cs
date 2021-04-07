using System;

namespace VTubeMon.API.Data.Objects
{
    public interface IVTuber : IDataObject
    {
        IDataProperty<int> IdVtubers { get; }
        IDataProperty<string> EnName { get; }
        IDataProperty<string> JpName { get; }
        IDataProperty<string> ChannelLink { get; }
        IDataProperty<DateTime> DebutDatetimeUtc { get; }
        IDataProperty<int> IdAgency { get; }
        IDataProperty<bool> IsIndependent { get; }
        IDataProperty<int> Generation { get; }
    }
}
