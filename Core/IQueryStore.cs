using Core.Models;

namespace Core
{
    public interface IQueryStore
    {
        public Task SaveChanges(QueryState state);

        public Task<QueryState> GetAll();
    }
}
