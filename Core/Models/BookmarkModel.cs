namespace Core.Models
{
    public class BookmarkModel
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Clicks { get; set; }

        public BookmarkModel ShallowCopy() => (BookmarkModel)MemberwiseClone();
    }
}
