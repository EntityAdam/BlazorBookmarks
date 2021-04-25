using StateService.Models;
using System.Collections.Generic;

namespace StateService
{
    public sealed class StateManager : IStateManager
    {
        private readonly Stack<State> UndoStack = new Stack<State>();
        private readonly Stack<State> RedoStack = new Stack<State>();
        private State CurrentState { get; set; }

        public void UpdateState(State state)
        {
            if (CurrentState != null)
            {
                UndoStack.Push(CurrentState);
            }
            CurrentState = state;
        }

        public State Redo()
        {
            if (RedoStack.Count > 0)
            {
                UndoStack.Push(CurrentState);
                CurrentState = RedoStack.Pop();
            }
            return CurrentState;
        }
        public State Undo()
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