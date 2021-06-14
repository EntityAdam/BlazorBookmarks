using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

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

        public async Task DeleteBookmark(int bookmarkId)
        {
            //remove from ui state
            var state = await GetState();
            state.Bookmarks.RemoveAll(x => x.Id == bookmarkId);
            //snap
            await Snapshot(await GetState());
        }

        public async Task EditBookmark(int bookmarkId, string name, string url)
        {
            var state = await GetState();
            var bookmarkToEdit = state.Bookmarks.Single(x => x.Id == bookmarkId);
            //todo mutation, want immutable
            bookmarkToEdit.Name = name;
            bookmarkToEdit.Url = url;
            await Snapshot(state);
        }

        public async Task<FolderModel> AddFolder(string folderName)
        {
            var state = await GetState();
            var index = state.Folders.Max(x => x.Id) + 1;
            var folder = new FolderModel() {Id = index, Name = folderName, LastUpdated = DateTime.Now};

            //add to ui state
            state = await GetState();
            state.Folders.Add(folder);
            //snap
            await Snapshot(await GetState());
            return folder;
        }

        public async Task DeleteFolder(int folderId)
        {
            var state = await GetState();
            state.Folders.RemoveAll(x => x.Id == folderId);
            state.Bookmarks.RemoveAll(x => x.FolderId == folderId);
            await Snapshot(await GetState());
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