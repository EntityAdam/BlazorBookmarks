using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Core.Models;
using Core;

namespace UnitTests
{
    public class StateTests
    {

        private Facade Originator { get; set; }

        public StateTests()
        {
            Originator = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());
            InitializeState();
        }

        private void InitializeState()
        {
            var folders = new List<FolderModel>() {
                new FolderModel() { Id = 1, Name = "Folder1" }
            };
            var bookmarks = new List<BookmarkModel>() {
                new BookmarkModel() { Id = 1, FolderId = 1, Name = "Bookmark1" }
            };

            var state = new StateModel(folders, bookmarks);

            Task.Run(() => Originator.Snapshot(state));
        }

        private async Task AddNewState()
        {
            var newState = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder2" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark2", FolderId = 1 } }
            };

            await Originator.Snapshot(newState);
        }

        [Fact]
        public async Task InitialState_Should_BeValid()
        {
            var state = await Originator.GetState();
            state.Folders[0].Name.Should().Be("Folder1");
            state.Bookmarks[0].Name.Should().Be("Bookmark1");
            state.Bookmarks[0].FolderId.Should().Be(1);
        }

        [Fact]
        public async Task InitialState_Should_NotUndo()
        {
            var state = await Originator.GetState();

            await Originator.Undo();

            state.Folders[0].Name.Should().Be("Folder1");
            state.Bookmarks[0].Name.Should().Be("Bookmark1");
            state.Bookmarks[0].FolderId.Should().Be(1);
        }

        [Fact]
        public void State_Should_DeepCopy()
        {
            var folders = new List<FolderModel>() { new FolderModel() { Id = 1, Name = "Folder1" } };
            var bookmarks = new List<BookmarkModel>() { new BookmarkModel() { Id = 1, FolderId = 1, Name = "Bookmark1" } };
            var state1 = new StateModel(folders, bookmarks);

            var state2 = state1.DeepCopy();

            state1.Should().NotBeSameAs(state2);
            state1.Folders.First().Should().NotBeSameAs(state2.Folders.First());
            state1.Bookmarks.First().Should().NotBeSameAs(state2.Bookmarks.First());
        }

        [Fact]
        public async Task State_Should_SnapShot()
        {
            await AddNewState();

            var currentState = await Originator.GetState();
            currentState.Folders[0].Name.Should().Be("Folder2");
            currentState.Bookmarks[0].Name.Should().Be("Bookmark2");
            currentState.Bookmarks[0].FolderId.Should().Be(1);
        }

        [Fact]
        public async Task State_Should_Undo()
        {
            await AddNewState();

            await Originator.Undo();

            var currentState = await Originator.GetState();
            currentState.Folders[0].Name.Should().Be("Folder1");
            currentState.Bookmarks[0].Name.Should().Be("Bookmark1");
            currentState.Bookmarks[0].FolderId.Should().Be(1);

        }

        [Fact]
        public async Task State_Should_Redo()
        {
            await AddNewState();
            
            await Originator.Undo();
            await Originator.Redo();

            var currentState = await Originator.GetState();
            currentState.Folders[0].Name.Should().Be("Folder2");
            currentState.Bookmarks[0].Name.Should().Be("Bookmark2");
            currentState.Bookmarks[0].FolderId.Should().Be(1);
        }

        [Fact]
        public async Task OriginatorUndo_ShouldReturnToOriginalStateAndNotFail()
        {
            await AddNewState();

            await Originator.Undo();
            await Originator.Undo();
            await Originator.Undo();

            var currentState = await Originator.GetState();
            currentState.Folders[0].Name.Should().Be("Folder1");
            currentState.Bookmarks[0].Name.Should().Be("Bookmark1");
            currentState.Bookmarks[0].FolderId.Should().Be(1);
        }

        [Fact]
        public async Task OriginatorRedo_ShouldReturnToLatestStateAndNotFail()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());

            var state1 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder1" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark1", FolderId = 1 } }
            };

            await orig.Snapshot(state1);

            var state2 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder2" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark2", FolderId = 1 } }
            };

            await orig.Undo();
            await orig.Redo();
            await orig.Redo();
            await orig.Redo();
            await orig.Redo();

            var state = await orig.GetState();
            state.Folders[0].Name.Should().Be("Folder1");
        }

        [Fact]
        public async Task Order_ShouldBePreserved_WhenOnlyIndexIsChanged()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());
            var folder1 = new FolderModel { Id = 1, Name = "Folder1" };
            var folder2 = new FolderModel { Id = 2, Name = "Folder2" };
            var folder3 = new FolderModel { Id = 3, Name = "Folder3" };
            var folders = new List<FolderModel>() { folder3, folder2, folder1 };
            var state1 = new StateModel(folders, new List<BookmarkModel>());

            await orig.Snapshot(state1);

            var sortedFolders = state1.Folders.OrderBy(x => x.Id).ToList();

            var state2 = new StateModel(sortedFolders, new List<BookmarkModel>());

            await orig.Snapshot(state2);

            var x = orig.Undo();
        }

        [Fact]
        public void CopyListIndexOrderRewriteIds()
        {
            var folder1 = new FolderModel { Id = 1, Name = "Folder1" };
            var folder2 = new FolderModel { Id = 2, Name = "Folder2" };
            var folder3 = new FolderModel { Id = 3, Name = "Folder3" };
            var folders = new List<FolderModel>() { folder3, folder2, folder1 };

            for (int i = 0; i < folders.Count; i++)
            {
                folders[i].Id = i;
            }

            var x = folders;
        }

        [Fact]
        public async Task Should_DeleteFolder_AndUndo()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());
            var folder1 = new FolderModel { Id = 1, Name = "Folder1" };
            var folder2 = new FolderModel { Id = 2, Name = "Folder2" };
            var folders = new List<FolderModel>() { folder1, folder2 };
            var state1 = new StateModel(folders, new List<BookmarkModel>());
            await orig.Snapshot(state1);

            await orig.DeleteFolder(2);

            var state = await orig.GetState();

            state.Folders.Count.Should().Be(1);
            state.Folders[0].Name.Should().Be("Folder1");
        }

        [Fact]
        public async Task Should_DeleteBookmark()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());
            var folder1 = new FolderModel { Id = 1, Name = "Folder1" };
            var folders = new List<FolderModel>() { folder1 };
            var bookmark1 = new BookmarkModel() { Id = 1, FolderId = 1, Name = "Bookmark1" };
            var bookmark2 = new BookmarkModel() { Id = 2, FolderId = 1, Name = "Bookmark2" };
            var bookmarks = new List<BookmarkModel>() { bookmark1, bookmark2 };
            var state = new StateModel(folders, bookmarks);
            await orig.Snapshot(state);
            await orig.DeleteBookmark(1);
            state = await orig.GetState();
            state.Bookmarks.Count.Should().Be(1);
        }

        [Fact]
        public async Task Should_DeleteFolder_And_DeleteChildBookmarks()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());
            var folder1 = new FolderModel { Id = 1, Name = "Folder1" };
            var folders = new List<FolderModel>() { folder1 };
            var bookmark1 = new BookmarkModel() { Id = 1, FolderId = 1, Name = "Bookmark1" };
            var bookmark2 = new BookmarkModel() { Id = 2, FolderId = 1, Name = "Bookmark2" };
            var bookmarks = new List<BookmarkModel>() { bookmark1, bookmark2 };
            var state = new StateModel(folders, bookmarks);
            await orig.Snapshot(state);
            await orig.DeleteFolder(1);
            state = await orig.GetState();
            state.Bookmarks.Count.Should().Be(0);
        }

        [Fact]
        public async Task Should_EditBookmark_AndUndo()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());
            var folder1 = new FolderModel { Id = 1, Name = "Folder1" };
            var folders = new List<FolderModel>() { folder1 };
            var bookmark1 = new BookmarkModel() { Id = 1, FolderId = 1, Name = "Bookmark1" };
            var bookmark2 = new BookmarkModel() { Id = 2, FolderId = 1, Name = "Bookmark2" };
            var bookmarks = new List<BookmarkModel>() { bookmark1, bookmark2 };
            var state = new StateModel(folders, bookmarks);
            await orig.Snapshot(state);
            await orig.EditBookmark(1, "Google", "www.google.com");
            state = await orig.GetState();
            state.Bookmarks.Count.Should().Be(2);
            state.Bookmarks.First().Name.Should().Be("Google");
            state.Bookmarks.First().Url.Should().Be("www.google.com");
        }
    }
}