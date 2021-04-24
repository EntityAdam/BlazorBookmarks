using System;

namespace BlazorBookmarks.Data
{
    public class LinkCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsEmptyDropZone { get; set; }
        public bool Hidden { get; set; }
    }
}
