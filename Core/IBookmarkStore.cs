using Core.Models;

namespace Core
{
    public interface IBookmarkStore
    {
        public void Save(State state);
        public State Get();
    }
}