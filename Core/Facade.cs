﻿using System;
using System.Linq;
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

        public void DeleteBookmark(int bookmarkId)
        {
            GetState().Bookmarks.RemoveAll(x => x.Id == bookmarkId);
            Snapshot(GetState());
        }

        public void AddFolder(string folderName)
        {
            var index = GetState().Folders.Max(x => x.Id) +1 ;
            GetState().Folders.Add(new FolderModel() { Id = index, Name = folderName, LastUpdated = DateTime.Now });
            Snapshot(GetState());
        }

        public void DeleteFolder(int folderId)
        {
            GetState().Folders.RemoveAll(x => x.Id == folderId);
            GetState().Bookmarks.RemoveAll(x => x.FolderId == folderId);
            Snapshot(GetState());
        }

        public StateModel GetState() => stateManager.CurrentState;
        public StateModel GetStateFromStore()
        {
            var state = bookmarkStore.Get();
            stateManager.LoadState(state);
            stateManager.Snapshot(state);
            return state;
        }
        public StateModel Redo()
        {
            var state = stateManager.Redo();
            SaveStateToStore(state);
            return state;
        }
        public void SaveStateToStore(StateModel state) => bookmarkStore.Save(state);
        public void Snapshot(StateModel state)
        {
            stateManager.Snapshot(state);
            SaveStateToStore(state);
        }
        public StateModel Undo()
        {
            var state = stateManager.Undo();
            SaveStateToStore(state);
            return state;
        }
    }
}
