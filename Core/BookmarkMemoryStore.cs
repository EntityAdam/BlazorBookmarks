using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public class BookmarkMemoryStore : IBookmarkStore
    {
        private StateModel _state = new();
        
        public async Task<StateModel> Get() => await Task.FromResult(_state);
        public async Task Save(StateModel state) => this._state = state;
    }
}
