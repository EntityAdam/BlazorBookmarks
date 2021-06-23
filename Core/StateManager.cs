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
        
        public Task<T> GetState() => Task.FromResult(CurrentState);

        public Task LoadState(T state)
        {
            CurrentState = state.DeepCopy();
            return Task.CompletedTask;
        }

        public Task Snapshot(T state)
        {
            if (CurrentState != null) UndoStack.Push(CurrentState);
            CurrentState = state.DeepCopy();
            return Task.CompletedTask;
        }

        public Task<T> Redo()
        {
            if (RedoStack.IsEmpty) return Task.FromResult(CurrentState);
            
            UndoStack.Push(CurrentState);

            RedoStack.TryPop(out var result);

            if (result != null)
                CurrentState = result;
            
            return Task.FromResult(CurrentState);
        }

        public Task<T> Undo()
        {
            if (UndoStack.IsEmpty) return Task.FromResult(CurrentState);
            
            RedoStack.Push(CurrentState);

            UndoStack.TryPop(out var result);

            if (result != null)
                CurrentState = result;
            
            return Task.FromResult(CurrentState);
        }
    }
}