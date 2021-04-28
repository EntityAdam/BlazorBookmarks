using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace BlazorBookmarks.Models
{
    public class StateModelUi : IDeepCloneable<StateModelUi>
    {
        public List<FolderModelUi> Folders { get; set; }
        public List<BookmarkModelUi> Bookmarks { get; set; }

        public StateModelUi()
        {
            Folders = new List<FolderModelUi>();
            Bookmarks = new List<BookmarkModelUi>();
        }

        public StateModelUi(List<FolderModelUi> folders, List<BookmarkModelUi> bookmarks)
        {
            Folders = folders;
            Bookmarks = bookmarks;
        }

        public StateModelUi DeepCopy()
        {
            var copyFolders = Folders.Select(x => x.ShallowCopy()).ToList();
            var copyBookmarks = Bookmarks.Select(x => x.ShallowCopy()).ToList();
            return new StateModelUi(copyFolders, copyBookmarks);
        }
    }
}
