namespace Core.Models
{
    public class StateModel : IState<StateModel>
    {
        public List<FolderModel> Folders { get; set; }
        public List<BookmarkModel> Bookmarks { get; set; }

        public StateModel()
        {
            Folders = new List<FolderModel>();
            Bookmarks = new List<BookmarkModel>();
        }

        public StateModel(List<FolderModel> folders, List<BookmarkModel> bookmarks)
        {
            Folders = folders;
            Bookmarks = bookmarks;
        }

        public StateModel Snapshot()
        {
            return new StateModel(Folders, Bookmarks);
        }
    }
}
