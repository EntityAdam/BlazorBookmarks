using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Core;
using Core.Models;

namespace UnitTests
{
    public class FileStoreTests
    {
        [Fact]
        public async Task CanSaveToFile()
        {
            var folders = new List<FolderModel>()
            {
                new(1, "Folder1", DateTime.UtcNow),
                new(2, "Folder2", DateTime.UtcNow),
                new(3, "Folder3", DateTime.UtcNow),
                new(4, "Folder4", DateTime.UtcNow),
                new(5, "Folder5", DateTime.UtcNow),
            };
            var bookmarks = new List<BookmarkModel>()
            {
                new(1, 1, "https://entityadam.com", "Folder1_Bookmark1", 0),
                new(1, 1, "https://entityadam.com", "Folder1_Bookmark2", 0),
                new(1, 1, "https://entityadam.com", "Folder1_Bookmark3", 0),
                new(1, 1, "https://entityadam.com", "Folder1_Bookmark4", 0),
                new(1, 1, "https://entityadam.com", "Folder1_Bookmark5", 0),
                new(1, 2, "https://entityadam.com", "Folder2_Bookmark1", 0),
                new(1, 2, "https://entityadam.com", "Folder2_Bookmark2", 0),
                new(1, 2, "https://entityadam.com", "Folder2_Bookmark3", 0),
                new(1, 2, "https://entityadam.com", "Folder2_Bookmark4", 0),
                new(1, 2, "https://entityadam.com", "Folder2_Bookmark5", 0),
                new(1, 3, "https://entityadam.com", "Folder3_Bookmark1", 0),
                new(1, 3, "https://entityadam.com", "Folder3_Bookmark2", 0),
                new(1, 3, "https://entityadam.com", "Folder3_Bookmark3", 0),
                new(1, 3, "https://entityadam.com", "Folder3_Bookmark4", 0),
                new(1, 3, "https://entityadam.com", "Folder3_Bookmark5", 0),
            };
            var state = new StateModel() { Folders = folders, Bookmarks = bookmarks };
            var store = new LocalBookmarkFileStore();

            await store.Save(state);
        }

        [Fact]
        public async Task CanReadFromFile()
        {
            var store = new LocalBookmarkFileStore();
            var state = await store.Get();

            state.Folders.Count.Should().Be(5);
            state.Bookmarks.Count.Should().Be(15);
            state.Bookmarks.Count(x => x.FolderId == 1).Should().Be(5);
            state.Bookmarks.Count(x => x.FolderId == 2).Should().Be(5);
            state.Bookmarks.Count(x => x.FolderId == 3).Should().Be(5);
        }
    }
}
