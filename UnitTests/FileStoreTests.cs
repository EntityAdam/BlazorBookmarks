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
            var folders = new List<Folder>()
            {
                new Folder() { Id = 1, Name = "Folder1", LastUpdated = DateTime.Now },
                new Folder() { Id = 2, Name = "Folder2", LastUpdated = DateTime.Now },
                new Folder() { Id = 3, Name = "Folder3", LastUpdated = DateTime.Now },
            };
            var bookmarks = new List<Bookmark>()
            {
                new Bookmark() { Id = 1, FolderId = 1, Name = "Folder1_Bookmark1", Url = "https://folder1-bookmark1.com" },
                new Bookmark() { Id = 2, FolderId = 1, Name = "Folder1_Bookmark2", Url = "https://folder1-bookmark2.com" },
                new Bookmark() { Id = 3, FolderId = 2, Name = "Folder2_Bookmark1", Url = "https://folder2-bookmark1.com" },
            };
            var state = new State() { Folders = folders, Bookmarks = bookmarks };
            var store = new BookmarkFileStore();

            store.Save(state);
        }

        [Fact]
        public void CanReadFromFile()
        {
            var store = new BookmarkFileStore();
            var state = store.Get();

            state.Should().BeOfType(typeof(State));
            state.Folders.Count.Should().Be(3);
            state.Bookmarks.Count.Should().Be(3);
            state.Bookmarks.Where(x => x.FolderId == 1).Count().Should().Be(2);
            state.Bookmarks.Where(x => x.FolderId == 2).Count().Should().Be(1);
            state.Bookmarks.Where(x => x.FolderId == 3).Should().BeEmpty();
        }
    }
}
