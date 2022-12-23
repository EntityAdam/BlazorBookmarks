using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class QueryStore : IQueryStore
    {
        private readonly IPersistantStore persistantStore;

        public QueryStore(IPersistantStore persistantStore)
        {
            this.persistantStore = persistantStore;
        }
        public async Task<QueryState> GetAll()
        {
            var state = await persistantStore.Get();
            var b = state.Bookmarks;
            var bookmarks = b.Select(x => new BookmarkQueryModel(x.Id, x.FolderId, x.Url, x.Name, x.Clicks, false));
            return new QueryState(bookmarks);
        }

        public async Task SaveChanges(QueryState state)
        {
            var store = await persistantStore.Get();
            var changes = state.Bookmarks.Where(x => x.HasChangesToPersist);
            store.Bookmarks.RemoveAll(x => changes.Any(c => c.Id == x.Id));
            store.Bookmarks.AddRange(changes.Where(x => x.HasChangesToPersist).Select(x => new BookmarkModel(x.Id, x.FolderId, x.Url, x.Name, x.Clicks)));
        }
    }
}
