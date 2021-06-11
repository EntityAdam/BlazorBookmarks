using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public class BookmarkMemoryStore : IBookmarkStore
    {
        private StateModel _state = new();
        
        public Task<StateModel> Get() => Task.FromResult(this._state);
        public Task Save(StateModel state)
        {
            this._state = state;
            return Task.FromResult(this._state);
        }
    }
}
