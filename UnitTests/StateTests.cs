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
            var b1 = new Bookmark() { Id = 1, FolderId = 1, Name = "Bookmark1" };
            var b2 = b1.ShallowCopy();

            Assert.True(b1.Name == b2.Name && b1.Id == b2.Id);
            Assert.False(ReferenceEquals(b1, b2));
        }

        [Fact]
        public void Folder_Should_ShallowCopy()
        {
            var f1 = new Folder() { Id = 1, Name = "Folder1" };
            var f2 = f1.ShallowCopy();

            f1.Should().BeEquivalentTo(f2);
            f1.Should().NotBeSameAs(f2);
        }

        [Fact]
        public void State_Should_DeepCopy()
        {
            var folders = new List<Folder>() { new Folder() { Id = 1, Name = "Folder1" } };
            var bookmarks = new List<Bookmark>() { new Bookmark() { Id = 1, FolderId = 1, Name = "Bookmark1" } };

            var state1 = new State(folders, bookmarks);
            var state2 = state1.DeepCopy();

            state1.Should().NotBeSameAs(state2);
            state1.Folders.First().Should().NotBeSameAs(state2.Folders.First());
            state1.Bookmarks.First().Should().NotBeSameAs(state2.Bookmarks.First());
        }

        [Fact]
        public void State_Should_DeepCopy2()
        {
            var stack = new Stack<State>();
            var state1 = new State() { Folders = new List<Folder> { new Folder() { Id = 1, Name = "Folder1" } } };
            stack.Push(state1.DeepCopy());

            var state2 = stack.Pop();          
            Assert.False(ReferenceEquals(state1, state2));

            state1.Folders[0].Name = "Folder2";
            
        }

        [Fact]
        public void Originator_Should_AddFolder()
        {
            var orig = new Facade(new StateManager<State>(), new BookmarkMemoryStore());

            orig.AddFolder("Folder1");

            orig.CurrentState.Folders.Count.Should().Be(1);
        }

        [Fact]
        public void Originator_Should_Undo()
        {
            var orig = new Facade(new StateManager<State>(), new BookmarkMemoryStore());
            orig.AddFolder("Folder1");

            orig.Undo();

            orig.CurrentState.Folders.Count.Should().Be(0);
        }

        [Fact]
        public void Originator_Should_Redo()
        {
            var orig = new Facade(new StateManager<State>(), new BookmarkMemoryStore());
            orig.AddFolder("Folder1");

            orig.Undo();
            orig.Redo();

            orig.CurrentState.Folders.Count.Should().Be(1);
        }

        [Fact]
        public void OriginatorUndo_ShouldReturnToOriginalStateAndNotFail()
        {
            var orig = new Facade(new StateManager<State>(), new BookmarkMemoryStore());
            orig.AddFolder("Folder1");

            orig.Undo();
            orig.Undo();

            orig.CurrentState.Folders.Count.Should().Be(0);
        }

        [Fact]
        public void OriginatorRedo_ShouldReturnToLatestStateAndNotFail()
        {
            var orig = new Facade(new StateManager<State>(), new BookmarkMemoryStore());
            orig.AddFolder("Folder1");
            orig.AddFolder("Folder2");

            orig.Undo();
            orig.Undo();
            orig.Redo();
            orig.Redo();
            orig.Redo();

            orig.CurrentState.Folders.Count.Should().Be(2);
        }
    }
}
