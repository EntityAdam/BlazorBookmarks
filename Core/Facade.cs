using Core.Models;
using StateService;
using System;

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

        private void LoadState() => stateManager.UpdateState(bookmarkStore.Get());

        public State CurrentState => stateManager.CurrentState;

        public StateManagerBase<State> StateManager => throw new NotImplementedException();

        public void Redo() => stateManager.Redo();

        public void Snapshot() => throw new NotImplementedException();

        public void Undo() => stateManager.Undo();
    }
}
