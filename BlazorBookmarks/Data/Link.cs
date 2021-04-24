using System;

namespace BlazorBookmarks.Data
{
    public class Link
    {
        public int Id { get; set; }
        public int LinkCollectionId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Hidden { get; set; }
    }
}
