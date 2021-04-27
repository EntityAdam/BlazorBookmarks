using System.Linq;
using Core;
using Core.Models;


namespace UnitTests
{

    internal class TestAppWithState : ISnapshottable<State>
    {
        public State CurrentState { get; private set; }
        public StateManagerBase<State> StateManager { get; }

        public TestAppWithState()
        {
            CurrentState = new State();
            //fetch state from persistence
            CurrentState.Folders.Add(new Folder { Id = 1, Name = "Folder1" });
            CurrentState.Bookmarks.Add(new Bookmark { Id = 1, FolderId = 1, Name = "Bookmark1 " });
            //update statemanager
            StateManager = new StateManagerBase<State>();
            StateManager.UpdateState(CurrentState.DeepCopy());
        }

        public void AddFolder(string name)
        {
            CurrentState.Folders.Add(new Folder { Id = CurrentState.Folders.Max(x => x.Id) + 1, Name = name });
            Snapshot();
        }

        public void Snapshot() => StateManager.UpdateState(CurrentState.DeepCopy());
        public void Undo() => CurrentState = StateManager.Undo();
        public void Redo() => CurrentState = StateManager.Redo();
    }
}
