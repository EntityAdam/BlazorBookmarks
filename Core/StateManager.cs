using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public sealed class StateManager<T> : IStateManager<T> where T : IDeepCloneable<T>
    {
        //todo concurrent stack
        private readonly Stack<T> UndoStack = new();
        private readonly Stack<T> RedoStack = new();

        private T CurrentState { get; set; }

        //todo this is bad!
        public async Task<T> GetState() => CurrentState;

        public async Task LoadState(T state)
        {
            CurrentState = state.DeepCopy();
        }

        public async Task Snapshot(T state)
        {
            UndoStack.Push(CurrentState);
            CurrentState = state.DeepCopy();
        }

        public async Task<T> Redo()
        {
            if (RedoStack.Count > 0)
            {
                UndoStack.Push(CurrentState);
                CurrentState = RedoStack.Pop();
            }

            return CurrentState;
        }

        public async Task<T> Undo()
        {
            if (UndoStack.Count > 0)
            {
                RedoStack.Push(CurrentState);
                CurrentState = UndoStack.Pop();
            }

            return CurrentState;
        }
    }
}