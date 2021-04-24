using StateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateService
{
    public class StateManager //caretaker
    {
        private Stack<State> undoStack = new Stack<State>();
        private Stack<State> redoStack = new Stack<State>();
        private State currentState { get; set; }

        public StateManager(State state)
        {
            currentState = state;
        }

        public void Undo()
        {
            redoStack.Push(currentState);
            currentState = undoStack.Pop();
        }

        public void Redo()
        {
            undoStack.Push(currentState);
            currentState = redoStack.Pop();
        }
    }

    public class State //memento
    {
        public State(IEnumerable<Folder> folders, IEnumerable<Bookmark> bookmarks)
        {
            Folders = folders;
            Bookmarks = bookmarks;
        }

        public IEnumerable<Folder> Folders { get; private set; }
        public IEnumerable<Bookmark> Bookmarks { get; private set; }
    }
}


