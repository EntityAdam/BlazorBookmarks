using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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
            var copyFolders = Folders.Select(x => x.ShallowCopy()).ToList();
            var copyBookmarks = Bookmarks.Select(x => x.ShallowCopy()).ToList();
            return new StateModel(copyFolders, copyBookmarks);
        }
    }
}
