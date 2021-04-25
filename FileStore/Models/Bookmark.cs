namespace FileStore.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
