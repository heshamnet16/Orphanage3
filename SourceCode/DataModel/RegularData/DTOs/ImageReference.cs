using System.Drawing;

namespace OrphanageDataModel.RegularData.DTOs
{
    public class ImageReference
    {
        public int Id { get; set; }
        public Size ImageSize { get; set; }
        public ObjectTypeEnum ObjectType { get; set; }
        public string PropertyName { get; set; }
    }
}