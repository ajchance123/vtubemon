
using System;

namespace VTubeMon.API.Data.Objects
{
    public interface IDaily : IDataObject
    {
        IDataProperty<ulong> IdUser { get; }
        IDataProperty<ulong> IdGuild { get; }
        IDataProperty<DateTime> CheckInDate { get; }
    }
}
