using Core.Models;
using System;
using System.Linq;

namespace Core
{
    public class Facade : ISnapshottable<State>, IFacade
    {
        private readonly IStateManager<State> stateManager;

        private readonly IBookmarkStore bookmarkStore;

        public Facade(IStateManager<State> stateManager, IBookmarkStore bookmarkStore)
        {
            this.stateManager = stateManager;
            this.bookmarkStore = bookmarkStore;
            LoadState();
        }

        public State CurrentState { get; set; }

        public void Redo() => CurrentState = stateManager.Redo();

        public void Snapshot() => stateManager.Snapshot(CurrentState);

        public void Undo() => CurrentState = stateManager.Undo();

        private void LoadState()
        {
            CurrentState = bookmarkStore.Get();
            stateManager.LoadState(CurrentState);
        }

        public void AddFolder(string name)
        {
            int nextId;
            if (CurrentState.Folders.Count == 0)
            {
                nextId = 1;
            }
            else
            {
                nextId = CurrentState.Folders.Max(x => x.Id) + 1;
            }
            CurrentState.Folders.Add(new Folder { Id = nextId, Name = name });
            Snapshot();
        }
    }
}
