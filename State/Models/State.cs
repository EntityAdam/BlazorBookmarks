using StateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StateService.Models
{
    public class State
    {
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

        public List<Folder> Folders { get; } 
        public List<Bookmark> Bookmarks { get; } 

        public State DeepCopy()
        {
            var copyFolders = Folders.Select(x => x.ShallowCopy()).ToList();
            var copyBookmarks = Bookmarks.Select(x=>x.ShallowCopy()).ToList();
            return new State(copyFolders, copyBookmarks);
        }
    }
}