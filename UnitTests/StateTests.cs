using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using Core.Models;
using Core;

namespace UnitTests
{
    public class StateTests
    {
        [Fact]
        public void Bookmark_Should_ShallowCopy()
        {
            var b1 = new BookmarkModel() { Id = 1, FolderId = 1, Name = "Bookmark1" };
            var b2 = b1.ShallowCopy();

            Assert.True(b1.Name == b2.Name && b1.Id == b2.Id);
            Assert.False(ReferenceEquals(b1, b2));
        }

        [Fact]
        public void Folder_Should_ShallowCopy()
        {
            var f1 = new FolderModel() { Id = 1, Name = "Folder1" };
            var f2 = f1.ShallowCopy();

            f1.Should().BeEquivalentTo(f2);
            f1.Should().NotBeSameAs(f2);
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
        public void State_Should_DeepCopy2()
        {
            var stack = new Stack<StateModel>();
            var state1 = new StateModel() { Folders = new List<FolderModel> { new FolderModel() { Id = 1, Name = "Folder1" } } };
            stack.Push(state1.DeepCopy());

            var state2 = stack.Pop();
            Assert.False(ReferenceEquals(state1, state2));

            state1.Folders[0].Name = "Folder2";

        }

        [Fact]
        public void Originator_Should_Undo()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());

            var state1 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder1" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark1", FolderId = 1 } }
            };

            orig.Snapshot(state1);

            var state2 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder2" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark2", FolderId = 1 } }
            };

            orig.Snapshot(state2);

            orig.Undo();

            orig.GetState().Folders[0].Name.Should().Be("Folder1");
        }

        [Fact]
        public void Originator_Should_Redo()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());

            var state1 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder1" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark1", FolderId = 1 } }
            };

            orig.Snapshot(state1);

            var state2 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder2" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark2", FolderId = 1 } }
            };

            orig.Snapshot(state2);


            orig.Undo();
            orig.Redo();

            orig.GetState().Folders[0].Name.Should().Be("Folder2");
        }

        [Fact]
        public void OriginatorUndo_ShouldReturnToOriginalStateAndNotFail()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());

            var state1 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder1" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark1", FolderId = 1 } }
            };

            orig.Snapshot(state1);

            var state2 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder2" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark2", FolderId = 1 } }
            };

            orig.Undo();
            orig.Undo();
            orig.Undo();
            orig.Undo();

            orig.GetState().Folders[0].Name.Should().Be("Folder1");
        }

        [Fact]
        public void OriginatorRedo_ShouldReturnToLatestStateAndNotFail()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());

            var state1 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder1" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark1", FolderId = 1 } }
            };

            orig.Snapshot(state1);

            var state2 = new StateModel()
            {
                Folders = new List<FolderModel> { new FolderModel { Id = 1, Name = "Folder2" } },
                Bookmarks = new List<BookmarkModel> { new BookmarkModel { Id = 1, Name = "Bookmark2", FolderId = 1 } }
            };

            orig.Undo();
            orig.Redo();
            orig.Redo();
            orig.Redo();
            orig.Redo();

            orig.GetState().Folders[0].Name.Should().Be("Folder2");
        }

        [Fact]
        public void Order_ShouldBePreserved_WhenOnlyIndexIsChanged()
        {
            var orig = new Facade(new StateManager<StateModel>(), new BookmarkMemoryStore());
            var folder1 = new FolderModel { Id = 1, Name = "Folder1" };
            var folder2 = new FolderModel { Id = 2, Name = "Folder2" };
            var folder3 = new FolderModel { Id = 3, Name = "Folder3" };
            var folders = new List<FolderModel>() { folder3, folder2, folder1 };
            var state1 = new StateModel(folders, new List<BookmarkModel>());

            orig.Snapshot(state1);

            var sortedFolders = state1.Folders.OrderBy(x => x.Id).ToList();

            var state2 = new StateModel(sortedFolders, new List<BookmarkModel>());

            orig.Snapshot(state2);

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
    }
}
