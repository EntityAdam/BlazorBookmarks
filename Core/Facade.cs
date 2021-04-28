using Core.Models;
using System;
using System.Linq;

namespace Core
{
    public class Facade : ISnapshottable<StateModel>, IFacade
    {
        private readonly IStateManager<StateModel> stateManager;

        private readonly IBookmarkStore bookmarkStore;

        public Facade(IStateManager<StateModel> stateManager, IBookmarkStore bookmarkStore)
        {
            this.stateManager = stateManager;
            this.bookmarkStore = bookmarkStore;
        }

        public StateModel GetState() => stateManager.CurrentState;
        public StateModel GetStateFromStore()
        {
            var state = bookmarkStore.Get();
            stateManager.LoadState(state);
            stateManager.Snapshot(state);
            return state;
        }
        public StateModel Redo() => stateManager.Redo();
        public void Snapshot(StateModel state) => stateManager.Snapshot(state);
        public StateModel Undo() => stateManager.Undo();
    }
}
