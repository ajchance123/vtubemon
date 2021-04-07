using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class VTuberImage : DataObjectBase, IVTuberImage
    {
        public VTuberImage()
        {
            IdVtuberImage = new DataProperty<int>("id_vtuber_image", (r) => r.GetInt32, false);
            IdVtuber = new DataProperty<int>("id_vtuber", (r) => r.GetInt32);
            ImagePath = new StringDataProperty("image_path");

            DataPropertyList = new List<IDataProperty>()
            {
                IdVtuberImage,
                IdVtuber,
                ImagePath
            };
        }

        public VTuberImage(int idVtuber, string imagePath)
        {
            IdVtuberImage = new DataProperty<int>("id_vtuber_image", (r) => r.GetInt32, false);
            IdVtuber = new DataProperty<int>("id_vtuber", (r) => r.GetInt32, idVtuber);
            ImagePath = new StringDataProperty("image_path", imagePath);

            DataPropertyList = new List<IDataProperty>()
            {
                IdVtuberImage,
                IdVtuber,
                ImagePath
            };
        }

        public IDataProperty<int> IdVtuberImage { get; }
        public IDataProperty<int> IdVtuber { get; }
        public IDataProperty<string> ImagePath { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
