using System;

namespace Core.Models
{
    public class FolderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public FolderModel ShallowCopy() => (FolderModel)MemberwiseClone();
    }
}
