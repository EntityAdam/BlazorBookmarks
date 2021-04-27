using Core.Models;

namespace Core
{
    public class BookmarkMemoryStore : IBookmarkStore
    {
        private State State = new();

        public State Get() => State;

        public void Save(State state) => State = state;
    }
}
