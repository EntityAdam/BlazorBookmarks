namespace Core.Models
{
    public class QueryState
    {
        private readonly IEnumerable<BookmarkQueryModel> bookmarks;

        public QueryState(IEnumerable<BookmarkQueryModel> bookmarks)
        {
            this.bookmarks = bookmarks;
        }

        public IEnumerable<BookmarkQueryModel> Bookmarks => bookmarks;
    }
}
