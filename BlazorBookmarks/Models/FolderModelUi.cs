using System;

namespace BlazorBookmarks.Models
{
    public class FolderModelUi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Hidden { get; set; }
        public FolderModelUi ShallowCopy() => (FolderModelUi)MemberwiseClone();
    }
}
