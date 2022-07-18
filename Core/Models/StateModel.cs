namespace Core.Models
{
    public class StateModel : IDeepCloneable<StateModel>
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

        public StateModel DeepCopy()
        {
            var copyFolders = Folders.ToList();
            var copyBookmarks = Bookmarks.ToList();
            return new StateModel(copyFolders, copyBookmarks);
        }
    }
}
