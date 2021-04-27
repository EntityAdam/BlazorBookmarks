using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{
    public class State : IDeepCloneable<State>
    {
        public List<Folder> Folders { get; set; }
        public List<Bookmark> Bookmarks { get; set; }

        public State()
        {
            Folders = new List<Folder>();
            Bookmarks = new List<Bookmark>();
        }

        public State(List<Folder> folders, List<Bookmark> bookmarks)
        {
            Folders = folders;
            Bookmarks = bookmarks;
        }

        public State DeepCopy()
        {
            var copyFolders = Folders.Select(x => x.ShallowCopy()).ToList();
            var copyBookmarks = Bookmarks.Select(x => x.ShallowCopy()).ToList();
            return new State(copyFolders, copyBookmarks);
        }
    }
}
