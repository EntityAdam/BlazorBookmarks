using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Core;
using Core.Models;

namespace UnitTests
{
    public class FileStoreTests
    {
        [Fact]
        public void CanSaveToFile()
        {
            var folders = new List<FolderModel>()
            {
                new FolderModel() { Id = 1, Name = "Folder1", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 2, Name = "Folder2", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 3, Name = "Folder3", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 4, Name = "Folder4", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 5, Name = "Folder5", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 6, Name = "Folder6", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 7, Name = "Folder7", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 8, Name = "Folder8", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 9, Name = "Folder9", LastUpdated = DateTime.Now },
                new FolderModel() { Id = 10, Name = "Folder10", LastUpdated = DateTime.Now },
            };
            var bookmarks = new List<BookmarkModel>()                                                    
            {                                                                                            
                new BookmarkModel() { Id = 1, FolderId = 1, Name = "Folder1_Bookmark1",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 2, FolderId = 1, Name = "Folder1_Bookmark2",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 3, FolderId = 1, Name = "Folder1_Bookmark3",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 4, FolderId = 1, Name = "Folder1_Bookmark4",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 5, FolderId = 1, Name = "Folder1_Bookmark5",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 6, FolderId = 1, Name = "Folder1_Bookmark6",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 7, FolderId = 1, Name = "Folder1_Bookmark7",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 8, FolderId = 1, Name = "Folder1_Bookmark8",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 9, FolderId = 1, Name = "Folder1_Bookmark9",   Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 10, FolderId = 1, Name = "Folder1_Bookmark10",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 11, FolderId = 2, Name = "Folder2_Bookmark1",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 12, FolderId = 2, Name = "Folder2_Bookmark2",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 13, FolderId = 2, Name = "Folder2_Bookmark3",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 14, FolderId = 2, Name = "Folder2_Bookmark4",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 15, FolderId = 2, Name = "Folder2_Bookmark5",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 16, FolderId = 2, Name = "Folder2_Bookmark6",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 17, FolderId = 2, Name = "Folder2_Bookmark7",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 18, FolderId = 2, Name = "Folder2_Bookmark8",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 19, FolderId = 2, Name = "Folder2_Bookmark9", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 20, FolderId = 2, Name = "Folder2_Bookmark10", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 21, FolderId = 2, Name = "Folder2_Bookmark11", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 22, FolderId = 2, Name = "Folder2_Bookmark12", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 23, FolderId = 2, Name = "Folder2_Bookmark13", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 24, FolderId = 2, Name = "Folder2_Bookmark14", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 25, FolderId = 2, Name = "Folder2_Bookmark15", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 26, FolderId = 2, Name = "Folder2_Bookmark16", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 27, FolderId = 2, Name = "Folder2_Bookmark17", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 28, FolderId = 2, Name = "Folder2_Bookmark18", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 29, FolderId = 2, Name = "Folder2_Bookmark19", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 30, FolderId = 2, Name = "Folder2_Bookmark20", Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 31, FolderId = 3, Name = "Folder3_Bookmark1",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 32, FolderId = 3, Name = "Folder3_Bookmark2",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 33, FolderId = 3, Name = "Folder3_Bookmark3",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 34, FolderId = 3, Name = "Folder3_Bookmark4",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 35, FolderId = 3, Name = "Folder3_Bookmark5",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 36, FolderId = 3, Name = "Folder3_Bookmark6",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 37, FolderId = 3, Name = "Folder3_Bookmark7",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 38, FolderId = 3, Name = "Folder3_Bookmark8",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 39, FolderId = 3, Name = "Folder3_Bookmark9",  Url = "https://entityadam.com" },
                new BookmarkModel() { Id = 40, FolderId = 3, Name = "Folder3_Bookmark10",  Url = "https://entityadam.com" },
            };
            var state = new StateModel() { Folders = folders, Bookmarks = bookmarks };
            var store = new BookmarkFileStore();

            store.Save(state);
        }

        [Fact]
        public void CanReadFromFile()
        {
            var store = new BookmarkFileStore();
            var state = store.Get();

            state.Should().BeOfType(typeof(StateModel));
            state.Folders.Count.Should().Be(10);
            state.Bookmarks.Count.Should().Be(40);
            state.Bookmarks.Where(x => x.FolderId == 1).Count().Should().Be(10);
            state.Bookmarks.Where(x => x.FolderId == 2).Count().Should().Be(20);
            state.Bookmarks.Where(x => x.FolderId == 3).Count().Should().Be(10);
        }
    }
}
