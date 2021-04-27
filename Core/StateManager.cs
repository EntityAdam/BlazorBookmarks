using System.Collections.Generic;

namespace Core
{
    public sealed class StateManager<T> : IStateManager<T> where T : IDeepCloneable<T>
    {
        private readonly Stack<T> UndoStack = new();
        private readonly Stack<T> RedoStack = new();

        public T CurrentState { get; private set; }

        public void LoadState(T state)
        {
            CurrentState = state.DeepCopy();
        }

        public void Snapshot(T state)
        {
            UndoStack.Push(CurrentState);
            CurrentState = state.DeepCopy();
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