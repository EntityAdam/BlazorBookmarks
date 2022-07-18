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
        private Facade StateModel { get; set; }

        public StateTests()
        {
            StateModel = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());
            InitializeState();
        }

        private void InitializeState()
        {
            var folders = new List<FolderModel>() {
                new FolderModel(1, "Folder1", DateTime.UtcNow)
            };
            var bookmarks = new List<BookmarkModel>() {
                new BookmarkModel(1, 1, "https://null.com", "Bookmark1", 0)
            };

            var state = new StateModel(folders, bookmarks);

            Task.Run(() => StateModel.Snapshot(state));
        }

        private async Task AddNewState()
        {
            var newState = new StateModel()
            {
                Folders = new List<FolderModel> { new(2, "Folder2", DateTime.UtcNow) },
                Bookmarks = new List<BookmarkModel> { new(2, 2, "https://null.com", "Bookmark2", 0) }
            };

            await StateModel.Snapshot(newState);
        }

        [Fact]
        public async Task InitialState_Should_BeValid()
        {
            var state = await StateModel.GetState();
            state.Folders[0].Name.Should().Be("Folder1");
            state.Bookmarks[0].Name.Should().Be("Bookmark1");
            state.Bookmarks[0].FolderId.Should().Be(1);
        }

        [Fact]
        public async Task InitialState_Should_NotUndo()
        {
            var state = await StateModel.GetState();

            await StateModel.Undo();

            state.Folders[0].Name.Should().Be("Folder1");
            state.Bookmarks[0].Name.Should().Be("Bookmark1");
            state.Bookmarks[0].FolderId.Should().Be(1);
        }

        [Fact]
        public async Task State_Should_SnapShot()
        {
            await AddNewState();

            var currentState = await StateModel.GetState();
            currentState.Folders[0].Name.Should().Be("Folder2");
            currentState.Bookmarks[0].Name.Should().Be("Bookmark2");
            currentState.Bookmarks[0].FolderId.Should().Be(2);
        }

        [Fact]
        public async Task State_Should_Undo()
        {
            await AddNewState();

            await StateModel.Undo();

            var currentState = await StateModel.GetState();
            currentState.Folders[0].Name.Should().Be("Folder1");
            currentState.Bookmarks[0].Name.Should().Be("Bookmark1");
            currentState.Bookmarks[0].FolderId.Should().Be(1);

        }

        [Fact]
        public async Task State_Should_Redo()
        {
            await AddNewState();

            await StateModel.Undo();
            await StateModel.Redo();

            var currentState = await StateModel.GetState();
            currentState.Folders[0].Name.Should().Be("Folder2");
            currentState.Bookmarks[0].Name.Should().Be("Bookmark2");
            currentState.Bookmarks[0].FolderId.Should().Be(2);
        }

        [Fact]
        public async Task OriginatorUndo_ShouldReturnToOriginalStateAndNotFail()
        {
            await AddNewState();

            await StateModel.Undo();
            await StateModel.Undo();
            await StateModel.Undo();

            var currentState = await StateModel.GetState();
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
                Folders = new List<FolderModel> { new(1, "Folder1", DateTime.UtcNow) },
                Bookmarks = new List<BookmarkModel> { new(1, 1, "https://null.com", "Bookmark1", 0) }
            };

            await orig.Snapshot(state1);

            var state2 = new StateModel()
            {
                Folders = new List<FolderModel> { new(1, "Folder1", DateTime.UtcNow) },
                Bookmarks = new List<BookmarkModel> { new(1, 1, "https://null.com", "Bookmark1", 0) }
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
            FolderModel folder1 = new(1, "Folder1", DateTime.UtcNow);
            FolderModel folder2 = new(2, "Folder2", DateTime.UtcNow);
            FolderModel folder3 = new(3, "Folder3", DateTime.UtcNow);
            List<FolderModel> folders = new() { folder3, folder2, folder1 };
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
            var folder1 = new FolderModel(30, "Folder1", DateTime.UtcNow);
            var folder2 = new FolderModel(20, "Folder2", DateTime.UtcNow);
            var folder3 = new FolderModel(10, "Folder3", DateTime.UtcNow);
            var folders = new List<FolderModel>() { folder3, folder2, folder1 };

            List<FolderModel> newOrder = new();
            for (int i = 0; i < folders.Count; i++)
            {
                newOrder.Add(folders[i] with { Id = i });
            }

        }
    }
}