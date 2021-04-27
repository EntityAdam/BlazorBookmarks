using System.Collections.Generic;

namespace Core
{
    public sealed class StateManagerBase<T> : IStateManager<T>
    {
        private readonly Stack<T> UndoStack = new();
        private readonly Stack<T> RedoStack = new();
        
        public T CurrentState { get; private set; }

        public void UpdateState(T state)
        {
            if (CurrentState != null)
            {
                UndoStack.Push(CurrentState);
            }
            CurrentState = state;
        }

        public T Redo()
        {
            if (RedoStack.Count > 0)
            {
                UndoStack.Push(CurrentState);
                CurrentState = RedoStack.Pop();
            }
            return CurrentState;
        }
        
        public T Undo()
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