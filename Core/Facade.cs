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
        public StateModel GetStateFromStore() => bookmarkStore.Get();
        public StateModel Redo() => stateManager.Redo();
        public void Snapshot(StateModel state) => stateManager.Snapshot(state);
        public StateModel Undo() => stateManager.Undo();
    }
}
