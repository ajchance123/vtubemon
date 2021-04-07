
namespace VTubeMon.API.Data.Objects
{
    public interface IVTuberImage : IDataObject
    {
        IDataProperty<int> IdVtuberImage { get; }
        IDataProperty<int> IdVtuber { get; }
        IDataProperty<string> ImagePath { get; }
    }
}
