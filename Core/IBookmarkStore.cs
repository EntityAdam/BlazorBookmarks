using Core.Models;

namespace Core
{
    public interface IBookmarkStore
    {
        public void Save(StateModel state);
        public StateModel Get();
    }
}