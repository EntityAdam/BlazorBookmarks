namespace BlazorBookmarks.Models
{
    public class BookmarkModelUi
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public bool Hidden { get; set; }
        public BookmarkModelUi ShallowCopy() => (BookmarkModelUi)MemberwiseClone();
    }
}
