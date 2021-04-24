using StateService;
using StateService.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class StateTests
    {
        internal class CurrentState
        {
            public List<Bookmark> Bookmarks = new List<Bookmark>();
            public List<Folder> Folders = new List<Folder>();

            public State Save() => new(Folders, Bookmarks);
        }

        [Fact]
        public void Test1()
        {




        }


        internal class TestData
        {
            public List<Bookmark> Bookmarks = new List<Bookmark>();
            public List<Folder> Folders = new List<Folder>();
            public void CreateTestData()
            {
                Folders.Add(new Folder() { Id = 1, Title = "Work", LastUpdated = DateTime.Now });
                Folders.Add(new Folder() { Id = 2, Title = "Games", LastUpdated = DateTime.Now });
                Folders.Add(new Folder() { Id = 3, Title = "Stuff", LastUpdated = DateTime.Now });
                Folders.Add(new Folder() { Id = 4, Title = "Junk", LastUpdated = DateTime.Now });
                Folders.Add(new Folder() { Id = 5, Title = "Things", LastUpdated = DateTime.Now });
                Folders.Add(new Folder() { Id = 6, Title = "Apps", LastUpdated = DateTime.Now });

                //Work Links
                Bookmarks.Add(new Bookmark() { Id = 1, FolderId = 1, Url = "https://www.entityadam.com", Name = "HR" });
                Bookmarks.Add(new Bookmark() { Id = 2, FolderId = 1, Url = "https://www.entityadam.com", Name = "Career Framework" });
                Bookmarks.Add(new Bookmark() { Id = 3, FolderId = 1, Url = "https://www.entityadam.com", Name = "Sharepoint" });
                Bookmarks.Add(new Bookmark() { Id = 4, FolderId = 1, Url = "https://www.entityadam.com", Name = "Teams" });
                Bookmarks.Add(new Bookmark() { Id = 5, FolderId = 1, Url = "https://www.entityadam.com", Name = "Projects" });
                Bookmarks.Add(new Bookmark() { Id = 6, FolderId = 1, Url = "https://www.entityadam.com", Name = "Azure Portal" });

                //Games Links
                Bookmarks.Add(new Bookmark() { Id = 7, FolderId = 2, Url = "https://www.entityadam.com", Name = "Monster Hunter" });
                Bookmarks.Add(new Bookmark() { Id = 8, FolderId = 2, Url = "https://www.entityadam.com", Name = "Bloons TD6" });
            }
        }
    }
}
