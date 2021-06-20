using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public sealed class StateManager<T> : IStateManager<T> where T : IDeepCloneable<T>
    {
        //todo concurrent stack
        private readonly ConcurrentStack<T> UndoStack = new();
        private readonly ConcurrentStack<T> RedoStack = new();

        private T CurrentState { get; set; }
        
        public async Task<T> GetState() => CurrentState;

        public async Task LoadState(T state)
        {
            CurrentState = state.DeepCopy();
        }

        public async Task Snapshot(T state)
        {
            if (CurrentState != null) UndoStack.Push(CurrentState);
            CurrentState = state.DeepCopy();
        }

        public async Task<T> Redo()
        {
            if (RedoStack.Count == 0) return CurrentState;
            
            UndoStack.Push(CurrentState);

            RedoStack.TryPop(out var result);

            if (result != null)
                CurrentState = result;
            
            return CurrentState;
        }

        public async Task<T> Undo()
        {
            if (UndoStack.Count == 0) return CurrentState;
            
            RedoStack.Push(CurrentState);

            RedoStack.TryPop(out var result);

            if (result != null)
                CurrentState = result;
            
            return CurrentState;
        }
    }
}