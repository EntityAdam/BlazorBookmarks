using Core.Models;

namespace Core
{
    public class BookmarkMemoryStore : IBookmarkStore
    {
        private StateModel State = new();

        public StateModel Get() => State;

        public void Save(StateModel state) => State = state;
    }
}
