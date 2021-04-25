using System;

namespace StateService.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }

        public Folder ShallowCopy() => (Folder)MemberwiseClone();
    }
}
