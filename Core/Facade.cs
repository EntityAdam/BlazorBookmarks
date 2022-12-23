using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public class Facade : ISnapshottable<StateModel>, IFacade
    {
        private readonly IStateManager<StateModel> stateManager;

        private readonly IPersistantStore bookmarkStore;

        public Facade(IStateManager<StateModel> stateManager, IPersistantStore bookmarkStore)
        {
            this.stateManager = stateManager;
            this.bookmarkStore = bookmarkStore;
        }

        public async Task<StateModel> GetState() => await stateManager.GetState();

        public async Task<StateModel> GetStateFromStore()
        {
            var state = await bookmarkStore.Get();
            await stateManager.LoadState(state);
            await stateManager.Snapshot(state);
            return state;
        }

        public async Task<StateModel> Redo()
        {
            var state = await stateManager.Redo();
            await SaveStateToStore(state);
            return state;
        }

        public async Task SaveStateToStore(StateModel state) => await bookmarkStore.Save(state);

        public async Task Snapshot(StateModel state)
        {
            await stateManager.Snapshot(state);
            await SaveStateToStore(state);
        }

        public async Task<StateModel> Undo()
        {
            var state = await stateManager.Undo();
            await SaveStateToStore(state);
            return state;
        }
    }
}