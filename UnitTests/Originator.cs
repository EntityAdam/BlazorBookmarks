using StateService;
using StateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{

    internal class Originator : IStateful
    {
        public Originator()
        {
            Folders.Add(new Folder() { Id = 1, Name = "Default", LastUpdated = DateTime.Now });
            Bookmarks.Add(new Bookmark() { Id = 1, FolderId = 1, Url = "https://www.entityadam.com", Name = "Default" });
        }

        public List<Bookmark> Bookmarks { get; private set; } = new List<Bookmark>();
        public List<Folder> Folders { get; private set; } = new List<Folder>();

        public void AddFolder(string name) => Folders.Add(new Folder() { Id = Folders.Max(x => x.Id), Name = name, LastUpdated = DateTime.Now });
        public void AddBookmark(string name, int folderId) => Bookmarks.Add(new Bookmark() { Id = Bookmarks.Max(x => x.Id), Name = name, FolderId = folderId });

        public State SaveState() => new State(Folders, Bookmarks);

        public void RestoreState(State state)
        {
            Folders = state.Folders;
            Bookmarks = state.Bookmarks;
        }
    }
}
